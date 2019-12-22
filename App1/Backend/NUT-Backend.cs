using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Net;
using System.ComponentModel;

namespace nuttyupsclient.Backend
{
    class Background
    {
        public static bool isSimulated = false;
        public static Tuple<IPAddress, UInt16, UInt32> NUTConnectionSettings;
        public static bool isLogging = false;

        public static void InitializeBg()
        {

            string DebugLogging = Backend.NUT_Config.GetConfig("Debug");
            if (DebugLogging == null)
                MainPage.debugLog.Info("[BACKEND] No registry entry exists for debug logging");
            else if (DebugLogging.Equals("true"))
                Backend.Background.isLogging = true;

            MainPage.debugLog.Info("[INITIALIZE] Started");

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
                    MainPage.debugLog.Info("[BACKEND] Empty values found, starting Settings form");
                    return;
                }
            }
            catch (Exception e)
            {
                MainPage.debugLog.Fatal("[BACKEND] Error occurred: " + e);
            }
         

        }
        
    }
}
