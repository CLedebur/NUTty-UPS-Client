﻿using System;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;

namespace NUTty_UPS_Client
{
    public class NUT_Poller
    {

        private static void WriteNUTLog(string strOutput)
        {
            Console.WriteLine(strOutput);
        }
        public static Tuple<string, bool> PollNUTServer(string nutIP, int nutPort)
        {
            if (Backend.Background.isSimulated)
            {
                // If simulation is enabled, then it will receive data from the simulator instead of the UPS
                // It will simulate the CyberPower UPS for now
                return SimulateNUTServer();
            }

            bool isSuccessful = false;

            TelnetConnection nutServer = new TelnetConnection(nutIP, nutPort);
            string nutUPSStatus = "LIST VAR ups";

            WriteNUTLog("Connecting to NUT server " + nutIP + " at " + nutPort);

            if(nutServer.IsConnected)
            {
                WriteNUTLog("Connected to NUT server");
            }

            nutServer.WriteLine(nutUPSStatus);
            string nutOutput = nutServer.Read();
            //WriteNUTLog("[NUT Poller] Got data from server:\n" + nutOutput + "\n");

            if (nutOutput.Contains("ERR ACCESS-DENIED")) 
            {
                WriteNUTLog("[NUT Poller] Got ACCESS DENIED when trying to retrieve data");
            } else
            {
                isSuccessful = true;
            }

            return Tuple.Create(nutOutput, isSuccessful);
            
        }
        
        public static Tuple<string, bool> SimulateNUTServer()
        {
            TextReader SimFile = new StreamReader(@"Assets\Simulated\CraigUPS.txt");
            string SimFileContents = SimFile.ReadToEnd();

            return Tuple.Create(SimFileContents, true);

        }
    }
}
