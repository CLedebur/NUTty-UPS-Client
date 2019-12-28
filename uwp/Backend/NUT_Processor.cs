using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Windows.UI;
using Windows.UI.Xaml;


namespace nuttyupsclient.Backend
{
    class NUT_Processor
    {
        public static string[,] UPSVariables;

        public static Tuple<List<string>,bool> ValidateNUTOutput(string NUTOutput)
        {

            NUT_Background.debugLog.Trace("[PROCESSOR:VALIDATOR] Received data:\n" + NUTOutput);
            NUT_Background.debugLog.Trace("[PROCESSOR:VALIDATOR] Attempting to validate output");


            // List<string> NUTList = new List<string>(NUTOutput.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));

            List<string> NUTList = new List<string>();

            // Simple test to see if the variable is not null
            if (NUTOutput == null) return Tuple.Create(NUTList, false);

            NUTList = NUTOutput.Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();

            // Sanity check! 
            if (NUTList[0].Contains("BEGIN LIST VAR ups") && NUTList[NUTList.Count - 1].Contains("END LIST VAR ups"))
            {
                NUT_Background.debugLog.Debug("[PROCESSOR:VALIDATOR] Data structure is correct.");
                return Tuple.Create(NUTList,true);
            }
            NUT_Background.debugLog.Fatal("[PROCESSOR:VALIDATOR] Data structure is not correct.");
            return Tuple.Create(NUTList,false);

        }

        public static void ParseNUTOutput(string NUTOutput)
        {

            Tuple<List<string>, bool> NUTValidatedData = ValidateNUTOutput(NUTOutput);

            List<string> NUTList = NUTValidatedData.Item1;
            
            UPSVariables = new string[NUTList.Count -1, 2]; // Truncating list by two to remove the `BEGIN LIST VAR` and `END LIST VAR` lines

            int j = 0;
            for (int i = 1; i < NUTList.Count - 1; i++)
            {
                List<string> NUTVarNames = new List<string>(NUTList[i].Split(new String[] { "\"" }, StringSplitOptions.RemoveEmptyEntries));

                // We're now putting the variable names and values alongside each other in the list, giving us an easy lookup table
                UPSVariables[j, 0] = NUTVarNames[0].Trim(' ').Substring(8); // Only needs the data in between the quotes
                UPSVariables[j, 1] = NUTVarNames[1].Trim(' ');

                j++;
            }
            
            return;

        }

        public static string UPSStatistics()
        {
            string UPSInfo = "";

            if (UPSVariables == null || UPSVariables.GetLength(0) == 0) return "No data has been received from the UPS.";

            try
            {
                // Input voltage and nominal voltage
                decimal UPSInputVoltage = Convert.ToDecimal(SearchNUTData("input.voltage"));
                decimal UPSInputNominalVoltage = Convert.ToDecimal(SearchNUTData("input.voltage.nominal"));

                // Battery voltage and nominal voltage
                // TODO: Make this smarter, and skip if battery voltage info is not available

                /*
                decimal UPSBatteryVoltage = Convert.ToDecimal(SearchNUTData("battery.voltage"));
                Console.WriteLine("[PROCESSOR] UPS battery voltage is " + UPSBatteryVoltage);
                decimal UPSBatteryNominalVoltage = Convert.ToDecimal(SearchNUTData("battery.voltage.nominal"));
                Console.WriteLine("[PROCESSOR] UPS battery nominal voltage is " + UPSBatteryNominalVoltage);
                */

                // Output voltage and nominal voltage
                decimal UPSOutputVoltage = Convert.ToDecimal(SearchNUTData("output.voltage"));
                //TODO: See if this is still necessary
                //UPSOutputVoltage = UPSOutputVoltage - UPSBatteryVoltage; // It adds the battery voltage to the output voltage. Fixing this.

                // UPS alarm
                bool UPSBeeper = false;
                if (SearchNUTData("ups.beeper.status").Equals("enabled")) { UPSBeeper = true; }

                // Strings all the information 
                UPSInfo = (
                        "Manufacturer: " + SearchNUTData("device.mfr")
                        + "\nModel: " + SearchNUTData("device.model")
                        + "\nSerial: " + SearchNUTData("device.serial")
                        + "\n\nBattery charge: " + SearchNUTData("battery.charge") + "%"
                        + "\nInput Voltage: " + UPSInputVoltage + "v / " + UPSInputNominalVoltage
                        + "\nOutput Voltage: " + UPSOutputVoltage + "v"
                        + "\n\nUPS Beeper enabled: " + UPSBeeper
                    );
            }
            catch (Exception e)
            {
                UPSInfo = "No data has been received from the UPS.";
                if (NUT_Background.isDebug) UPSInfo = (UPSInfo + "\n" + e);
            }
            
            return UPSInfo;

        }

        public static string ParseUPSVariables()
        {
            if (UPSVariables == null || UPSVariables.GetLength(0) == 0) return "No data has been collected from the UPS yet.";

            string UPSFormattedVariables = "";
            try
            {
                for (int i = 0; i < UPSVariables.GetLength(0); i++)
                {
                    UPSFormattedVariables = (UPSFormattedVariables + UPSVariables[i, 0] + "\t" + UPSVariables[i, 1] + "\n");
                }
            } catch (Exception e)
            {
                NUT_Background.debugLog.Error("[PROCESSOR] Exception occurred:\n" + e);
                return ("Error: " + e);
            }
            return UPSFormattedVariables;

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
             * OL CHRG = Online, connected to AC power, charging
             */

            int UPSStatusCode = -1;

            string UPSStatusMessage = null;
            double  UPSBatteryRuntime = Convert.ToDouble(SearchNUTData("battery.runtime"));
            double UPSBatteryCharge = Convert.ToDouble(SearchNUTData("battery.charge"));
            string UPSStatus = "ERR";

            try
            {
                UPSStatus = SearchNUTData("ups.status");
            }
            catch
            {
                return Tuple.Create("No connection to UPS", (double)0, -1);
            }

            if (UPSStatus.Equals("OL"))
            {
                UPSStatusCode = 0; // GREEN - All OK

                if (UPSBatteryRuntime <= 60)
                {
                    UPSStatusMessage = (Math.Round(UPSBatteryRuntime, 0) + " seconds"); // Only display in seconds, since it's exactly a minute (or less)
                }
                else
                {
                    UPSBatteryRuntime = Math.Round((UPSBatteryRuntime / 60), 0);
                    if (UPSBatteryRuntime == 1) UPSStatusMessage = (UPSBatteryRuntime + " minute");
                    else UPSStatusMessage = (UPSBatteryRuntime + " minutes"); // Breaks it down into minutes
                }
            }
            else if (UPSStatus.Equals("OB DISCHRG"))
            {
                UPSStatusCode = 1;
                if (UPSBatteryRuntime <= 60)
                {
                    UPSStatusMessage = (Math.Round(UPSBatteryRuntime, 0) + " seconds remaining"); // Only display in seconds, since it's exactly a minute (or less)
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
                    UPSStatusMessage = (Math.Round(UPSBatteryRuntime, 0) + " sec remaining"); // Only display in seconds, since it's exactly a minute (or less)
                }
                else
                {
                    UPSBatteryRuntime = Math.Round((UPSBatteryRuntime / 60), 0);
                    UPSStatusMessage = (Math.Round(UPSBatteryRuntime, 0) + " min remaining"); // Breaks it down into minutes
                }
            }

            return Tuple.Create(UPSStatusMessage, UPSBatteryCharge, UPSStatusCode);
        }

        public static Tuple<string, int, double> ChargeStatus()
        {
            Tuple<string, double, int> BatteryStatus = GetBatteryStatus();
            string StatusMessage = "";

            if (BatteryStatus.Item3 == 0) StatusMessage = ("On AC Power, " + BatteryStatus.Item1);
            else if (BatteryStatus.Item3 == 1) StatusMessage = ("On Battery Power, " + BatteryStatus.Item1);
            else if (BatteryStatus.Item3 == 2) StatusMessage = ("Charging, " + BatteryStatus.Item1);
            else if (BatteryStatus.Item3 == 3) StatusMessage = ("Not connected to UPS");
            
            return Tuple.Create(StatusMessage,BatteryStatus.Item3,BatteryStatus.Item2);
        }

        public static string SearchNUTData(string NUTVariable)
        {
            if (UPSVariables == null || UPSVariables.GetLength(0) == 0)
            {
                return "";
            }
                NUT_Background.debugLog.Trace("[PROCESSOR:SEARCH] Searching for " + NUTVariable);
            for (int i = 0; i < UPSVariables.GetLength(0); i++)
            {
                if (UPSVariables[i, 0].Contains(NUTVariable))
                {
                    return UPSVariables[i, 1];
                }
            }

            NUT_Background.debugLog.Debug("[PROCESSOR:SEARCHNUTDATA] Could not find requested variable");
            return "";
        }

        public static void ModifySimNUTData(string NUTVariable, string NUTValue)
        {
            NUT_Background.debugLog.Debug("[MODNUTDATA] Array length is " + Convert.ToString(UPSVariables.GetLength(0)));
            NUT_Background.debugLog.Debug("[MODNUTDATA] Invoked. Searching for: " + NUTVariable);
            for (int i = 0; i < UPSVariables.GetLength(0); i++)
            {
                if (UPSVariables[i, 0].Equals(NUTVariable))
                {
                    NUT_Background.debugLog.Debug("[SEARCHNUTDATA] Searched for " + NUTVariable + " and got: " + UPSVariables[i, 1]);
                    UPSVariables[i, 1] = NUTValue;
                    return;
                }
            }

            NUT_Background.debugLog.Debug("[SEARCHNUTDATA] Could not find requested variable");
            return;
        }

    }
}
