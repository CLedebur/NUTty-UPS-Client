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
            this.lblBatteryPercentage = new System.Windows.Forms.Label();
            this.lblRuntime = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblBatteryPercentage
            // 
            this.lblBatteryPercentage.AutoSize = true;
            this.lblBatteryPercentage.Font = new System.Drawing.Font("Segoe UI Black", 25.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatteryPercentage.Location = new System.Drawing.Point(0, 9);
            this.lblBatteryPercentage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBatteryPercentage.MinimumSize = new System.Drawing.Size(125, 0);
            this.lblBatteryPercentage.Name = "lblBatteryPercentage";
            this.lblBatteryPercentage.Size = new System.Drawing.Size(125, 47);
            this.lblBatteryPercentage.TabIndex = 0;
            this.lblBatteryPercentage.Text = "100%";
            this.lblBatteryPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRuntime
            // 
            this.lblRuntime.AutoSize = true;
            this.lblRuntime.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRuntime.Location = new System.Drawing.Point(2, 56);
            this.lblRuntime.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRuntime.MinimumSize = new System.Drawing.Size(125, 0);
            this.lblRuntime.Name = "lblRuntime";
            this.lblRuntime.Size = new System.Drawing.Size(125, 32);
            this.lblRuntime.TabIndex = 1;
            this.lblRuntime.Text = "50 min";
            this.lblRuntime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmTray
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(383, 99);
            this.ControlBox = false;
            this.Controls.Add(this.lblRuntime);
            this.Controls.Add(this.lblBatteryPercentage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTray";
            this.ShowIcon = false;
            this.Text = "v";
            this.Load += new System.EventHandler(this.frmTray_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblBatteryPercentage;
        private System.Windows.Forms.Label lblRuntime;
    }
}