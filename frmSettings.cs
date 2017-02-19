using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Timers;

namespace NUTty_UPS_Client
{

    public partial class frmSettings : Form
    {
        public bool isPolled = false;

        public bool isPollingUPS = false;
        const int UPSPollingInterval = 5000;
        private System.Timers.Timer UPSPollTimer;

        public frmSettings()
        {
            InitializeComponent();
            _frmSettings = this;

            UPSPollTimer = new System.Timers.Timer(UPSPollingInterval);
            UPSPollTimer.Elapsed += OnTimedEvent;
            UPSPollTimer.AutoReset = true;
            UPSPollTimer.Enabled = false;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (InvokeRequired) {
                Invoke(new MethodInvoker(UPSPoll));
            } else
            {
                if (isPollingUPS)
                {
                    UPSPoll();
                    if (!isPolled)
                    {
                        pnlSettings.Enabled = false;
                        pnlAlarms.Enabled = false;
                        lblUPSModel.Text = "Lost connection to UPS";
                    }
                }
            }

            return;
        }

        private void UPSPoll()
        {
            Int16 nutPort = Convert.ToInt16(txtPort.Text);
            try
            {
                Tuple<string, bool> TupNutOutput = NUT_poller.PollNUTServer(txtIPAddress.Text, nutPort);
                isPolled = TupNutOutput.Item2;
                string nutOutput = TupNutOutput.Item1;
                NUT_Processor.ParseNUTOutput(nutOutput);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception occured: " + e);
            }

            if (!isPolled)
            {
                Console.WriteLine("Did not get data successfully");
                MessageBox.Show("Was unable to retrieve data from NUT sever. Got an \"Access Denied\" message.\n\nA common cause of this is the IP address of this client not being whitelisted on the NUT server.");
                return;
            }
            else
            {
                lblUPSModel.Text = NUT_Processor.UPSStatistics();

                Tuple<string, double, int> UPSBatteryStatus = NUT_Processor.GetBatteryStatus();

                this.UpdateUPSStatus(UPSBatteryStatus.Item1, UPSBatteryStatus.Item3);
                this.updateUPSModelLabel(NUT_Processor.UPSStatistics());
            }
        }

        public static frmSettings _frmSettings;

        public void WriteNUTLog(string strOutput)
        {
            Console.WriteLine(strOutput);
        }

        public void UpdateUPSStatus(string UPSStatusMessage, int UPSStatusCode)
        {
            if (UPSStatusCode == 0)
            {
                lblUPSStatus.Text = ("On AC Power - " + UPSStatusMessage);
                lblUPSStatus.ForeColor = System.Drawing.Color.Green;
            } else if (UPSStatusCode == 1)
            {
                lblUPSStatus.Text = ("On Battery Power - " + UPSStatusMessage);
                lblUPSStatus.ForeColor = System.Drawing.Color.Orange;
            }
        }

        public void updateUPSModelLabel(string strOutput)
        {
            lblUPSModel.Text = strOutput;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            UPSPoll();

            if(!isPolled)
            {
                return; // Was not able to retrieve data from the NUT server, so aborting here.
            }

            // If information was successfully retrieved, we know that we can communicare with NUT server
            pnlSettings.Enabled = true;
            pnlAlarms.Enabled = true;

        }

        private void chkEnablePolling_CheckedChanged(object sender, EventArgs e)
        {
            if(chkEnablePolling.Checked = true)
            {
                UPSPollTimer.Enabled = true;
            } else
            {
                UPSPollTimer.Enabled = false;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    
}
