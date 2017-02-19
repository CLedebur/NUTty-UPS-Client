
namespace NUTty_UPS_Client.Backend
{
    class Registry
    {
        public static void SetRegistryKey(string KeyName, string KeyValue)
        {
            Microsoft.Win32.RegistryKey RegKey;
            RegKey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("NUTty UPS Client");
            RegKey.SetValue(KeyName, KeyValue);
            RegKey.Close();
        }
    }
}
