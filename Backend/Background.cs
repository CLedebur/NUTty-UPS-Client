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
        // Background thread worker
        private System.ComponentModel.BackgroundWorker BGUPSPolling;
        
        // Experimenting with timers
        System.Timers.Timer UPSPollTimer;
        System.Threading.Timer UPSPollTimer2 = null;

        public static int UPSPollingInterval = 5000;
        public static bool isSimulated = false;
        public bool isPollingUPS = false;
        public static Tuple<IPAddress, UInt16, UInt32> NUTConnectionSettings;

        public Background()
        {
            

        }
        public static void WriteNUTLog(string strOutput)
        {
            Console.WriteLine("[BACKGROUND] " + strOutput);
        }

        void OnTimedEvent(Object sender, ElapsedEventArgs e)
        {
            GC.KeepAlive(UPSPollTimer);
            WriteNUTLog("[TIMER] Triggered");
            if (isPollingUPS)
            {
                // Runtime, charge and status code
                WriteNUTLog("Polling UPS");
                Tuple<string, double, int> UPSBatteryStatus = NUT_Processor.GetBatteryStatus();
            }
            
        }

        private void InitializeBackgroundWorker()
        {
            BGUPSPolling.DoWork += new System.ComponentModel.DoWorkEventHandler(BGUPSPolling_DoWork);
        }

        private void BGUPSPolling_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

        }


        public void StartBackgroundProcess()
        {
            UPSPollTimer = new System.Timers.Timer(UPSPollingInterval);
            UPSPollTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            UPSPollTimer.AutoReset = true;
            UPSPollTimer.Enabled = true;
            UPSPollTimer.Start();

            //UPSPollTimer2 = new System.Threading.Timer(_ => WriteNUTLog("[TIMER2] Keeping Alive" ));
            //UPSPollTimer2.Change(0, UPSPollingInterval);

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
                    StartSettings();
                    NUTConnectionSettings = NUT_Config.GetConnectionSettings();
                }
            }
            catch
            {
                
            } finally
            {
                //StartSettings(); // Temporary - as the timer won't work
                Thread.Sleep(10000);
                
            }

        }
        
        public static void StartSettings()
        {
            Application.Run(new frmSettings());
            new Thread(() => new frmSettings().Show()).Start();
            //frmSettings._frmSettings.Activate();
        }
    }
}
