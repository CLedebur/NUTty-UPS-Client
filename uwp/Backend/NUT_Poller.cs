using System;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;
using PrimS.Telnet;
using Windows.Networking;
using System.Threading.Tasks;
using System.Timers;

namespace nuttyupsclient.Backend
{

    public class NUT_Poller
    {

        private static string NUTOutput;

        private static async Task TelnetClient(string nutIP, UInt16 nutPort)
        {
            NUT_Background.debugLog.Debug("[POLLER] Connecting to NUT server " + nutIP + " at " + nutPort);
            using (var Client = new Client(nutIP, nutPort, new CancellationToken()))
            {
                while (true)
                {
                    // Gets the NUT server to return the list of UPS variables
                    await Client.WriteLine("LIST VAR ups");

                    var s = await Client.TerminatedReadAsync("END LIST VAR ups");

                    NUT_Background.debugLog.Trace("[POLLER] NUT server returned:\r\n" + s.ToString());

                    NUTOutput = s;
                    return;
                }
            }

        }

        #region Poll Timer

        System.Timers.Timer PollUPS;
        public void InitializeUPSPolling()
        {
            PollUPS = new System.Timers.Timer(NUT_Background.PollFrequency);
            PollUPS.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            PollUPS.AutoReset = true;
            PollUPS.Enabled = true;
            PollNUTServer("192.168.253.6", 3493);
        }

        public void PauseUPSPolling()
        {
            PollUPS.Enabled = false;
            NUT_Background.isPolling = false;
        }

        public void ResumeUPSPolling()
        {
            PollUPS.Interval = NUT_Background.PollFrequency;
            PollUPS.Enabled = true;
        }

        void OnTimedEvent(Object sender, ElapsedEventArgs e)
        {
            NUT_Background.debugLog.Trace("[POLLER] Timer fired");
            // TODO: Make this respect server config
            PollUPS.Enabled = false;
            PollNUTServer("192.168.253.6", 3493);
            PollUPS.Enabled = true;
        }

        #endregion

        public static void PollNUTServer(String nutIP, UInt16 nutPort)
        {            

            if (NUT_Background.isSimulated)
            {
                // If simulation is enabled, then it will receive data from the simulator instead of the UPS

                // TODO: Format simulation files to adhere to UPS variable format
                //NUT_Processor.UPSVariables = Tuple.Create(SimulateNUTServer());
                return;
            }
            
            Task NUTConnection = Task.Factory.StartNew(async () =>
           {
               NUT_Background.debugLog.Trace("[POLLER] Executing telnet client task");
               await TelnetClient(nutIP, nutPort);
           });

            while (!NUTConnection.IsCompleted)
            {
                NUT_Background.debugLog.Trace("[POLLER] Waiting for task to complete. Waiting 500ms.");
                Thread.Sleep(500);
            }
            NUTConnection.Dispose();

            NUT_Background.isPolling = true;
            NUT_Processor.ParseNUTOutput(NUTOutput); // Pipes to the parser so that the new data can be processed
            return;
            
        }
        
        public static string SimulateNUTServer()
        {
            TextReader SimFile = new StreamReader(@"Assets\Simulated\CraigUPS.txt");
            string SimFileContents = SimFile.ReadToEnd();

            return SimFileContents;

        }
    }
}
