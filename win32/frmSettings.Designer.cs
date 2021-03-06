﻿namespace NUTty_UPS_Client
{
    partial class frmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblUPSModel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPollFrequency = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.pnlDebug = new System.Windows.Forms.Panel();
            this.chkDebugLogging = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlAlarms = new System.Windows.Forms.Panel();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtScriptPath = new System.Windows.Forms.TextBox();
            this.chkAlarm = new System.Windows.Forms.CheckBox();
            this.chkNotification = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbBatteryPercentage = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbAlarmAction = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chkSimulate = new System.Windows.Forms.CheckBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnRevert = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblUPSStatus = new System.Windows.Forms.Label();
            this.pnlSimulator = new System.Windows.Forms.Panel();
            this.btnSimBatteryDecay = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.txtSimBatteryDecay = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnSimApply = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.cmbSimUPSStatus = new System.Windows.Forms.ComboBox();
            this.txtSimOutputVoltage = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtSimInputVoltageNominal = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtSimInputVoltage = new System.Windows.Forms.TextBox();
            this.lblSimInputVoltage = new System.Windows.Forms.Label();
            this.txtSimBatteryVoltageNominal = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtSimBatteryVoltage = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtSimBatteryRuntimeLow = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtSimBatteryRuntime = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtSimBatteryChargeWarn = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtSimBatteryChargeLow = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSimBatteryCharge = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lblSimulator = new System.Windows.Forms.Label();
            this.ntfUPSTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.mnuNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuNotifySettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNotifyExit = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgScriptPath = new System.Windows.Forms.OpenFileDialog();
            this.pnlSettings.SuspendLayout();
            this.pnlDebug.SuspendLayout();
            this.pnlAlarms.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnlSimulator.SuspendLayout();
            this.mnuNotify.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(48, 78);
            this.txtIPAddress.Margin = new System.Windows.Forms.Padding(6);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(180, 29);
            this.txtIPAddress.TabIndex = 0;
            this.txtIPAddress.Text = "127.0.0.1";
            this.txtIPAddress.TextChanged += new System.EventHandler(this.txtIPAddress_TextChanged);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(242, 78);
            this.txtPort.Margin = new System.Windows.Forms.Padding(6);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(77, 29);
            this.txtPort.TabIndex = 1;
            this.txtPort.Text = "3493";
            this.txtPort.TextChanged += new System.EventHandler(this.txtPort_TextChanged);
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.Location = new System.Drawing.Point(46, 48);
            this.lblIPAddress.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(108, 25);
            this.lblIPAddress.TabIndex = 2;
            this.lblIPAddress.Text = "IP Address";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(237, 48);
            this.lblPort.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(47, 25);
            this.lblPort.TabIndex = 3;
            this.lblPort.Text = "Port";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(334, 48);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(6);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(182, 66);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Test Connection";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblUPSModel
            // 
            this.lblUPSModel.AutoSize = true;
            this.lblUPSModel.Location = new System.Drawing.Point(535, 17);
            this.lblUPSModel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblUPSModel.Name = "lblUPSModel";
            this.lblUPSModel.Size = new System.Drawing.Size(143, 25);
            this.lblUPSModel.TabIndex = 6;
            this.lblUPSModel.Text = "Not Connected";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(188, 25);
            this.label1.TabIndex = 8;
            this.label1.Text = "Connection Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 25);
            this.label2.TabIndex = 12;
            this.label2.Text = "Polling Frequency";
            // 
            // txtPollFrequency
            // 
            this.txtPollFrequency.Location = new System.Drawing.Point(20, 30);
            this.txtPollFrequency.Margin = new System.Windows.Forms.Padding(6);
            this.txtPollFrequency.Name = "txtPollFrequency";
            this.txtPollFrequency.Size = new System.Drawing.Size(77, 29);
            this.txtPollFrequency.TabIndex = 13;
            this.txtPollFrequency.Text = "5";
            this.txtPollFrequency.TextChanged += new System.EventHandler(this.txtPollFrequency_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(112, 35);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 25);
            this.label3.TabIndex = 14;
            this.label3.Text = "seconds";
            // 
            // pnlSettings
            // 
            this.pnlSettings.Controls.Add(this.label3);
            this.pnlSettings.Controls.Add(this.txtPollFrequency);
            this.pnlSettings.Controls.Add(this.label2);
            this.pnlSettings.Enabled = false;
            this.pnlSettings.Location = new System.Drawing.Point(28, 142);
            this.pnlSettings.Margin = new System.Windows.Forms.Padding(6);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(488, 79);
            this.pnlSettings.TabIndex = 12;
            // 
            // pnlDebug
            // 
            this.pnlDebug.Controls.Add(this.chkDebugLogging);
            this.pnlDebug.Controls.Add(this.label4);
            this.pnlDebug.Location = new System.Drawing.Point(28, 642);
            this.pnlDebug.Margin = new System.Windows.Forms.Padding(6);
            this.pnlDebug.Name = "pnlDebug";
            this.pnlDebug.Size = new System.Drawing.Size(488, 68);
            this.pnlDebug.TabIndex = 13;
            // 
            // chkDebugLogging
            // 
            this.chkDebugLogging.AutoSize = true;
            this.chkDebugLogging.Location = new System.Drawing.Point(26, 30);
            this.chkDebugLogging.Margin = new System.Windows.Forms.Padding(6);
            this.chkDebugLogging.Name = "chkDebugLogging";
            this.chkDebugLogging.Size = new System.Drawing.Size(174, 29);
            this.chkDebugLogging.TabIndex = 20;
            this.chkDebugLogging.Text = "Enable Logging";
            this.chkDebugLogging.UseVisualStyleBackColor = true;
            this.chkDebugLogging.CheckedChanged += new System.EventHandler(this.chkDebugLogging_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 25);
            this.label4.TabIndex = 20;
            this.label4.Text = "Debug Logging";
            // 
            // pnlAlarms
            // 
            this.pnlAlarms.Controls.Add(this.btnBrowse);
            this.pnlAlarms.Controls.Add(this.label5);
            this.pnlAlarms.Controls.Add(this.txtScriptPath);
            this.pnlAlarms.Controls.Add(this.chkAlarm);
            this.pnlAlarms.Controls.Add(this.chkNotification);
            this.pnlAlarms.Controls.Add(this.label8);
            this.pnlAlarms.Controls.Add(this.cmbBatteryPercentage);
            this.pnlAlarms.Controls.Add(this.label6);
            this.pnlAlarms.Controls.Add(this.cmbAlarmAction);
            this.pnlAlarms.Controls.Add(this.label7);
            this.pnlAlarms.Enabled = false;
            this.pnlAlarms.Location = new System.Drawing.Point(28, 233);
            this.pnlAlarms.Margin = new System.Windows.Forms.Padding(6);
            this.pnlAlarms.Name = "pnlAlarms";
            this.pnlAlarms.Size = new System.Drawing.Size(488, 399);
            this.pnlAlarms.TabIndex = 14;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Enabled = false;
            this.btnBrowse.Location = new System.Drawing.Point(370, 238);
            this.btnBrowse.Margin = new System.Windows.Forms.Padding(6);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(117, 42);
            this.btnBrowse.TabIndex = 21;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 212);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 25);
            this.label5.TabIndex = 20;
            this.label5.Text = "Path to script";
            // 
            // txtScriptPath
            // 
            this.txtScriptPath.Enabled = false;
            this.txtScriptPath.Location = new System.Drawing.Point(26, 242);
            this.txtScriptPath.Margin = new System.Windows.Forms.Padding(6);
            this.txtScriptPath.Name = "txtScriptPath";
            this.txtScriptPath.Size = new System.Drawing.Size(330, 29);
            this.txtScriptPath.TabIndex = 19;
            this.txtScriptPath.TextChanged += new System.EventHandler(this.txtScriptPath_TextChanged);
            // 
            // chkAlarm
            // 
            this.chkAlarm.AutoSize = true;
            this.chkAlarm.Location = new System.Drawing.Point(26, 166);
            this.chkAlarm.Margin = new System.Windows.Forms.Padding(6);
            this.chkAlarm.Name = "chkAlarm";
            this.chkAlarm.Size = new System.Drawing.Size(217, 29);
            this.chkAlarm.TabIndex = 18;
            this.chkAlarm.Text = "Audible alarm on PC";
            this.chkAlarm.UseVisualStyleBackColor = true;
            this.chkAlarm.CheckedChanged += new System.EventHandler(this.chkAlarm_CheckedChanged);
            // 
            // chkNotification
            // 
            this.chkNotification.AutoSize = true;
            this.chkNotification.Location = new System.Drawing.Point(26, 124);
            this.chkNotification.Margin = new System.Windows.Forms.Padding(6);
            this.chkNotification.Name = "chkNotification";
            this.chkNotification.Size = new System.Drawing.Size(322, 29);
            this.chkNotification.TabIndex = 17;
            this.chkNotification.Text = "Notify when UPS status changes";
            this.chkNotification.UseVisualStyleBackColor = true;
            this.chkNotification.CheckedChanged += new System.EventHandler(this.chkNotification_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 76);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 25);
            this.label8.TabIndex = 16;
            this.label8.Text = "the PC will:";
            // 
            // cmbBatteryPercentage
            // 
            this.cmbBatteryPercentage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBatteryPercentage.FormattingEnabled = true;
            this.cmbBatteryPercentage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.cmbBatteryPercentage.Items.AddRange(new object[] {
            "95%",
            "90%",
            "85%",
            "80%",
            "75%",
            "70%",
            "65%",
            "60%",
            "55%",
            "50%",
            "45%",
            "40%",
            "35%",
            "30%",
            "25%",
            "20%",
            "15%",
            "10%",
            "5%"});
            this.cmbBatteryPercentage.Location = new System.Drawing.Point(270, 26);
            this.cmbBatteryPercentage.Margin = new System.Windows.Forms.Padding(6);
            this.cmbBatteryPercentage.Name = "cmbBatteryPercentage";
            this.cmbBatteryPercentage.Size = new System.Drawing.Size(90, 32);
            this.cmbBatteryPercentage.TabIndex = 15;
            this.cmbBatteryPercentage.SelectedIndexChanged += new System.EventHandler(this.cmbBatteryPercentage_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 31);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(236, 25);
            this.label6.TabIndex = 14;
            this.label6.Text = "When the battery reaches";
            // 
            // cmbAlarmAction
            // 
            this.cmbAlarmAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlarmAction.FormattingEnabled = true;
            this.cmbAlarmAction.Items.AddRange(new object[] {
            "Do Nothing",
            "Hibernate",
            "Shut Down",
            "Execute Script"});
            this.cmbAlarmAction.Location = new System.Drawing.Point(128, 70);
            this.cmbAlarmAction.Margin = new System.Windows.Forms.Padding(6);
            this.cmbAlarmAction.Name = "cmbAlarmAction";
            this.cmbAlarmAction.Size = new System.Drawing.Size(231, 32);
            this.cmbAlarmAction.TabIndex = 13;
            this.cmbAlarmAction.SelectedIndexChanged += new System.EventHandler(this.cmbAlarmAction_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 0);
            this.label7.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(73, 25);
            this.label7.TabIndex = 12;
            this.label7.Text = "Alarms";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkSimulate);
            this.panel1.Controls.Add(this.btnApply);
            this.panel1.Controls.Add(this.btnRevert);
            this.panel1.Location = new System.Drawing.Point(28, 722);
            this.panel1.Margin = new System.Windows.Forms.Padding(6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(488, 61);
            this.panel1.TabIndex = 15;
            // 
            // chkSimulate
            // 
            this.chkSimulate.AutoSize = true;
            this.chkSimulate.Location = new System.Drawing.Point(26, 13);
            this.chkSimulate.Margin = new System.Windows.Forms.Padding(6);
            this.chkSimulate.Name = "chkSimulate";
            this.chkSimulate.Size = new System.Drawing.Size(125, 29);
            this.chkSimulate.TabIndex = 2;
            this.chkSimulate.Text = "Simulated";
            this.chkSimulate.UseVisualStyleBackColor = true;
            this.chkSimulate.CheckedChanged += new System.EventHandler(this.chkSimulate_CheckedChanged);
            // 
            // btnApply
            // 
            this.btnApply.Enabled = false;
            this.btnApply.Location = new System.Drawing.Point(350, 6);
            this.btnApply.Margin = new System.Windows.Forms.Padding(6);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(138, 42);
            this.btnApply.TabIndex = 1;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnRevert
            // 
            this.btnRevert.Location = new System.Drawing.Point(202, 6);
            this.btnRevert.Margin = new System.Windows.Forms.Padding(6);
            this.btnRevert.Name = "btnRevert";
            this.btnRevert.Size = new System.Drawing.Size(138, 42);
            this.btnRevert.TabIndex = 0;
            this.btnRevert.Text = "Revert";
            this.btnRevert.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblUPSStatus);
            this.panel2.Location = new System.Drawing.Point(28, 794);
            this.panel2.Margin = new System.Windows.Forms.Padding(6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(818, 57);
            this.panel2.TabIndex = 16;
            // 
            // lblUPSStatus
            // 
            this.lblUPSStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUPSStatus.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblUPSStatus.Location = new System.Drawing.Point(2, 6);
            this.lblUPSStatus.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblUPSStatus.Name = "lblUPSStatus";
            this.lblUPSStatus.Size = new System.Drawing.Size(816, 46);
            this.lblUPSStatus.TabIndex = 9;
            this.lblUPSStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlSimulator
            // 
            this.pnlSimulator.Controls.Add(this.btnSimBatteryDecay);
            this.pnlSimulator.Controls.Add(this.label20);
            this.pnlSimulator.Controls.Add(this.txtSimBatteryDecay);
            this.pnlSimulator.Controls.Add(this.label12);
            this.pnlSimulator.Controls.Add(this.btnSimApply);
            this.pnlSimulator.Controls.Add(this.label19);
            this.pnlSimulator.Controls.Add(this.cmbSimUPSStatus);
            this.pnlSimulator.Controls.Add(this.txtSimOutputVoltage);
            this.pnlSimulator.Controls.Add(this.label17);
            this.pnlSimulator.Controls.Add(this.txtSimInputVoltageNominal);
            this.pnlSimulator.Controls.Add(this.label18);
            this.pnlSimulator.Controls.Add(this.txtSimInputVoltage);
            this.pnlSimulator.Controls.Add(this.lblSimInputVoltage);
            this.pnlSimulator.Controls.Add(this.txtSimBatteryVoltageNominal);
            this.pnlSimulator.Controls.Add(this.label15);
            this.pnlSimulator.Controls.Add(this.txtSimBatteryVoltage);
            this.pnlSimulator.Controls.Add(this.label16);
            this.pnlSimulator.Controls.Add(this.txtSimBatteryRuntimeLow);
            this.pnlSimulator.Controls.Add(this.label14);
            this.pnlSimulator.Controls.Add(this.txtSimBatteryRuntime);
            this.pnlSimulator.Controls.Add(this.label13);
            this.pnlSimulator.Controls.Add(this.txtSimBatteryChargeWarn);
            this.pnlSimulator.Controls.Add(this.label11);
            this.pnlSimulator.Controls.Add(this.txtSimBatteryChargeLow);
            this.pnlSimulator.Controls.Add(this.label10);
            this.pnlSimulator.Controls.Add(this.txtSimBatteryCharge);
            this.pnlSimulator.Controls.Add(this.label9);
            this.pnlSimulator.Controls.Add(this.lblSimulator);
            this.pnlSimulator.Location = new System.Drawing.Point(840, 17);
            this.pnlSimulator.Margin = new System.Windows.Forms.Padding(6);
            this.pnlSimulator.Name = "pnlSimulator";
            this.pnlSimulator.Size = new System.Drawing.Size(367, 834);
            this.pnlSimulator.TabIndex = 17;
            // 
            // btnSimBatteryDecay
            // 
            this.btnSimBatteryDecay.Location = new System.Drawing.Point(11, 639);
            this.btnSimBatteryDecay.Margin = new System.Windows.Forms.Padding(6);
            this.btnSimBatteryDecay.Name = "btnSimBatteryDecay";
            this.btnSimBatteryDecay.Size = new System.Drawing.Size(138, 42);
            this.btnSimBatteryDecay.TabIndex = 29;
            this.btnSimBatteryDecay.Text = "Manual Poll";
            this.btnSimBatteryDecay.UseVisualStyleBackColor = true;
            this.btnSimBatteryDecay.Click += new System.EventHandler(this.btnSimBatteryDecay_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(334, 593);
            this.label20.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(30, 25);
            this.label20.TabIndex = 28;
            this.label20.Text = "%";
            // 
            // txtSimBatteryDecay
            // 
            this.txtSimBatteryDecay.Location = new System.Drawing.Point(227, 587);
            this.txtSimBatteryDecay.Margin = new System.Windows.Forms.Padding(6);
            this.txtSimBatteryDecay.Name = "txtSimBatteryDecay";
            this.txtSimBatteryDecay.Size = new System.Drawing.Size(98, 29);
            this.txtSimBatteryDecay.TabIndex = 27;
            this.txtSimBatteryDecay.Text = "1";
            this.txtSimBatteryDecay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 580);
            this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(215, 50);
            this.label12.TabIndex = 26;
            this.label12.Text = "UPS Battery decay rate\r\n(in % points per poll)";
            // 
            // btnSimApply
            // 
            this.btnSimApply.Location = new System.Drawing.Point(224, 639);
            this.btnSimApply.Margin = new System.Windows.Forms.Padding(6);
            this.btnSimApply.Name = "btnSimApply";
            this.btnSimApply.Size = new System.Drawing.Size(138, 42);
            this.btnSimApply.TabIndex = 25;
            this.btnSimApply.Text = "Apply";
            this.btnSimApply.UseVisualStyleBackColor = true;
            this.btnSimApply.Click += new System.EventHandler(this.btnSimApply_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(6, 530);
            this.label19.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(101, 25);
            this.label19.TabIndex = 24;
            this.label19.Text = "ups.status";
            // 
            // cmbSimUPSStatus
            // 
            this.cmbSimUPSStatus.FormattingEnabled = true;
            this.cmbSimUPSStatus.Items.AddRange(new object[] {
            "OL",
            "OB DISCHRG"});
            this.cmbSimUPSStatus.Location = new System.Drawing.Point(227, 524);
            this.cmbSimUPSStatus.Margin = new System.Windows.Forms.Padding(6);
            this.cmbSimUPSStatus.Name = "cmbSimUPSStatus";
            this.cmbSimUPSStatus.Size = new System.Drawing.Size(131, 32);
            this.cmbSimUPSStatus.TabIndex = 23;
            this.cmbSimUPSStatus.SelectedIndexChanged += new System.EventHandler(this.cmbSimUPSStatus_SelectedIndexChanged);
            // 
            // txtSimOutputVoltage
            // 
            this.txtSimOutputVoltage.Location = new System.Drawing.Point(227, 476);
            this.txtSimOutputVoltage.Margin = new System.Windows.Forms.Padding(6);
            this.txtSimOutputVoltage.Name = "txtSimOutputVoltage";
            this.txtSimOutputVoltage.Size = new System.Drawing.Size(131, 29);
            this.txtSimOutputVoltage.TabIndex = 22;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 482);
            this.label17.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(134, 25);
            this.label17.TabIndex = 21;
            this.label17.Text = "output.voltage";
            // 
            // txtSimInputVoltageNominal
            // 
            this.txtSimInputVoltageNominal.Location = new System.Drawing.Point(227, 428);
            this.txtSimInputVoltageNominal.Margin = new System.Windows.Forms.Padding(6);
            this.txtSimInputVoltageNominal.Name = "txtSimInputVoltageNominal";
            this.txtSimInputVoltageNominal.Size = new System.Drawing.Size(131, 29);
            this.txtSimInputVoltageNominal.TabIndex = 20;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 434);
            this.label18.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(195, 25);
            this.label18.TabIndex = 19;
            this.label18.Text = "input.voltage.nominal";
            // 
            // txtSimInputVoltage
            // 
            this.txtSimInputVoltage.Location = new System.Drawing.Point(227, 382);
            this.txtSimInputVoltage.Margin = new System.Windows.Forms.Padding(6);
            this.txtSimInputVoltage.Name = "txtSimInputVoltage";
            this.txtSimInputVoltage.Size = new System.Drawing.Size(131, 29);
            this.txtSimInputVoltage.TabIndex = 18;
            // 
            // lblSimInputVoltage
            // 
            this.lblSimInputVoltage.AutoSize = true;
            this.lblSimInputVoltage.Location = new System.Drawing.Point(6, 388);
            this.lblSimInputVoltage.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSimInputVoltage.Name = "lblSimInputVoltage";
            this.lblSimInputVoltage.Size = new System.Drawing.Size(122, 25);
            this.lblSimInputVoltage.TabIndex = 17;
            this.lblSimInputVoltage.Text = "input.voltage";
            // 
            // txtSimBatteryVoltageNominal
            // 
            this.txtSimBatteryVoltageNominal.Location = new System.Drawing.Point(227, 334);
            this.txtSimBatteryVoltageNominal.Margin = new System.Windows.Forms.Padding(6);
            this.txtSimBatteryVoltageNominal.Name = "txtSimBatteryVoltageNominal";
            this.txtSimBatteryVoltageNominal.Size = new System.Drawing.Size(131, 29);
            this.txtSimBatteryVoltageNominal.TabIndex = 16;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(6, 340);
            this.label15.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(212, 25);
            this.label15.TabIndex = 15;
            this.label15.Text = "battery.voltage.nominal";
            // 
            // txtSimBatteryVoltage
            // 
            this.txtSimBatteryVoltage.Location = new System.Drawing.Point(227, 286);
            this.txtSimBatteryVoltage.Margin = new System.Windows.Forms.Padding(6);
            this.txtSimBatteryVoltage.Name = "txtSimBatteryVoltage";
            this.txtSimBatteryVoltage.Size = new System.Drawing.Size(131, 29);
            this.txtSimBatteryVoltage.TabIndex = 14;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 292);
            this.label16.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(139, 25);
            this.label16.TabIndex = 13;
            this.label16.Text = "battery.voltage";
            // 
            // txtSimBatteryRuntimeLow
            // 
            this.txtSimBatteryRuntimeLow.Location = new System.Drawing.Point(227, 240);
            this.txtSimBatteryRuntimeLow.Margin = new System.Windows.Forms.Padding(6);
            this.txtSimBatteryRuntimeLow.Name = "txtSimBatteryRuntimeLow";
            this.txtSimBatteryRuntimeLow.Size = new System.Drawing.Size(131, 29);
            this.txtSimBatteryRuntimeLow.TabIndex = 12;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 246);
            this.label14.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(174, 25);
            this.label14.TabIndex = 11;
            this.label14.Text = "battery.runtime.low";
            // 
            // txtSimBatteryRuntime
            // 
            this.txtSimBatteryRuntime.Location = new System.Drawing.Point(227, 192);
            this.txtSimBatteryRuntime.Margin = new System.Windows.Forms.Padding(6);
            this.txtSimBatteryRuntime.Name = "txtSimBatteryRuntime";
            this.txtSimBatteryRuntime.Size = new System.Drawing.Size(131, 29);
            this.txtSimBatteryRuntime.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 198);
            this.label13.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(140, 25);
            this.label13.TabIndex = 9;
            this.label13.Text = "battery.runtime";
            // 
            // txtSimBatteryChargeWarn
            // 
            this.txtSimBatteryChargeWarn.Location = new System.Drawing.Point(227, 144);
            this.txtSimBatteryChargeWarn.Margin = new System.Windows.Forms.Padding(6);
            this.txtSimBatteryChargeWarn.Name = "txtSimBatteryChargeWarn";
            this.txtSimBatteryChargeWarn.Size = new System.Drawing.Size(131, 29);
            this.txtSimBatteryChargeWarn.TabIndex = 6;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 150);
            this.label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(209, 25);
            this.label11.TabIndex = 5;
            this.label11.Text = "battery.charge.warning";
            // 
            // txtSimBatteryChargeLow
            // 
            this.txtSimBatteryChargeLow.Location = new System.Drawing.Point(227, 96);
            this.txtSimBatteryChargeLow.Margin = new System.Windows.Forms.Padding(6);
            this.txtSimBatteryChargeLow.Name = "txtSimBatteryChargeLow";
            this.txtSimBatteryChargeLow.Size = new System.Drawing.Size(131, 29);
            this.txtSimBatteryChargeLow.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 102);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(170, 25);
            this.label10.TabIndex = 3;
            this.label10.Text = "battery.charge.low";
            // 
            // txtSimBatteryCharge
            // 
            this.txtSimBatteryCharge.Location = new System.Drawing.Point(227, 48);
            this.txtSimBatteryCharge.Margin = new System.Windows.Forms.Padding(6);
            this.txtSimBatteryCharge.Name = "txtSimBatteryCharge";
            this.txtSimBatteryCharge.Size = new System.Drawing.Size(131, 29);
            this.txtSimBatteryCharge.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 54);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(136, 25);
            this.label9.TabIndex = 1;
            this.label9.Text = "battery.charge";
            // 
            // lblSimulator
            // 
            this.lblSimulator.AutoSize = true;
            this.lblSimulator.Location = new System.Drawing.Point(6, 0);
            this.lblSimulator.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSimulator.Name = "lblSimulator";
            this.lblSimulator.Size = new System.Drawing.Size(170, 25);
            this.lblSimulator.TabIndex = 0;
            this.lblSimulator.Text = "Simulator Settings";
            // 
            // ntfUPSTray
            // 
            this.ntfUPSTray.ContextMenuStrip = this.mnuNotify;
            this.ntfUPSTray.Icon = ((System.Drawing.Icon)(resources.GetObject("ntfUPSTray.Icon")));
            this.ntfUPSTray.Text = "NUTty UPS Client";
            this.ntfUPSTray.Visible = true;
            // 
            // mnuNotify
            // 
            this.mnuNotify.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.mnuNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNotifySettings,
            this.mnuNotifyExit});
            this.mnuNotify.Name = "mnuNotify";
            this.mnuNotify.Size = new System.Drawing.Size(161, 72);
            // 
            // mnuNotifySettings
            // 
            this.mnuNotifySettings.Name = "mnuNotifySettings";
            this.mnuNotifySettings.Size = new System.Drawing.Size(160, 34);
            this.mnuNotifySettings.Text = "&Settings";
            this.mnuNotifySettings.Click += new System.EventHandler(this.mnuNotifySettings_Click);
            // 
            // mnuNotifyExit
            // 
            this.mnuNotifyExit.Name = "mnuNotifyExit";
            this.mnuNotifyExit.Size = new System.Drawing.Size(160, 34);
            this.mnuNotifyExit.Text = "E&xit";
            this.mnuNotifyExit.Click += new System.EventHandler(this.mnuNotifyExit_Click_1);
            // 
            // dlgScriptPath
            // 
            this.dlgScriptPath.FileName = "openFileDialog1";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 866);
            this.Controls.Add(this.pnlSimulator);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlAlarms);
            this.Controls.Add(this.pnlDebug);
            this.Controls.Add(this.pnlSettings);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblUPSModel);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblIPAddress);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIPAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "frmSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "NUTty UPS Client";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.Resize += new System.EventHandler(this.frmSettings_Resize);
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            this.pnlDebug.ResumeLayout(false);
            this.pnlDebug.PerformLayout();
            this.pnlAlarms.ResumeLayout(false);
            this.pnlAlarms.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.pnlSimulator.ResumeLayout(false);
            this.pnlSimulator.PerformLayout();
            this.mnuNotify.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblUPSModel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPollFrequency;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnlSettings;
        private System.Windows.Forms.Panel pnlDebug;
        private System.Windows.Forms.CheckBox chkDebugLogging;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlAlarms;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbBatteryPercentage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbAlarmAction;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkAlarm;
        private System.Windows.Forms.CheckBox chkNotification;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnRevert;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblUPSStatus;
        private System.Windows.Forms.CheckBox chkSimulate;
        private System.Windows.Forms.Panel pnlSimulator;
        private System.Windows.Forms.Label lblSimulator;
        private System.Windows.Forms.TextBox txtSimBatteryRuntime;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtSimBatteryChargeWarn;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtSimBatteryChargeLow;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtSimBatteryCharge;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSimBatteryRuntimeLow;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtSimBatteryVoltageNominal;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtSimBatteryVoltage;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnSimApply;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cmbSimUPSStatus;
        private System.Windows.Forms.TextBox txtSimOutputVoltage;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtSimInputVoltageNominal;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtSimInputVoltage;
        private System.Windows.Forms.Label lblSimInputVoltage;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtSimBatteryDecay;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btnSimBatteryDecay;
        private System.Windows.Forms.NotifyIcon ntfUPSTray;
        private System.Windows.Forms.ContextMenuStrip mnuNotify;
        private System.Windows.Forms.ToolStripMenuItem mnuNotifyExit;
        private System.Windows.Forms.ToolStripMenuItem mnuNotifySettings;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtScriptPath;
        private System.Windows.Forms.OpenFileDialog dlgScriptPath;
        private System.Windows.Forms.Button btnBrowse;
    }
}

