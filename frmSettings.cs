using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net;
using System.Timers;

namespace NUTty_UPS_Client
{
    
    public partial class frmSettings : Form
    {
        public static frmSettings _frmSettings;
        public bool isPolled = false;
        public static bool isPollingUPS = false;

        System.Timers.Timer UPSPollTimer; // Timer to poll the NUT server every UPSPollingInterval ms, default 5000 ms
        private double SimUPSDecayRate = 1;

        void OnTimedEvent(Object sender, ElapsedEventArgs e)
        {
            if (isPollingUPS)
            {
                // Runtime, charge and status code
                WriteNUTLog("[TIMER] Polling UPS");

                new Thread(delegate ()
                {
                    UPSPoll();
                } ).Start();
            }
        }

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
                isPollingUPS = false;
                return;
            } else
            {
                isPollingUPS = true;
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
            isPollingUPS = true;
            
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {

            UPSPollTimer = new System.Timers.Timer(Backend.Background.UPSPollingInterval);
            UPSPollTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            UPSPollTimer.AutoReset = true;
            UPSPollTimer.Enabled = true;
            UPSPollTimer.Start();

            // Checks to see if we are in a simulation environment
            if (Backend.Background.isSimulated) {
                chkSimulate.Checked = true;
                SimulatorPopulate();
            }
            else
            {
                // Narrows the form and hides the simulator panel
                frmSettings._frmSettings.Width = 483;
                pnlSimulator.Visible = false;
                chkSimulate.Checked = false;
            }

            
            // Constructing notify tray icon
            ntfUPSTray = new NotifyIcon(this.components);
            ntfUPSTray.Visible = true;
            ntfUPSTray.DoubleClick += new System.EventHandler(this.ntfUPSTray_DoubleClick);
            

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

            try
            {
                txtPollFrequency.Text = Backend.NUT_Config.GetConfig("Poll Interval");
                cmbBatteryPercentage.Text = Backend.NUT_Config.GetConfig("Warn Threshold");
                cmbAlarmAction.Text = Backend.NUT_Config.GetConfig("Threshold Action");
                chkNotification.Checked = Convert.ToBoolean(Backend.NUT_Config.GetConfig("Notification"));
                chkAlarm.Checked = Convert.ToBoolean(Backend.NUT_Config.GetConfig("Alarm"));
                chkDebugLogging.Checked = Convert.ToBoolean(Backend.NUT_Config.GetConfig("Debug"));
            }
            catch
            {

            }

            if (chkNotification.Checked)
            {
                Backend.NUT_Config.SetConfig("Notification", "true");
            }
            else
            {
                Backend.NUT_Config.SetConfig("Notification", "false");
            }
            // Alarm
            if (chkAlarm.Checked)
            {
                Backend.NUT_Config.SetConfig("Alarm", "true");
            }
            else
            {
                Backend.NUT_Config.SetConfig("Alarm", "false");
            }
            // Debug Logging
            if (chkDebugLogging.Checked)
            {
                Backend.NUT_Config.SetConfig("Debug", "true");
            }
            else
            {
                Backend.NUT_Config.SetConfig("Debug", "false");
            }


            UPSPoll();
            if (isPollingUPS)
            {
                pnlSettings.Enabled = true;
                pnlAlarms.Enabled = true;
            }

            btnApply.Enabled = false;
        }

        private void ntfUPSTray_DoubleClick(object Sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void mnuNotifyExit_Click_1(object Sender, EventArgs e)
        {
            // Close the form, which closes the application.
            ntfUPSTray.Dispose();
            this.Close();
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

            // Poll interval (in ms)
            Backend.NUT_Config.SetConfig("Poll Interval", txtPollFrequency.Text);
            // Low battery threshold percentage
            Backend.NUT_Config.SetConfig("Warn Threshold", cmbBatteryPercentage.Text);
            // Threshold action
            Backend.NUT_Config.SetConfig("Threshold action", cmbAlarmAction.Text);
            // Notifications
            if (chkNotification.Checked) {
                Backend.NUT_Config.SetConfig("Notification", "true");
            } else
            {
                Backend.NUT_Config.SetConfig("Notification", "false");
            }
            // Alarm
            if (chkAlarm.Checked)
            {
                Backend.NUT_Config.SetConfig("Alarm", "true");
            }
            else
            {
                Backend.NUT_Config.SetConfig("Alarm", "false");
            }
            // Debug Logging
            if (chkDebugLogging.Checked)
            {
                Backend.NUT_Config.SetConfig("Debug", "true");
            }
            else
            {
                Backend.NUT_Config.SetConfig("Debug", "false");
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

            SimUPSDecayRate = Convert.ToDouble(txtSimBatteryDecay.Text);

            UPSPoll();
        }

        private void SimulateBatteryDecay()
        {
            double BattValue = Convert.ToDouble(NUT_Processor.SearchNUTData("battery.charge"));
            Int32 BattRuntimeMax = Convert.ToInt32(txtSimBatteryRuntime.Text);
            Int32 BattRuntimeBreakdown = (BattRuntimeMax / 100);
            Int32 BattRuntime = Convert.ToInt32(NUT_Processor.SearchNUTData("battery.runtime"));

            // Since it would be impossible for a battery to have a negative charge, it will set the charge to 0
            if ((BattValue - SimUPSDecayRate) <= 0)
            {
                WriteNUTLog("[SIMULATOR] Battery decayed to empty");
                NUT_Processor.ModifySimNUTData("battery.charge", "0");
                NUT_Processor.ModifySimNUTData("battery.runtime", "0");
            }
            else
            {
                WriteNUTLog("[SIMULATOR] Battery decayed by " + SimUPSDecayRate + " from " + BattValue + " to " + (BattValue - SimUPSDecayRate));
                NUT_Processor.ModifySimNUTData("battery.charge", Convert.ToString(BattValue - SimUPSDecayRate));
                NUT_Processor.ModifySimNUTData("battery.runtime", Convert.ToString(BattRuntime - (BattRuntimeBreakdown * SimUPSDecayRate)));
            }

            // Refresh information 
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

        private void btnSimBatteryDecay_Click(object sender, EventArgs e)
        {
            SimulateBatteryDecay();
        }

        private void cmbSimUPSStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSimUPSStatus.Text.Equals("OL"))
            {
                txtSimBatteryDecay.Enabled = false;
            }
            else
            {
                txtSimBatteryDecay.Enabled = true;
            }
        }

        private void frmSettings_Resize(object sender, System.EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                frmSettings._frmSettings.Hide();
            }
        }

        private void mnuNotifySettings_Click(object sender, EventArgs e)
        {
            frmSettings._frmSettings.Show();
            _frmSettings.WindowState = FormWindowState.Normal;
        }

        private void cmbBatteryPercentage_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void cmbAlarmAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void chkNotification_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void chkAlarm_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

        private void chkDebugLogging_CheckedChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }
    }

}
