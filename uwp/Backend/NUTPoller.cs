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
using Windows.System.Threading;

namespace nuttyupsclient.Backend
{

    public class NUTPoller : IDisposable
    {

        TextReader SimFile;

        private static async Task<string> TelnetClient(string nutIP, ushort nutPort)
        {
            NUTInitialization.PollCount++;
            NUTInitialization.debugLog.Trace("[POLLER] Connecting to NUT server " + nutIP + " at " + nutPort);
            using (var Client = new Client(nutIP, nutPort, new CancellationToken()))
            {
                while (true)
                {
                    // Gets the NUT server to return the list of UPS variables
                    await Client.WriteLine("LIST VAR ups").ConfigureAwait(true);

                    var s = await Client.TerminatedReadAsync("END LIST VAR ups").ConfigureAwait(true);

                    NUTInitialization.debugLog.Trace("[POLLER] NUT server returned:\r\n" + s.ToString());

                    return s;
                }
            }

        }

        #region Poll Timer

        ThreadPoolTimer PollUPS = ThreadPoolTimer.CreatePeriodicTimer((source) =>
            {

                NUTInitialization.debugLog.Trace("[POLLER:TIMER] Multithreaded timer fired");
                // Will only poll if configuration is not needed
                if (!NUTInitialization.NeedConfig && NUTInitialization.isPolling)
                {
                    //PollUPS.Enabled = false;
                    PollNUTServer(NUTInitialization.NUTConnectionSettings.Item1, NUTInitialization.NUTConnectionSettings.Item2);
                    //PollUPS.Enabled = true;
                }

            }, TimeSpan.FromMilliseconds(NUTInitialization.PollFrequency));

        public void InitializeUPSPolling()
        {
            //PollUPS();
        }

        public void PauseUPSPolling()
        {
            NUTInitialization.isPolling = false;
        }

        public void ResumeUPSPolling()
        {
            //PollUPS.Interval = NUTInitialization.PollFrequency;
            //PollUPS.Enabled = true;
        }

        void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            NUTInitialization.debugLog.Trace("[POLLER] Timer fired");
            // Will only poll if configuration is not needed
            if (!NUTInitialization.NeedConfig && NUTInitialization.isPolling)
            {
                //PollUPS.Enabled = false;
                PollNUTServer(NUTInitialization.NUTConnectionSettings.Item1, NUTInitialization.NUTConnectionSettings.Item2);
                //PollUPS.Enabled = true;
            }
        }

        #endregion

        public static void PollNUTServer(string nutIP, ushort nutPort)
        {
            string s = null;

            if (NUTInitialization.isSimulated)
            {
                // If simulation is enabled, then it will receive data from the simulator instead of the UPS

                // TODO: Format simulation files to adhere to UPS variable format
                //NUTProcessor.UPSVariables = Tuple.Create(SimulateNUTServer());
                return;
            }


            Task NUTConnection = Task.Run(async () =>
           {
               NUTInitialization.debugLog.Trace("[POLLER] Executing telnet client task");
               s = await TelnetClient(nutIP, nutPort).ConfigureAwait(true);
           });

            Task.WaitAll(NUTConnection);
            NUTConnection.Dispose();

            NUTInitialization.isPolling = true;
            NUTProcessor.ParseNUTOutput(s); // Pipes to the parser so that the new data can be processed
            return;
        }

        public static async Task<bool> ValidateNUTServer(string nutIP, ushort nutPort)
        {
            string s = null;
            Task NUTConnection = Task.Run(async () =>
            {
                NUTInitialization.debugLog.Trace("[POLLER:VALIDATE] Executing telnet client task");
                s = await TelnetClient(nutIP, nutPort).ConfigureAwait(true);
            });

            Task.WaitAll(NUTConnection);

            NUTConnection.Dispose();
            NUTInitialization.debugLog.Info("ValidateNUTServer task " + NUTConnection.Status.ToString());

            Tuple<List<string>, bool> NUTValidation = NUTProcessor.ValidateNUTOutput(s);

            return NUTValidation.Item2;
        }

        public string SimulateNUTServer()
        {
            SimFile = new StreamReader(@"Assets\Simulated\CraigUPS.txt");
            string SimFileContents = SimFile.ReadToEnd();
            SimFile.Dispose();

            return SimFileContents;
        }

        protected virtual void Dispose (bool disposing)
        {
            if(disposing)
            {
                SimFile.Close();
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
