using System;
using Microsoft.Win32;
using System.Net;

namespace NUTty_UPS_Client.Backend
{
    class NUT_Config
    {
        public static bool SetConfig(string KeyName, string KeyValue, RegistryValueKind RegKind = RegistryValueKind.String)
        {
            try
            {
                RegistryKey RegKey;
                RegKey = Registry.CurrentUser.CreateSubKey("Software\\NUTty UPS Client");
                RegKey.SetValue(KeyName, KeyValue);
                RegKey.Close();
            }
            catch (Exception e)
            {
                Backend.Background.WriteNUTLog("[CONFIG] Could not set registry key: " + KeyName + " wtih value " + KeyValue + "\nException:" + e);
                return false;
            }
            Backend.Background.WriteNUTLog("[CONFIG] Set registry key " + KeyName + " with value " + KeyValue);
            return true;

        }

        public static string GetConfig(string KeyName)
        {
            Backend.Background.WriteNUTLog("[CONFIG] Checking for registry key: " + KeyName);
            RegistryKey RegKey = Registry.CurrentUser.OpenSubKey("Software\\NUTty UPS Client", false);

            object RegValue;
            try
            {
                RegValue = RegKey.GetValue(KeyName);
                Backend.Background.WriteNUTLog("[CONFIG] Got: " + RegValue.ToString());
                switch (RegKey.GetValueKind(KeyName))
                {
                    case RegistryValueKind.String:
                        return RegValue.ToString();
                    case RegistryValueKind.ExpandString:
                        Backend.Background.WriteNUTLog("[CONFIG] Got: " + RegValue);
                        break;
                    case RegistryValueKind.Binary:
                        foreach (byte b in (byte[])RegValue)
                        {
                            Console.Write("{0:x2} ", b);
                        }
                        break;
                    case RegistryValueKind.DWord:
                        Backend.Background.WriteNUTLog("[CONFIG] Got: " + Convert.ToString((Int32)RegValue));
                        break;
                    case RegistryValueKind.QWord:
                        Backend.Background.WriteNUTLog("[CONFIG] Got: " + Convert.ToString((Int64)RegValue));
                        break;
                    case RegistryValueKind.MultiString:
                        foreach (string s in (string[])RegValue)
                        {
                            Console.Write("[{0:s}], ", s);
                        }
                        break;
                    default:
                        Backend.Background.WriteNUTLog("[CONFIG] Got: Unknown");
                        break;
                }

            }
            catch (NullReferenceException)
            {
                Backend.Background.WriteNUTLog("[CONFIG] Registry key does not exist: " + KeyName);
                return null;
            }
            catch (Exception e) {
                Backend.Background.WriteNUTLog("[CONFIG] Failed to read registry key: " + e);
                return null;
            }

            return RegValue.ToString();
        }

        public static Tuple<IPAddress, UInt16, UInt32> GetConnectionSettings()
        {
            string NUTServerIP = GetConfig("IP Address");
            string NUTServerPort = GetConfig("Port");
            string NUTPollInterval = GetConfig("Poll Interval");

            if (NUTServerIP == null)
            {
                NUTServerIP = "127.0.0.1";
            }

            return Tuple.Create(IPAddress.Parse(NUTServerIP), Convert.ToUInt16(NUTServerPort), Convert.ToUInt32(NUTPollInterval));

        }
    }
}
