using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace nuttyupsclient.Backend
{
    class NUT_Validator
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
            UInt16 NUTPort;
            if (UInt16.TryParse(TestPort, out NUTPort))
            {
                return true;
            }
            return false;
        }

        public bool ValidatePollInterval(string TestPollInterval)
        {
            UInt64 NUTPollInterval;
            if (UInt64.TryParse(TestPollInterval, out NUTPollInterval))
            {
                return true;
            }
            return false;
        }

    }
}
