using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Sockets;
using System.Net;
using PrimS.Telnet;

namespace nuttyupsclient.Backend
{
    public class NUT_Telnet
    {

        protected string TelnetClient(IPAddress nutIP, UInt16 nutPort)
        {
            MainPage.debugLog.Debug("[POLLER] Connecting to NUT server " + nutIP + " at " + nutPort);
            using (var Client = new Client("192.168.253.6", 3493, new CancellationToken()))
            {
                while (true)
                {
                    // Gets the NUT server to return the list of UPS variables
                    Client.WriteLine("LIST VAR ups");

                    string s = Client.TerminatedReadAsync("END LIST VAR ups").ToString();

                    return s;
                }
            }

        }



    }
}
