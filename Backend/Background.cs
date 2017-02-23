using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Net;
using System.Windows.Forms;
using System.ComponentModel;

namespace NUTty_UPS_Client.Backend
{
    class Background
    {
        public static bool isSimulated = false;
        public static Tuple<IPAddress, UInt16, UInt32> NUTConnectionSettings;

        public static void WriteNUTLog(string strOutput)
        {
            Console.WriteLine("[BACKGROUND] " + strOutput);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Backend.Background BGProcess = new Backend.Background();

            BGProcess.InitializeBg();
        }

        public void InitializeBg()
        {
            WriteNUTLog("[INITIALIZE] Started");

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
                    WriteNUTLog("Empty values found, starting Settings form");
                    return;
                }
            }
            catch (Exception e)
            {
                WriteNUTLog("[BACKGROUND] Error occurred: " + e);
            }
            finally
            {
                Application.Run(new frmSettings());
            }
        }
        
    }
}
