using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace NUTty_UPS_Client
{
    public partial class frmTray : Form
    {

        public static frmTray _frmTray;

        public frmTray()
        {
            InitializeComponent();
            _frmTray = this;
            //_frmTray.Location = new Point(Screen.GetWorkingArea( - _frmTray.Width, 50);
            InitializeUPSData();
        }

        private void frmTray_ResizeEnd(Object sender, EventArgs e)
        {
            lblBatteryPercentage.Top = (lblUPSConnection.Top + lblUPSConnection.Height + 12);
        }

        private void InitializeUPSData()
        {
            try
            {
                string NUTOutput = NUT_poller.PollNUTServer("192.168.253.6", 3493);
            }
            catch
            {
                lblUPSConnection.Text = "Not Connected";
                txtUPSStatistics.Text = "Unable to establish connection to NUT server";
                  
            }

            this.txtUPSStatistics.Text = NUT_Processor.ParseNUTOutput(NUT_poller.PollNUTServer("192.168.253.6", 3493));
            lblUPSConnection.Text = "Connected";
            Tuple<string, int, int> UPSBatteryStatus = NUT_Processor.GetBatteryStatus();
            lblTimeRemaining.Text = UPSBatteryStatus.Item1;
            lblBatteryPercentage.Text = Convert.ToString(UPSBatteryStatus.Item2) + "%";

            if (Convert.ToInt16(UPSBatteryStatus.Item3) == 0)
            {
                lblBatteryPercentage.ForeColor = Color.Green;
                lblTimeRemaining.ForeColor = Color.Green;
            }

        }

        private void frmTray_Load(object sender, EventArgs e)


        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void picSettings_Click(object sender, EventArgs e)
        {

            new Thread(new ThreadStart(showForm)).Start();

        }

        public void showForm()
        {

            
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
