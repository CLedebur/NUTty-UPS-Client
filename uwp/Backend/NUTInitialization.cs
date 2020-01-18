using MetroLog;
using System;

namespace nuttyupsclient.Backend
{
    class NUTInitialization
    {
        public static bool isSimulated = false;
        public static Tuple<string, ushort, uint> NUTConnectionSettings;
        public static bool isLogging = false;
        public static bool isDebug = true;
        public static bool isPolling = false;
        public static bool NeedConfig = false;
        public static ulong PollFrequency = 5000;
        public static ulong PollCount = 0;
        public static ILogger debugLog = LogManagerFactory.DefaultLogManager.GetLogger<NUTInitialization>();

        public static void InitializeBg()
        {
            debugLog.Info("[INITIALIZATION] Attempting to load stored configuration");

            string DebugLogging = NUTConfig.GetConfig("Debug");
            if (DebugLogging == null)
                debugLog.Info("[INITIALIZATION] No registry entry exists for debug logging");
            else if (DebugLogging.Equals("true"))
                isLogging = true;


            try
            {
                isSimulated = Convert.ToBoolean(NUTConfig.GetConfig("Simulate"));
            }
            catch
            {
                debugLog.Info("[INITIALIZATION] No simulation setting found, so defaulting to collecting real data");
                isSimulated = false;
            }

            try
            {
                // Checking Registry for settings
                NUTConnectionSettings = NUTConfig.GetConnectionSettings();
                if (NUTConnectionSettings.Item1 == null || NUTConnectionSettings.Item2 == 0 || NUTConnectionSettings.Item3 == 0)
                {
                    debugLog.Info("[INITIALIZATION] Empty values found, starting setup workflow");
                    NeedConfig = true;
                }
            }
            catch (Exception e)
            {
                debugLog.Fatal("[INITIALIZATION] Error occurred: " + e);
                
            }
            NUTConnectionSettings = NUTConfig.GetConnectionSettings();
         

        }
        
    }
}
