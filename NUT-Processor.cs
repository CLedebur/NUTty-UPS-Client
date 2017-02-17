using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace NUTty_UPS_Client
{
    class NUT_Processor
    {
        public static string[,] UPSVariables;
        private static void WriteNUTLog(string strOutput)
        {
            try
            {
                frmSettings._frmSettings.updateTxtOutput(strOutput);
            } catch
            {
                Console.WriteLine(strOutput);
            }
        }

        public static string ParseNUTOutput(string nutOutput)
        {
            nutOutput = Regex.Replace(nutOutput, @"\r\n?|\n", Environment.NewLine); // Replaces UNIX linefeeds with ANSI

            WriteNUTLog("Attempting to sanitize output");
            List<string> nutList = new List<string>(nutOutput.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries));

            // Sanity check! 
            if (nutList[0].Contains("BEGIN LIST VAR ups") && nutList[nutList.Count - 1].Contains("END LIST VAR ups"))
            {
                WriteNUTLog("Data structure is correct. Let's continue.");
            }

            UPSVariables = new string[nutList.Count -1, 2];

            int j = 0;
            for (int i = 1; i < nutList.Count - 1; i++)
            {
                List<string> strUPSVarList = new List<string>(nutList[i].Split(new String[] { "\"" }, StringSplitOptions.RemoveEmptyEntries));

                List<string> strTemp = new List<string>(strUPSVarList[0].Split(new String[] { " " }, StringSplitOptions.RemoveEmptyEntries));

                UPSVariables[j, 0] = strTemp[strTemp.Count - 1].Trim(' '); // Removes trailing spaces
                UPSVariables[j, 1] = strUPSVarList[strUPSVarList.Count - 2]; // Only needs the data in between the quotes

                //UPSVariables[j, 0] = (strTemp[strTemp.Count - 1].Trim(' '));
                WriteNUTLog(UPSVariables[j, 0] + " is " + UPSVariables[j, 1]);
                j++;
            }

            string UPSStatusMessage = UPSStatistics();
            return UPSStatusMessage;

        }

        private static void ProcessNUTData()
        {
            // Battery charge level in percentage (0 - 100)
            WriteNUTLog("Battery charge: " + SearchNUTData("battery.charge"));            
        }

        public static string UPSStatistics()
        {
            // Input voltage and nominal voltage
            decimal UPSInputVoltage = Convert.ToDecimal(SearchNUTData("input.voltage"));
            decimal UPSInputNominalVoltage = Convert.ToDecimal(SearchNUTData("input.voltage.nominal"));

            // Battery voltage and nominal voltage
            decimal UPSBatteryVoltage = Convert.ToDecimal(SearchNUTData("battery.voltage"));
            decimal UPSBatteryNominalVoltage = Convert.ToDecimal(SearchNUTData("battery.voltage.nominal"));

            // Output voltage and nominal voltage
            decimal UPSOutputVoltage = Convert.ToDecimal(SearchNUTData("output.voltage"));
            UPSOutputVoltage = UPSOutputVoltage - UPSBatteryVoltage; // It adds the battery voltage to the output voltage. Fixing this.

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
                    + "\nBattery Voltage: " + UPSBatteryVoltage + "v / " + UPSBatteryNominalVoltage
                    + "\nOutput Voltage: " + UPSOutputVoltage + "v"
                    + "\n\nUPS Beeper enabled: " + UPSBeeper
                );

            return UPSInfo;

        }

        public static Tuple<string, int, int> GetBatteryStatus()
        {
            /* Status codes:
            0 = GREEN, UPS online and connected to AC
            1 = ORANGE, UPS online and running on battery
            2 = RED, UPS online and running on battery, critically low
            -1 = Invalid/no data */
            int UPSStatusCode = 0;

            string UPSStatusMessage = null;
            int UPSBatteryRuntime = Convert.ToInt16(SearchNUTData("battery.runtime"));
            int UPSBatteryCharge = Convert.ToInt16(SearchNUTData("battery.charge"));

            string UPSStatus = SearchNUTData("ups.status");
            if (UPSStatus.Equals("OL"))
            {
                UPSStatusCode = 0;
                UPSStatusMessage = ((UPSBatteryRuntime / 60) + " min");

            }
            else if (UPSStatus.Equals("OB DISCHRG"))
            {
                UPSStatusCode = 1;
                UPSStatusMessage = ((UPSBatteryRuntime / 60) + " min remaining");
            }

            return Tuple.Create(UPSStatusMessage, UPSBatteryCharge, UPSStatusCode);
        }
        private static string SearchNUTData(string NUTVariable)
        {

            for (int i = 0; i < UPSVariables.Length; i++)
            {
                if (UPSVariables[i,0].Equals(NUTVariable))
                {
                    return UPSVariables[i, 1];
                }
            }

            return "INVALID";
        }
    }
}
