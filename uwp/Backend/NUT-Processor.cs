using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace nuttyupsclient.Backend
{
    class NUT_Processor
    {
        public static string[,] UPSVariables;

        public static Tuple<List<string>,bool> ValidateNUTOutput(string NUTOutput)
        {

            MainPage.debugLog.Debug("[PROCESSOR:VALIDATOR] Received data:\n" + NUTOutput);
            //NUTOutput = SanitizeNUTOutput(NUTOutput);
            
            MainPage.debugLog.Debug("[PROCESSOR:VALIDATOR] Attempting to validate output");
            List<string> NUTList = new List<string>(NUTOutput.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));

            // Sanity check! 
            if (NUTList[0].Contains("BEGIN LIST VAR ups") && NUTList[NUTList.Count - 1].Contains("END LIST VAR ups"))
            {
                MainPage.debugLog.Debug("[PROCESSOR:VALIDATOR] Data structure is correct.");
                return Tuple.Create(NUTList,true);
            }
            MainPage.debugLog.Fatal("[PROCESSOR:VALIDATOR] Data structure is not correct.");
            return Tuple.Create(NUTList,false);

        }

        public static string SanitizeNUTOutput(string NUTOutput)
        {
            MainPage.debugLog.Debug("[PROCESSOR:SANITIZER] Attempting to sanitize output");
            return Regex.Replace(NUTOutput, @"\r\n?|\n", Environment.NewLine); // Replaces UNIX linefeeds with ANSI
        }

        public static string ParseNUTOutput(string NUTOutput)
        {

            Tuple<List<string>, bool> NUTValidatedData = ValidateNUTOutput(NUTOutput);

            List<string> NUTList = NUTValidatedData.Item1;

            
            UPSVariables = new string[NUTList.Count -1, 2]; // Truncating list by two to remove the `BEGIN LAST VAR` and `END LIST VAR` lines

            int j = 0;
            for (int i = 1; i < NUTList.Count - 1; i++)
            {
                List<string> NUTVarNames = new List<string>(NUTList[i].Split(new String[] { "\"" }, StringSplitOptions.RemoveEmptyEntries));

                // We're now putting the variable names and values alongside each other in the list, giving us an easy lookup table
                UPSVariables[j, 0] = NUTVarNames[0].Trim(' ').Substring(8); // Only needs the data in between the quotes
                UPSVariables[j, 1] = NUTVarNames[1].Trim(' ');

                j++;
            }
            
            string UPSStatusMessage = UPSStatistics();
            return UPSStatusMessage;

        }

        public static string UPSStatistics()
        {
            // Input voltage and nominal voltage
            decimal UPSInputVoltage = Convert.ToDecimal(SearchNUTData("input.voltage"));
            Console.WriteLine("[PROCESSOR] UPS input voltage is " + UPSInputVoltage);
            decimal UPSInputNominalVoltage = Convert.ToDecimal(SearchNUTData("input.voltage.nominal"));
            Console.WriteLine("[PROCESSOR] UPS nominal input voltage is " + UPSInputNominalVoltage);

            // Battery voltage and nominal voltage
            // TODO: Make this smarter, and skip if battery voltage info is not available
            /*decimal UPSBatteryVoltage = Convert.ToDecimal(SearchNUTData("battery.voltage"));
            Console.WriteLine("[PROCESSOR] UPS battery voltage is " + UPSBatteryVoltage);
            decimal UPSBatteryNominalVoltage = Convert.ToDecimal(SearchNUTData("battery.voltage.nominal"));
            Console.WriteLine("[PROCESSOR] UPS battery nominal voltage is " + UPSBatteryNominalVoltage);*/

            // Output voltage and nominal voltage
            decimal UPSOutputVoltage = Convert.ToDecimal(SearchNUTData("output.voltage"));
            Console.WriteLine("[PROCESSOR] UPS output voltage before correction is " + UPSOutputVoltage);
            //UPSOutputVoltage = UPSOutputVoltage - UPSBatteryVoltage; // It adds the battery voltage to the output voltage. Fixing this.
            Console.WriteLine("[PROCESSOR] UPS output voltage after correction is " + UPSOutputVoltage);

            // UPS alarm
            bool UPSBeeper = false;
            if (SearchNUTData("ups.beeper.status").Equals("enabled")) { UPSBeeper = true; }

            // Strings all the information 
            string UPSInfo = (
                    "Manufacturer: " + SearchNUTData("device.mfr")
                    + "\nModel: " + SearchNUTData("device.model")
                    + "\nSerial: " + SearchNUTData("device.serial")
                    + "\n\nBattery charge: " + SearchNUTData("battery.charge") + "%"
                    + "\nInput Voltage: " + UPSInputVoltage + "v / " + UPSInputNominalVoltage
                    + "\nOutput Voltage: " + UPSOutputVoltage + "v"
                    + "\n\nUPS Beeper enabled: " + UPSBeeper
                );

            return UPSInfo;

        }

        public static Tuple<string, double, int> GetBatteryStatus()
        {
           /* Status codes:
            * 0 = GREEN, UPS online and connected to AC
            * 1 = ORANGE, UPS online and running on battery
            * 2 = RED, UPS online and running on battery, critically low
            * -1 = Invalid/no data 
            */

            /* UPS Status messages:
             * OL = Online, connected to AC power
             * OB DISCHRG = On battery, no AC power, discharging
             */

            int UPSStatusCode = -1;

            string UPSStatusMessage = null;
            double  UPSBatteryRuntime = Convert.ToDouble(SearchNUTData("battery.runtime"));
            double UPSBatteryCharge = Convert.ToDouble(SearchNUTData("battery.charge"));

            string UPSStatus = SearchNUTData("ups.status");
            if (UPSStatus.Equals("OL"))
            {
                UPSStatusCode = 0; // GREEN - All OK

                if (UPSBatteryRuntime <= 60)
                {
                    UPSStatusMessage = (Math.Round(UPSBatteryRuntime, 0) + " sec"); // Only display in seconds, since it's exactly a minute (or less)
                }
                else
                {
                    UPSBatteryRuntime = Math.Round((UPSBatteryRuntime / 60), 0);
                    UPSStatusMessage = (UPSBatteryRuntime + " min"); // Breaks it down into minutes
                }
            }
            else if (UPSStatus.Equals("OB DISCHRG"))
            {
                UPSStatusCode = 1;
                if (UPSBatteryRuntime <= 60)
                {
                    UPSStatusMessage = (Math.Round(UPSBatteryRuntime, 0) + " sec remaining"); // Only display in seconds, since it's exactly a minute (or less)
                }
                else
                {
                    UPSBatteryRuntime = Math.Round((UPSBatteryRuntime / 60), 0);
                    UPSStatusMessage = (Math.Round(UPSBatteryRuntime, 0) + " min remaining"); // Breaks it down into minutes
                }
            }
            else if (UPSStatus.Equals("OL CHRG"))
            {
                UPSStatusCode = 2;
                if (UPSBatteryRuntime <= 60)
                {
                    UPSStatusMessage = (UPSBatteryCharge + "% " + Math.Round(UPSBatteryRuntime, 0) + " sec remaining"); // Only display in seconds, since it's exactly a minute (or less)
                }
                else
                {
                    UPSBatteryRuntime = Math.Round((UPSBatteryRuntime / 60), 0);
                    UPSStatusMessage = (UPSBatteryCharge + "% " + Math.Round(UPSBatteryRuntime, 0) + " min remaining"); // Breaks it down into minutes
                }
            }

            return Tuple.Create(UPSStatusMessage, UPSBatteryCharge, UPSStatusCode);
        }

        public static string SearchNUTData(string NUTVariable)
        {
            MainPage.debugLog.Trace("[PROCESSOR:SEARCH] Searching for " + NUTVariable);
            for (int i = 0; i < UPSVariables.Length; i++)
            {
                if (UPSVariables[i, 0].Contains(NUTVariable))
                {
                    return UPSVariables[i, 1];
                }
            }

            MainPage.debugLog.Debug("[PROCESSOR:SEARCHNUTDATA] Could not find requested variable");
            return "INVALID";
        }

        public static void ModifySimNUTData(string NUTVariable, string NUTValue)
        {
            MainPage.debugLog.Debug("[MODNUTDATA] Array length is " + Convert.ToString(UPSVariables.GetLength(0)));
            MainPage.debugLog.Debug("[MODNUTDATA] Invoked. Searching for: " + NUTVariable);
            for (int i = 0; i < UPSVariables.GetLength(0); i++)
            {
                if (UPSVariables[i, 0].Equals(NUTVariable))
                {
                    MainPage.debugLog.Debug("[SEARCHNUTDATA] Searched for " + NUTVariable + " and got: " + UPSVariables[i, 1]);
                    UPSVariables[i, 1] = NUTValue;
                    return;
                }
            }

            MainPage.debugLog.Debug("[SEARCHNUTDATA] Could not find requested variable");
            return;
        }

    }
}
