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

namespace NUTty_UPS_Client.Backend
{
    class Background
    {
        System.Timers.Timer UPSPollTimer;
        public static int UPSPollingInterval = 5000;

        public static Tuple<IPAddress, UInt16, UInt32> NUTConnectionSettings;

        public Background()
        {
            //_Background = this;

        }
        public static void WriteNUTLog(string strOutput)
        {
            Console.WriteLine("[BACKGROUND] " + strOutput);
        }

        void OnTimedEvent(Object sender, ElapsedEventArgs e)
        {
            GC.KeepAlive(UPSPollTimer);
            WriteNUTLog("Polling UPS");

            // Runtime, charge and status code
            Tuple<string, double, int> UPSBatteryStatus = NUT_Processor.GetBatteryStatus();

            if(UPSBatteryStatus.Item2 <= 20)
            {
                MessageBox.Show("Warning - UPS battery level is now 20%");
            }

        }

        public void StartBackgroundProcess()
        {
            UPSPollTimer = new System.Timers.Timer(UPSPollingInterval);
            UPSPollTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            UPSPollTimer.AutoReset = true;
            //UPSPollTimer.Enabled = true;

            try
            {
                // Checking Registry for settings
                NUTConnectionSettings = NUT_Config.GetConnectionSettings();
                if (NUTConnectionSettings.Item1 == IPAddress.Parse("127.0.0.1") || NUTConnectionSettings.Item2 == 0 || NUTConnectionSettings.Item3 == 0)
                {
                    WriteNUTLog("Empty values found, starting Settings form");
                    StartSettings();
                    NUTConnectionSettings = NUT_Config.GetConnectionSettings();
                }
            }
            catch (Exception e)
            {
                
            } finally
            {
                
                //UPSPollTimer.Start();
            }

        }
        
        public static void StartSettings()
        {
            Application.Run(new frmSettings());
            frmSettings._frmSettings.Activate();

        }
    }
}
