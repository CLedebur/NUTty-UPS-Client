using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Net;
using System.ComponentModel;
using MetroLog;
using MetroLog.Targets;

namespace nuttyupsclient.Backend
{
    class NUT_Background
    {
        public static bool isSimulated = false;
        public static Tuple<String, UInt16, UInt32> NUTConnectionSettings;
        public static bool isLogging = false;
        public static bool isDebug = true;
        public static bool isPolling = false;
        public static bool NeedConfig = false;
        public static UInt64 PollFrequency = 5000;
        public static UInt64 PollCount = 0;
        public static ILogger debugLog = LogManagerFactory.DefaultLogManager.GetLogger<NUT_Background>();

        public static void InitializeBg()
        {
            debugLog.Info("[BACKGROUND] Attempting to load stored configuration");

            string DebugLogging = NUT_Config.GetConfig("Debug");
            if (DebugLogging == null)
                debugLog.Info("[BACKGROUND] No registry entry exists for debug logging");
            else if (DebugLogging.Equals("true"))
                isLogging = true;


            try
            {
                isSimulated = Convert.ToBoolean(NUT_Config.GetConfig("Simulate"));
            }
            catch
            {
                debugLog.Info("[BACKGROUND] No simulation setting found, so defaulting to collecting real data");
                isSimulated = false;
            }

            try
            {
                // Checking Registry for settings
                NUTConnectionSettings = NUT_Config.GetConnectionSettings();
                if (NUTConnectionSettings.Item1 == null || NUTConnectionSettings.Item2 == 0 || NUTConnectionSettings.Item3 == 0)
                {
                    debugLog.Info("[BACKGROUND] Empty values found, starting setup workflow");
                    NeedConfig = true;
                }
            }
            catch (Exception e)
            {
                debugLog.Fatal("[BACKGROUND] Error occurred: " + e);
                
            }
            NUTConnectionSettings = NUT_Config.GetConnectionSettings();
         

        }
        
    }
}
