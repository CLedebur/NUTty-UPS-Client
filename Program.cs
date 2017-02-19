using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NUTty_UPS_Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Backend.Background BGProcess = new Backend.Background();
            BGProcess.StartBackgroundProcess();            

            //Application.Run(new frmSettings());
            //frmSettings._frmSettings.Activate();
        }


    }
}
