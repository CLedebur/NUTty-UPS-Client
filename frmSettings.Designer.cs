namespace NUTty_UPS_Client
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.lblPort = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblUPSModel = new System.Windows.Forms.Label();
            this.lblUPSStatus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPollFrequency = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chkEnablePolling = new System.Windows.Forms.CheckBox();
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.pnlDebug = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlAlarms = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbAlarmAction = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbBatteryPercentage = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkNotification = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.pnlSettings.SuspendLayout();
            this.pnlDebug.SuspendLayout();
            this.pnlAlarms.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(23, 42);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(100, 20);
            this.txtIPAddress.TabIndex = 0;
            this.txtIPAddress.Text = "192.168.253.6";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(129, 42);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(44, 20);
            this.txtPort.TabIndex = 1;
            this.txtPort.Text = "3493";
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.Location = new System.Drawing.Point(23, 26);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(58, 13);
            this.lblIPAddress.TabIndex = 2;
            this.lblIPAddress.Text = "IP Address";
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Location = new System.Drawing.Point(126, 26);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(26, 13);
            this.lblPort.TabIndex = 3;
            this.lblPort.Text = "Port";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(179, 26);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(99, 36);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Test Connection";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblUPSModel
            // 
            this.lblUPSModel.AutoSize = true;
            this.lblUPSModel.Location = new System.Drawing.Point(297, 9);
            this.lblUPSModel.Name = "lblUPSModel";
            this.lblUPSModel.Size = new System.Drawing.Size(79, 13);
            this.lblUPSModel.TabIndex = 6;
            this.lblUPSModel.Text = "Not Connected";
            // 
            // lblUPSStatus
            // 
            this.lblUPSStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUPSStatus.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUPSStatus.Location = new System.Drawing.Point(7, 448);
            this.lblUPSStatus.Name = "lblUPSStatus";
            this.lblUPSStatus.Size = new System.Drawing.Size(445, 25);
            this.lblUPSStatus.TabIndex = 7;
            this.lblUPSStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Connection Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Polling Frequency";
            // 
            // txtPollFrequency
            // 
            this.txtPollFrequency.Location = new System.Drawing.Point(11, 16);
            this.txtPollFrequency.Name = "txtPollFrequency";
            this.txtPollFrequency.Size = new System.Drawing.Size(44, 20);
            this.txtPollFrequency.TabIndex = 13;
            this.txtPollFrequency.Text = "5";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(67, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "seconds";
            // 
            // chkEnablePolling
            // 
            this.chkEnablePolling.AutoSize = true;
            this.chkEnablePolling.Location = new System.Drawing.Point(14, 42);
            this.chkEnablePolling.Name = "chkEnablePolling";
            this.chkEnablePolling.Size = new System.Drawing.Size(93, 17);
            this.chkEnablePolling.TabIndex = 15;
            this.chkEnablePolling.Text = "Enable Polling";
            this.chkEnablePolling.UseVisualStyleBackColor = true;
            this.chkEnablePolling.CheckedChanged += new System.EventHandler(this.chkEnablePolling_CheckedChanged);
            // 
            // pnlSettings
            // 
            this.pnlSettings.Controls.Add(this.chkEnablePolling);
            this.pnlSettings.Controls.Add(this.label3);
            this.pnlSettings.Controls.Add(this.txtPollFrequency);
            this.pnlSettings.Controls.Add(this.label2);
            this.pnlSettings.Enabled = false;
            this.pnlSettings.Location = new System.Drawing.Point(12, 77);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(266, 63);
            this.pnlSettings.TabIndex = 12;
            // 
            // pnlDebug
            // 
            this.pnlDebug.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlDebug.Controls.Add(this.label5);
            this.pnlDebug.Controls.Add(this.checkBox1);
            this.pnlDebug.Controls.Add(this.label4);
            this.pnlDebug.Location = new System.Drawing.Point(12, 305);
            this.pnlDebug.Name = "pnlDebug";
            this.pnlDebug.Size = new System.Drawing.Size(266, 140);
            this.pnlDebug.TabIndex = 13;
            this.pnlDebug.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 36);
            this.label5.MaximumSize = new System.Drawing.Size(252, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(251, 91);
            this.label5.TabIndex = 21;
            this.label5.Text = resources.GetString("label5.Text");
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(14, 16);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(100, 17);
            this.checkBox1.TabIndex = 20;
            this.checkBox1.Text = "Enable Logging";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Debug Logging";
            // 
            // pnlAlarms
            // 
            this.pnlAlarms.Controls.Add(this.checkBox2);
            this.pnlAlarms.Controls.Add(this.chkNotification);
            this.pnlAlarms.Controls.Add(this.label8);
            this.pnlAlarms.Controls.Add(this.cmbBatteryPercentage);
            this.pnlAlarms.Controls.Add(this.label6);
            this.pnlAlarms.Controls.Add(this.cmbAlarmAction);
            this.pnlAlarms.Controls.Add(this.label7);
            this.pnlAlarms.Enabled = false;
            this.pnlAlarms.Location = new System.Drawing.Point(12, 146);
            this.pnlAlarms.Name = "pnlAlarms";
            this.pnlAlarms.Size = new System.Drawing.Size(266, 153);
            this.pnlAlarms.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Alarms";
            // 
            // cmbAlarmAction
            // 
            this.cmbAlarmAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlarmAction.FormattingEnabled = true;
            this.cmbAlarmAction.Items.AddRange(new object[] {
            "Do Nothing",
            "Hibernate",
            "Shut Down"});
            this.cmbAlarmAction.Location = new System.Drawing.Point(70, 38);
            this.cmbAlarmAction.Name = "cmbAlarmAction";
            this.cmbAlarmAction.Size = new System.Drawing.Size(128, 21);
            this.cmbAlarmAction.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "When the battery reaches";
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
            this.cmbBatteryPercentage.Location = new System.Drawing.Point(147, 14);
            this.cmbBatteryPercentage.Name = "cmbBatteryPercentage";
            this.cmbBatteryPercentage.Size = new System.Drawing.Size(51, 21);
            this.cmbBatteryPercentage.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 41);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "the PC will:";
            // 
            // chkNotification
            // 
            this.chkNotification.AutoSize = true;
            this.chkNotification.Location = new System.Drawing.Point(14, 67);
            this.chkNotification.Name = "chkNotification";
            this.chkNotification.Size = new System.Drawing.Size(182, 17);
            this.chkNotification.TabIndex = 17;
            this.chkNotification.Text = "Notify when UPS status changes";
            this.chkNotification.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(14, 90);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(121, 17);
            this.checkBox2.TabIndex = 18;
            this.checkBox2.Text = "Audible alarm on PC";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 482);
            this.Controls.Add(this.pnlAlarms);
            this.Controls.Add(this.pnlDebug);
            this.Controls.Add(this.pnlSettings);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblUPSStatus);
            this.Controls.Add(this.lblUPSModel);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.lblIPAddress);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIPAddress);
            this.Name = "frmSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "NUTty UPS Client";
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            this.pnlDebug.ResumeLayout(false);
            this.pnlDebug.PerformLayout();
            this.pnlAlarms.ResumeLayout(false);
            this.pnlAlarms.PerformLayout();
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
        private System.Windows.Forms.Label lblUPSStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPollFrequency;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox chkEnablePolling;
        private System.Windows.Forms.Panel pnlSettings;
        private System.Windows.Forms.Panel pnlDebug;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnlAlarms;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbBatteryPercentage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbAlarmAction;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox chkNotification;
    }
}

