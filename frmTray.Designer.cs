namespace NUTty_UPS_Client
{
    partial class frmTray
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblUPSConnection = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.picSettings = new System.Windows.Forms.PictureBox();
            this.txtUPSStatistics = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTimeRemaining = new System.Windows.Forms.Label();
            this.lblBatteryPercentage = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSettings)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lblUPSConnection);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.lblBatteryPercentage);
            this.flowLayoutPanel1.Controls.Add(this.lblTimeRemaining);
            this.flowLayoutPanel1.Controls.Add(this.txtUPSStatistics);
            this.flowLayoutPanel1.Controls.Add(this.picSettings);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(160, 352);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.flowLayoutPanel1_Paint);
            // 
            // lblUPSConnection
            // 
            this.lblUPSConnection.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblUPSConnection.AutoSize = true;
            this.lblUPSConnection.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUPSConnection.Location = new System.Drawing.Point(8, 0);
            this.lblUPSConnection.MaximumSize = new System.Drawing.Size(150, 100);
            this.lblUPSConnection.MinimumSize = new System.Drawing.Size(150, 0);
            this.lblUPSConnection.Name = "lblUPSConnection";
            this.lblUPSConnection.Padding = new System.Windows.Forms.Padding(0, 6, 0, 6);
            this.lblUPSConnection.Size = new System.Drawing.Size(150, 33);
            this.lblUPSConnection.TabIndex = 8;
            this.lblUPSConnection.Text = "Not connected";
            this.lblUPSConnection.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // picSettings
            // 
            this.picSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.SetFlowBreak(this.picSettings, true);
            this.picSettings.Image = global::NUTty_UPS_Client.Properties.Resources.Settings_24;
            this.picSettings.Location = new System.Drawing.Point(139, 299);
            this.picSettings.Name = "picSettings";
            this.picSettings.Size = new System.Drawing.Size(24, 24);
            this.picSettings.TabIndex = 13;
            this.picSettings.TabStop = false;
            // 
            // txtUPSStatistics
            // 
            this.txtUPSStatistics.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUPSStatistics.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUPSStatistics.Location = new System.Drawing.Point(3, 112);
            this.txtUPSStatistics.MaximumSize = new System.Drawing.Size(160, 400);
            this.txtUPSStatistics.MinimumSize = new System.Drawing.Size(150, 100);
            this.txtUPSStatistics.Name = "txtUPSStatistics";
            this.txtUPSStatistics.Size = new System.Drawing.Size(160, 181);
            this.txtUPSStatistics.TabIndex = 12;
            this.txtUPSStatistics.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label1.Location = new System.Drawing.Point(3, 33);
            this.label1.MaximumSize = new System.Drawing.Size(150, 2);
            this.label1.MinimumSize = new System.Drawing.Size(150, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 2);
            this.label1.TabIndex = 11;
            // 
            // lblTimeRemaining
            // 
            this.lblTimeRemaining.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTimeRemaining.AutoSize = true;
            this.lblTimeRemaining.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTimeRemaining.Location = new System.Drawing.Point(8, 88);
            this.lblTimeRemaining.MaximumSize = new System.Drawing.Size(150, 100);
            this.lblTimeRemaining.MinimumSize = new System.Drawing.Size(150, 0);
            this.lblTimeRemaining.Name = "lblTimeRemaining";
            this.lblTimeRemaining.Size = new System.Drawing.Size(150, 21);
            this.lblTimeRemaining.TabIndex = 10;
            this.lblTimeRemaining.Text = "0 min remaining";
            this.lblTimeRemaining.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblBatteryPercentage
            // 
            this.lblBatteryPercentage.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblBatteryPercentage.AutoSize = true;
            this.lblBatteryPercentage.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatteryPercentage.Location = new System.Drawing.Point(8, 35);
            this.lblBatteryPercentage.MaximumSize = new System.Drawing.Size(150, 100);
            this.lblBatteryPercentage.MinimumSize = new System.Drawing.Size(150, 0);
            this.lblBatteryPercentage.Name = "lblBatteryPercentage";
            this.lblBatteryPercentage.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.lblBatteryPercentage.Size = new System.Drawing.Size(150, 53);
            this.lblBatteryPercentage.TabIndex = 9;
            this.lblBatteryPercentage.Text = "0%";
            this.lblBatteryPercentage.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // frmTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(174, 321);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(174, 360);
            this.MinimumSize = new System.Drawing.Size(174, 200);
            this.Name = "frmTray";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmTray_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSettings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label lblUPSConnection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTimeRemaining;
        private System.Windows.Forms.Label lblBatteryPercentage;
        private System.Windows.Forms.RichTextBox txtUPSStatistics;
        private System.Windows.Forms.PictureBox picSettings;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}