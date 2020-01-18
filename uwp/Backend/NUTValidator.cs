using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace nuttyupsclient.Backend
{
    class NUTValidator
    {
        public bool ValidateIPAddress(string TestIP)
        {
            IPAddress NUTIPAddress;
            if (IPAddress.TryParse(TestIP, out NUTIPAddress))
            {
                return true;
            }
            return false;
        }

        public bool ValidatePort(string TestPort)
        {
            ushort NUTPort;
            if (ushort.TryParse(TestPort, out NUTPort))
            {
                return true;
            }
            return false;
        }

        public bool ValidatePollInterval(string TestPollInterval)
        {
            ulong NUTPollInterval;
            if (ulong.TryParse(TestPollInterval, out NUTPollInterval))
            {
                return true;
            }
            return false;
        }

    }
}
