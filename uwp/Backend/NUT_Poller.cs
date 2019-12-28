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

        private static async Task<string> TelnetClient(string nutIP, UInt16 nutPort)
        {
            NUT_Background.PollCount++;
            NUT_Background.debugLog.Trace("[POLLER] Connecting to NUT server " + nutIP + " at " + nutPort);
            using (var Client = new Client(nutIP, nutPort, new CancellationToken()))
            {
                while (true)
                {
                    // Gets the NUT server to return the list of UPS variables
                    await Client.WriteLine("LIST VAR ups");

                    var s = await Client.TerminatedReadAsync("END LIST VAR ups");

                    NUT_Background.debugLog.Trace("[POLLER] NUT server returned:\r\n" + s.ToString());

                    return s;
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
            if (!NUT_Background.NeedConfig)
            {
                PollUPS.Enabled = true;

                PollNUTServer(NUT_Background.NUTConnectionSettings.Item1, NUT_Background.NUTConnectionSettings.Item2);
            }
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
            // Will only poll if configuration is not needed
            if (!NUT_Background.NeedConfig && NUT_Background.isPolling)
            {
                PollUPS.Enabled = false;
                PollNUTServer(NUT_Background.NUTConnectionSettings.Item1, NUT_Background.NUTConnectionSettings.Item2);
                PollUPS.Enabled = true;
            }
        }

        #endregion

        public static void PollNUTServer(string nutIP, ushort nutPort)
        {
            string s = null;

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
               s = await TelnetClient(nutIP, nutPort);
           });

            while (!NUTConnection.IsCompleted)
            {
                NUT_Background.debugLog.Trace("[POLLER] Waiting for task to complete. Waiting 500ms.");
                Thread.Sleep(500);
            }
            NUTConnection.Dispose();

            NUT_Background.isPolling = true;
            NUT_Processor.ParseNUTOutput(s); // Pipes to the parser so that the new data can be processed
            return;
        }

        public static async Task<bool> ValidateNUTServer(String nutIP, UInt16 nutPort)
        {
            string s = null;
            Task NUTConnection = Task.Factory.StartNew(async () =>
            {
                NUT_Background.debugLog.Trace("[POLLER:VALIDATE] Executing telnet client task");
                s = await TelnetClient(nutIP, nutPort);
            });

            while (!NUTConnection.IsCompleted)
            {
                NUT_Background.debugLog.Trace("[POLLER:VALIDATE] Waiting for task to complete. Waiting 500ms.");
                Thread.Sleep(500);
            }
            NUTConnection.Dispose();
            NUT_Background.debugLog.Info("ValidateNUTServer task " + NUTConnection.Status.ToString());

            Tuple<List<string>, bool> NUTValidation = NUT_Processor.ValidateNUTOutput(s);

            return NUTValidation.Item2;
            

        }

        public static string SimulateNUTServer()
        {
            TextReader SimFile = new StreamReader(@"Assets\Simulated\CraigUPS.txt");
            string SimFileContents = SimFile.ReadToEnd();

            return SimFileContents;

        }
    }
}
