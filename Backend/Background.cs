using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Timers;
using System.Threading;
using System.Net;
using System.Windows.Forms;
using System.ComponentModel;

namespace NUTty_UPS_Client.Backend
{
    class Background
    {
        System.Timers.Timer UPSPollTimer;

        public static int UPSPollingInterval = 5000;
        public static bool isSimulated = false;
        public bool isPollingUPS = false;
        public static Tuple<IPAddress, UInt16, UInt32> NUTConnectionSettings;

        public static void WriteNUTLog(string strOutput)
        {
            Console.WriteLine("[BACKGROUND] " + strOutput);
        }

        void OnTimedEvent(Object sender, ElapsedEventArgs e)
        {
            WriteNUTLog("[TIMER] Triggered");
            if (isPollingUPS)
            {
                // Runtime, charge and status code
                WriteNUTLog("Polling UPS");
                Tuple<string, double, int> UPSBatteryStatus = NUT_Processor.GetBatteryStatus();
            }
            
        }

        public void InitializeBg()
        {
            UPSPollTimer = new System.Timers.Timer(UPSPollingInterval);
            UPSPollTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            UPSPollTimer.AutoReset = true;
            UPSPollTimer.Enabled = true;
            UPSPollTimer.Start();

            // Settings for BGWorker here
            WriteNUTLog("[INITIALIZE] Started");

            StartBackgroundProcess();
        }

        private void StartBackgroundProcess()
        {

            try
            {
                isSimulated = Convert.ToBoolean(NUT_Config.GetConfig("Simulate"));
            }
            catch
            {
                isSimulated = false;
            }

            try
            {
                // Checking Registry for settings
                NUTConnectionSettings = NUT_Config.GetConnectionSettings();
                if (NUTConnectionSettings.Item1 == IPAddress.Parse("127.0.0.1") || NUTConnectionSettings.Item2 == 0 || NUTConnectionSettings.Item3 == 0)
                {
                    WriteNUTLog("Empty values found, starting Settings form");
                    Application.Run(new frmSettings());
                    NUTConnectionSettings = NUT_Config.GetConnectionSettings();
                    return;
                }
            }
            catch
            {
                
            } finally
            {
                
            }
            Application.Run(new frmSettings());
        }
        
        public static void StartSettings()
        {
            Application.Run(new frmSettings());
            
        }
    }
}
