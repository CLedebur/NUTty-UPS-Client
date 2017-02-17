using System;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace NUTty_UPS_Client
{
    public class NUT_poller
    {

        private static void UpdateTxtOutput(string strOutput)
        {
            Console.WriteLine(strOutput);
        }
        public static string PollNUTServer(string nutIP, int nutPort)
        {

            TelnetConnection nutServer = new TelnetConnection(nutIP, nutPort);
            string nutUPSStatus = "LIST VAR ups";

            UpdateTxtOutput("Connecting to NUT server " + nutIP + " at " + nutPort);

            if(nutServer.IsConnected)
            {
                UpdateTxtOutput("Connected to NUT server");
            }

            nutServer.WriteLine(nutUPSStatus);
            string nutOutput = nutServer.Read();

            return nutOutput;
            
        }
    }
}
