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
        private bool IsSimulatingDecay = false;
        private UInt16 AlarmPercentage;

        IPAddress NUTServer;
        UInt16 NUTPort;

        System.Timers.Timer UPSPollTimer; // Timer to poll the NUT server every UPSPollingInterval ms, default 5000 ms
        private double SimUPSDecayRate = 1;


        void OnTimedEvent(Object sender, ElapsedEventArgs e)
        {
            if (isPollingUPS)
            {
                // Runtime, charge and status code
                Backend.Background.WriteNUTLog("[TIMER] Polling UPS");
                UPSPoll();
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
                // Checks to see if this is the first time this program has been run
                string TempValue = Backend.NUT_Config.GetConfig("IP Address");
                if (TempValue == null)
                    return;

                Console.WriteLine("Did not get data successfully");
                MessageBox.Show("Was unable to retrieve data from NUT sever. Got an \"Access Denied\" message.\n\nA common cause of this is the IP address of this client not being whitelisted on the NUT server.");
                isPollingUPS = false;
                return;
            } else
            {
                isPollingUPS = true;
            }

            UInt16 BatteryPercentage = Convert.ToUInt16(NUT_Processor.SearchNUTData("battery.charge"));
            if (BatteryPercentage <= AlarmPercentage)
            {
                PerformAlarmAction();
            }

            if (Backend.Background.isSimulated && IsSimulatingDecay)
            {
                SimulateBatteryDecay();
            }

            // Refreshes the data on the form
            Tuple<string, double, int> UPSBatteryStatus = NUT_Processor.GetBatteryStatus();
            lblUPSStatus.Invoke((MethodInvoker)(() => UpdateUPSStatus(UPSBatteryStatus.Item1, UPSBatteryStatus.Item3)));
            lblUPSModel.Invoke ((MethodInvoker)(() => lblUPSModel.Text = (NUT_Processor.UPSStatistics())));
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
            frmSettings_Resize("",EventArgs.Empty);

            UPSPollTimer = new System.Timers.Timer(5000);
            UPSPollTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            UPSPollTimer.AutoReset = true;
            bool IsConfigurationNeeded = false;

            // Checks to see if we are in a simulation environment
            if (Backend.Background.isSimulated) {
                chkSimulate.Checked = true;
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
                NUTServer = IPAddress.Parse(NUTConnectionSettings.Item1.ToString());
            }
            else IsConfigurationNeeded = true;

            if (NUTConnectionSettings.Item2 != 0)
            {
                txtPort.Text = NUTConnectionSettings.Item2.ToString();
                NUTPort = Convert.ToUInt16(NUTConnectionSettings.Item2);
            }
            else IsConfigurationNeeded = true;

            if (NUTConnectionSettings.Item3 != 0)
            {
                txtPollFrequency.Text = NUTConnectionSettings.Item3.ToString();
                UPSPollTimer.Interval = (Convert.ToUInt32(NUTConnectionSettings.Item3) * 1000);
            }
            else IsConfigurationNeeded = true;

            string TempValue;
            UInt32 number;

            TempValue = Backend.NUT_Config.GetConfig("Warn Threshold");
            if (UInt32.TryParse(TempValue, out number))
            {
                AlarmPercentage = Convert.ToUInt16(TempValue);
                cmbBatteryPercentage.Text = TempValue;
            }
            else
            {
                cmbBatteryPercentage.Text = "20%";
                AlarmPercentage = 20;
            }

            TempValue = Backend.NUT_Config.GetConfig("Threshold Action");
            if (TempValue == null)
                cmbAlarmAction.Text = "Hibernate";
            else if (TempValue.Equals(""))
                cmbAlarmAction.Text = "Hibernate";
            else if (TempValue.Equals("Execute Script"))
            { 
                cmbAlarmAction.Text = "Execute Script";
                txtScriptPath.Text = Backend.NUT_Config.GetConfig("Script Path");
                txtScriptPath.Enabled = true;
                btnBrowse.Enabled = true;
            } else
                cmbAlarmAction.Text = TempValue;

            chkNotification.Checked = Convert.ToBoolean(Backend.NUT_Config.GetConfig("Notification"));
            chkAlarm.Checked = Convert.ToBoolean(Backend.NUT_Config.GetConfig("Alarm"));
            chkDebugLogging.Checked = Convert.ToBoolean(Backend.NUT_Config.GetConfig("Debug"));

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

            if (Backend.Background.isSimulated)
            {
                SimulatorPopulate();
            }

            if (isPollingUPS)
            {
                pnlSettings.Enabled = true;
                pnlAlarms.Enabled = true;
            }

            btnApply.Enabled = false;
            UPSPollTimer.Enabled = true;
            UPSPollTimer.Start();

            if(IsConfigurationNeeded)
            {
                // If this is run for the first time, or the registry keys have been cleared it will bring up the
                // settings form to make it a bit more convenient for the user.
                _frmSettings.Show();
                _frmSettings.WindowState = FormWindowState.Normal;
            }

        }

        /*        private void frmSettings_FormClosing(object sender, FormClosingEventArgs e, System.EventArgs f)
                {

                    DialogResult = MessageBox.Show("Closing the application will also stop the UPS monitoring. Close anyway?", "Exiting " + Application.ProductName, MessageBoxButtons.YesNo);

                    if (f = )

                    if (DialogResult == DialogResult.Yes)
                    {
                        Backend.Background.WriteNUTLog("[APP] Application is now closing.");
                    }
                    else
                    {
                        e.Cancel = true;
                        return;
                    }
                    
    }
    */

    private void PerformAlarmAction()
        {
            if (Backend.Background.isSimulated && cmbSimUPSStatus.Text.Equals("OB DISCHRG"))
            {
                cmbAlarmAction.Invoke((MethodInvoker)(() => MessageBox.Show("This is a simulation. If this were a real event, the PC would be " + Backend.NUT_Config.GetConfig("Threshold Action"))));
                UPSPollTimer.Stop();
            }
            else if (Backend.Background.isSimulated)
                return;
            else
            {
                string AlarmAction = Backend.NUT_Config.GetConfig("Threshold Action");

                if (AlarmAction.Equals("Do Nothing"))
                {
                    return;
                }
                else if (AlarmAction.Equals("Shut Down"))
                {
                    Backend.Background.WriteNUTLog("[APP] Shutting down PC");
                    UPSPollTimer.Enabled = false;
                    var psi = new System.Diagnostics.ProcessStartInfo("shutdown", "/s /t 0");
                    psi.CreateNoWindow = true;
                    psi.UseShellExecute = false;
                    System.Diagnostics.Process.Start(psi);
                    
                }
                else if (AlarmAction.Equals("Hibernate"))
                {
                    UPSPollTimer.Enabled = false;
                    Backend.Background.WriteNUTLog("[APP] Hibernating PC");
                    Application.SetSuspendState(PowerState.Hibernate, true, true);

                }
                else if (AlarmAction.Equals("Execute Script"))
                {
                    string ScriptPath = Backend.NUT_Config.GetConfig("Script Path");
                    Backend.Background.WriteNUTLog("[APP] Executing command: " + ScriptPath);
                    System.Diagnostics.Process.Start("cmd.exe", ScriptPath);
                }

            }
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
                // Makes the required adjustments to get the simulator to work
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
                    UPSPollTimer.Interval = (Convert.ToInt32(txtPollFrequency.Text) * 1000);
                }

                if (isPollingUPS) UPSPoll();

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

            // Alarm Path
            if (cmbAlarmAction.Text.Equals("Execute Script"))
            {
                Backend.NUT_Config.SetConfig("Script Path", txtScriptPath.Text);
            }
            

            // Debug Logging
            if (chkDebugLogging.Checked)
            {
                Backend.NUT_Config.SetConfig("Debug", "true");
                Backend.Background.isLogging = true;
                Backend.Background.WriteNUTLog("[BACKEND] Debug Logging Enabled");
            }
            else
            {
                Backend.NUT_Config.SetConfig("Debug", "false");
                Backend.Background.isLogging = false;
                Backend.Background.WriteNUTLog("[BACKEND] Debug Logging Disabled");
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

            IsSimulatingDecay = chkSimulate.AutoCheck;

            UPSPoll();
        }

        private void SimulateBatteryDecay()
        {
            if (txtSimBatteryRuntime.Text.Equals(""))
                SimulatorPopulate();

            double BattValue = Convert.ToDouble(NUT_Processor.SearchNUTData("battery.charge"));
            Int32 BattRuntimeMax = Convert.ToInt32(txtSimBatteryRuntime.Text);
            Int32 BattRuntimeBreakdown = (BattRuntimeMax / 100);
            Int32 BattRuntime = Convert.ToInt32(NUT_Processor.SearchNUTData("battery.runtime"));

            // Since it would be impossible for a battery to have a negative charge, it will set the charge to 0
            if ((BattValue - SimUPSDecayRate) <= 0)
            {
                Backend.Background.WriteNUTLog("[SIMULATOR] Battery decayed to empty");
                NUT_Processor.ModifySimNUTData("battery.charge", "0");
                NUT_Processor.ModifySimNUTData("battery.runtime", "0");
            }
            else
            {
                Backend.Background.WriteNUTLog("[SIMULATOR] Battery decayed by " + SimUPSDecayRate + " from " + BattValue + " to " + (BattValue - SimUPSDecayRate));
                NUT_Processor.ModifySimNUTData("battery.charge", Convert.ToString(BattValue - SimUPSDecayRate));
                NUT_Processor.ModifySimNUTData("battery.runtime", Convert.ToString(BattRuntime - (BattRuntimeBreakdown * SimUPSDecayRate)));
            }

            // Refresh information 
            //UPSPoll();
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
            if (cmbAlarmAction.Text.Equals("Execute Script"))
            {
                txtScriptPath.Enabled = true;
                btnBrowse.Enabled = true;
            }
            else
            {
                txtScriptPath.Enabled = false;
                btnBrowse.Enabled = false;
            }
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
            if (chkDebugLogging.Checked)
                MessageBox.Show("Note: Debug logging has been enabled. This should only be enabled when absolutely necessary, as everything is logged and added to the file each time the UPS is polled, which could result in very large log files.\n\nLogs are stored in the Logs folder under the application folder path below:\n\n" + Application.StartupPath);
            btnApply.Enabled = true;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            dlgScriptPath.ShowDialog();
            txtScriptPath.Text = dlgScriptPath.FileName;
        }

        private void txtScriptPath_TextChanged(object sender, EventArgs e)
        {
            btnApply.Enabled = true;
        }

    }

}