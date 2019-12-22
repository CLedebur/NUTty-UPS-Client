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

namespace nuttyupsclient.Backend
{


    public class NUT_Poller
    {
        
        private static string NUTOutput { get => NUTOutput; set => NUTOutput = value; }

        private static async Task TelnetClient(string nutIP, UInt16 nutPort)
        {
            MainPage.debugLog.Debug("[POLLER] Connecting to NUT server " + nutIP + " at " + nutPort);
            using (var Client = new Client(nutIP, nutPort, new CancellationToken()))
            {
                while (true)
                {
                    // Gets the NUT server to return the list of UPS variables
                    await Client.WriteLine("LIST VAR ups");

                    var s = await Client.TerminatedReadAsync("END LIST VAR ups");

                    MainPage.debugLog.Debug("[POLLER] NUT server returned:\r\n" + s.ToString());

                    NUTOutput = s;
                    return;
                }
            }

        }

        public static Tuple<string, bool> PollNUTServer(String nutIP, UInt16 nutPort)
        {
            if (Background.isSimulated)
            {
                // If simulation is enabled, then it will receive data from the simulator instead of the UPS
                // It will simulate the CyberPower UPS for now
                return SimulateNUTServer();
            }
            
            bool isSuccessful = false;

            Task NUTConnection = Task.Factory.StartNew(async () =>
           {
               MainPage.debugLog.Debug("[POLLER] Executing telnet client task");
               await TelnetClient(nutIP, nutPort);
           });

            while (!NUTConnection.IsCompleted)
            {
                MainPage.debugLog.Trace("[POLLER] Waiting for task to complete. Waiting 500ms.");
                Thread.Sleep(500);
            }
            NUTConnection.Dispose();

            return Tuple.Create(NUTOutput, isSuccessful);
            
        }
        
        public static Tuple<string, bool> SimulateNUTServer()
        {
            TextReader SimFile = new StreamReader(@"Assets\Simulated\CraigUPS.txt");
            string SimFileContents = SimFile.ReadToEnd();

            return Tuple.Create(SimFileContents, true);

        }
    }
}
