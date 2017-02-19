using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;


namespace NUTty_UPS_Client
{
    
    public partial class frmSettings : Form
    {
        public bool isPolled = false;
        public bool isPollingUPS = false;

        public frmSettings()
        {
            InitializeComponent();
            _frmSettings = this;

        }

        public void TimerTrigger()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(UPSPoll));
            }
            else
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
            if(ValidateIPAddress() && ValidatePort())
            {
                UPSPoll();
            }



            if(!isPolled)
            {
                return; // Was not able to retrieve data from the NUT server, so aborting here.
            }

            // If information was successfully retrieved, we know that we can communicare with NUT server
            pnlSettings.Enabled = true;
            pnlAlarms.Enabled = true;

        }


        private void frmSettings_Load(object sender, EventArgs e)
        {
            if (Backend.Background.isSimulated) {
                chkSimulate.Checked = true;
            }
            else
            {
                chkSimulate.Checked = false;
            }

            Tuple<IPAddress, UInt16, UInt32> NUTConnectionSettings = Backend.NUT_Config.GetConnectionSettings();
            if (NUTConnectionSettings.Item1 != IPAddress.Parse("127.0.0.1"))
            {
                txtIPAddress.Text = NUTConnectionSettings.Item1.ToString();
            }

            if (NUTConnectionSettings.Item2 != 0)
            {
                txtPort.Text = NUTConnectionSettings.Item2.ToString();
            }

            if (NUTConnectionSettings.Item3 != 0)
            {
                txtPollFrequency.Text = NUTConnectionSettings.Item3.ToString();
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if(ValidateIPAddress())
            {
                Backend.NUT_Config.SetConfig("IP Address", txtIPAddress.Text);
            }

            if(ValidatePort())
            {
                Backend.NUT_Config.SetConfig("Port", txtPort.Text);
            }
            if(ValidatePollInterval())
            {
                Backend.NUT_Config.SetConfig("Poll Interval", txtPollFrequency.Text);
            }

        }

        private bool ValidateIPAddress()
        {
            IPAddress NUTIPAddress;
            if (IPAddress.TryParse(txtIPAddress.Text, out NUTIPAddress))
            {
                return true;
            }
            else
            {
                MessageBox.Show("The IP address entered is incorrect");
                return false;
            }
        }

        private bool ValidatePort()
        {
            UInt16 NUTPort;
            if(UInt16.TryParse(txtPort.Text, out NUTPort))
            {
                return true;
            }
            else
            {
                MessageBox.Show("The port number entered is incorrect");
                return false;
            }
        }

        private bool ValidatePollInterval()
        {
            UInt32 NUTPollInterval;
            if(UInt32.TryParse(txtPollFrequency.Text, out NUTPollInterval))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void chkSimulate_CheckedChanged(object sender, EventArgs e)
        {
            if(chkSimulate.Checked) {
                Backend.NUT_Config.SetConfig("Simulate", "true");
                txtIPAddress.Enabled = false;
                txtPort.Enabled = false;
            } else
            {
                Backend.NUT_Config.SetConfig("Simulate", "false");
                txtIPAddress.Enabled = true;
                txtPort.Enabled = true;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chkNotification_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }

    
}
