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
        public static frmSettings _frmSettings;

        public bool isPolled = false;
        public bool isPollingUPS = false;

        public frmSettings()
        {
            InitializeComponent();
            _frmSettings = this;
        }

        private void UPSPoll()
        {
            // Pulls latest data from UPSVariables
            Int16 nutPort = Convert.ToInt16(txtPort.Text);

            // Determines whether it is in simulation mode (isSimulated) and whether it has already been polled
            if (Backend.Background.isSimulated && isPolled)
            {
                // I have a feeling I'm eventually going to need to put something here.
            }
            else
            { 
                // If simulation mode is not active, it will poll the NUT server
                try
                {
                    Tuple<string, bool> TupNutOutput = NUT_Poller.PollNUTServer(txtIPAddress.Text, nutPort);
                    isPolled = TupNutOutput.Item2;
                    string nutOutput = TupNutOutput.Item1;
                    NUT_Processor.ParseNUTOutput(nutOutput);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Exception occured: " + e);
                }
            }

            // Error condition, unable to get data from NUT server for a variety of reasons
            if (!isPolled)
            {
                Console.WriteLine("Did not get data successfully");
                MessageBox.Show("Was unable to retrieve data from NUT sever. Got an \"Access Denied\" message.\n\nA common cause of this is the IP address of this client not being whitelisted on the NUT server.");
                return;
            }

            // Refreshes the data on the form
            lblUPSModel.Text = NUT_Processor.UPSStatistics();
            Tuple<string, double, int> UPSBatteryStatus = NUT_Processor.GetBatteryStatus();
            this.UpdateUPSStatus(UPSBatteryStatus.Item1, UPSBatteryStatus.Item3);
            this.updateUPSModelLabel(NUT_Processor.UPSStatistics());

        }


        public void WriteNUTLog(string strOutput)
        {
            Console.WriteLine(strOutput);
        }

        public void UpdateUPSStatus(string UPSStatusMessage, int UPSStatusCode)
        {
            // Updates the UPS status and runtime on the bottom of the form
            if (UPSStatusCode == 0)
            {
                lblUPSStatus.Text = ("On AC Power - " + UPSStatusMessage);
                lblUPSStatus.ForeColor = System.Drawing.Color.Green;
            } else if (UPSStatusCode == 1)
            {
                lblUPSStatus.Text = ("On Battery Power - " + UPSStatusMessage);
                lblUPSStatus.ForeColor = System.Drawing.Color.Orange;
            } else if (UPSStatusCode == -1)
            {
                lblUPSStatus.Text = ("No data");
                lblUPSStatus.ForeColor = System.Drawing.Color.Black;
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

            if (!isPolled)
            {
                return; // Was not able to retrieve data from the NUT server, so aborting here.
            }

            // If information was successfully retrieved, we know that we can communicare with NUT server
            pnlSettings.Enabled = true;
            pnlAlarms.Enabled = true;
        }


        private void frmSettings_Load(object sender, EventArgs e)
        {
            // Checks to see if we are in a simulation environment
            if (Backend.Background.isSimulated) {
                chkSimulate.Checked = true;
                UPSPoll();
                SimulatorPopulate();
            }
            else
            {
                // Narrows the form and hides the simulator panel
                frmSettings._frmSettings.Width = 483;
                pnlSimulator.Visible = false;
                chkSimulate.Checked = false;
            }

            // Checks settings in the registry and fills in the fields accordingly
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

            btnApply.Enabled = false;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {

            if (chkSimulate.Checked)
            {
                frmSettings._frmSettings.Width = 686;
                pnlSimulator.Visible = true;
                lblUPSModel.Text = "Retrieving data...";
                txtIPAddress.Enabled = false;
                txtPort.Enabled = false;
                _frmSettings.Refresh(); // Just to reduce the visual "lag"

                Backend.Background.isSimulated = true;
                Backend.NUT_Config.SetConfig("Simulate", "true");

                isPolled = false;
                UPSPoll();
                SimulatorPopulate();

            }
            else
            {
                frmSettings._frmSettings.Width = 483;
                pnlSimulator.Visible = false;
                Backend.Background.isSimulated = false;
                Backend.NUT_Config.SetConfig("Simulate", "false");
                txtIPAddress.Enabled = true;
                txtPort.Enabled = true;
                lblUPSModel.Text = "Not connected";
                lblUPSStatus.Text = "";
                isPolled = false;

                if (ValidateIPAddress())
                {
                    Backend.NUT_Config.SetConfig("IP Address", txtIPAddress.Text);
                }

                if (ValidatePort())
                {
                    Backend.NUT_Config.SetConfig("Port", txtPort.Text);
                }

                if (ValidatePollInterval())
                {
                    Backend.NUT_Config.SetConfig("Poll Interval", txtPollFrequency.Text);
                }

            }
            btnApply.Enabled = false;

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
            btnApply.Enabled = true;
        }

        private void SimulatorPopulate()
        {
            txtSimBatteryCharge.Text = NUT_Processor.SearchNUTData("battery.charge");
            txtSimBatteryChargeLow.Text = NUT_Processor.SearchNUTData("battery.charge.low");
            txtSimBatteryChargeWarn.Text = NUT_Processor.SearchNUTData("battery.charge.warning");
            txtSimBatteryRuntime.Text = NUT_Processor.SearchNUTData("battery.runtime");
            txtSimBatteryRuntimeLow.Text = NUT_Processor.SearchNUTData("battery.runtime.low");
            txtSimBatteryVoltage.Text = NUT_Processor.SearchNUTData("battery.voltage");
            txtSimBatteryVoltageNominal.Text = NUT_Processor.SearchNUTData("battery.voltage.nominal");
            txtSimInputVoltage.Text = NUT_Processor.SearchNUTData("input.voltage");
            txtSimInputVoltageNominal.Text = NUT_Processor.SearchNUTData("input.voltage.nominal");
            txtSimOutputVoltage.Text = NUT_Processor.SearchNUTData("output.voltage");
            cmbSimUPSStatus.Text = NUT_Processor.SearchNUTData("ups.status");
            lblUPSModel.Text = NUT_Processor.UPSStatistics();
        }

        private void btnSimApply_Click(object sender, EventArgs e)
        {
            NUT_Processor.ModifySimNUTData("battery.charge", txtSimBatteryCharge.Text);
            NUT_Processor.ModifySimNUTData("battery.charge.low", txtSimBatteryChargeLow.Text);
            NUT_Processor.ModifySimNUTData("battery.charge.warning", txtSimBatteryChargeWarn.Text);
            NUT_Processor.ModifySimNUTData("battery.runtime", txtSimBatteryRuntime.Text);
            NUT_Processor.ModifySimNUTData("battery.runtime.low", txtSimBatteryRuntimeLow.Text);
            NUT_Processor.ModifySimNUTData("battery.voltage", txtSimBatteryVoltage.Text);
            NUT_Processor.ModifySimNUTData("battery.voltage.nominal", txtSimBatteryVoltageNominal.Text);
            NUT_Processor.ModifySimNUTData("input.voltage", txtSimInputVoltage.Text);
            NUT_Processor.ModifySimNUTData("input.voltage.nominal", txtSimInputVoltageNominal.Text);
            NUT_Processor.ModifySimNUTData("output.voltage", txtSimOutputVoltage.Text);
            NUT_Processor.ModifySimNUTData("ups.status", cmbSimUPSStatus.Text);

            UPSPoll();
        }

        private void txtIPAddress_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void txtPort_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void txtPollFrequency_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }
    }

    
}
