using System;
using System.Net;


namespace nuttyupsclient.Backend
{
    class NUT_Config
    {
        public void InitializeContainer() 
            {
            // Declares the application data container
           Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        }

        public static bool SetConfig(string KeyName, string KeyValue)
        {
            try
            {
                Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                Windows.Storage.ApplicationDataContainer container = localSettings.CreateContainer("NUTtyUPSClient", Windows.Storage.ApplicationDataCreateDisposition.Always);

                if (localSettings.Containers.ContainsKey(KeyName))
                {
                    localSettings.Containers["NUTtyUPSClient"].Values[KeyName] = KeyValue;
                }
            }
            catch (Exception e)
            {
                NUT_Background.debugLog.Error("[CONFIG] Could not save setting: " + KeyName + " with value " + KeyValue + "\nException:" + e);
                return false;
            }
            NUT_Background.debugLog.Trace("[CONFIG] Set registry key " + KeyName + " with value " + KeyValue);
            return true;
        }

        public static string GetConfig(string KeyName)
        {
            NUT_Background.debugLog.Trace("[CONFIG] Checking for existince of setting: " + KeyName);
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            Windows.Storage.ApplicationDataContainer container = localSettings.CreateContainer("NUTtyUPSClient", Windows.Storage.ApplicationDataCreateDisposition.Always);

            try
            {


            }
            catch (NullReferenceException)
            {
                NUT_Background.debugLog.Error("[CONFIG] Registry key does not exist: " + KeyName);
                return null;
            }
            catch (Exception e) {
                NUT_Background.debugLog.Error("[CONFIG] Failed to read registry key: " + e);
                return null;
            }

            return "test";
        }

        public static Tuple<String, UInt16, UInt32> GetConnectionSettings()
        {
            string NUTServerIP = GetConfig("IP Address");
            string NUTServerPort = GetConfig("Port");
            string NUTPollInterval = GetConfig("Poll Interval");

            if (NUTServerIP == null)
            {
                NUTServerIP = "127.0.0.1";
            }

            return Tuple.Create(NUTServerIP.ToString(), Convert.ToUInt16(NUTServerPort), Convert.ToUInt32(NUTPollInterval));

        }
    }
}
