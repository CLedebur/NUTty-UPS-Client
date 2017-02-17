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

        public bool UPSPolling = false;
        const int UPSPollingInterval = 5000;
        private System.Timers.Timer UPSPollTimer;

        public frmSettings()
        {
            InitializeComponent();
            _frmSettings = this;

            UPSPollTimer = new System.Timers.Timer(UPSPollingInterval);
            UPSPollTimer.Elapsed += OnTimedEvent;
            UPSPollTimer.AutoReset = true;
            UPSPollTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (InvokeRequired) {
                Invoke(new MethodInvoker(UPSPoll));
            } else
            {
                if (UPSPolling) { UPSPoll(); }
            }

            return;
        }

        private void UPSPoll()
        {
            Int16 nutPort = Convert.ToInt16(txtPort.Text);
            string nutOutput = NUT_poller.PollNUTServer(txtIPAddress.Text, nutPort);
            NUT_Processor.ParseNUTOutput(nutOutput);
        }

        public static frmSettings _frmSettings;

        public void updateTxtOutput(string strOutput)
        {
            txtOutput.AppendText(Environment.NewLine + strOutput);
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

            if (UPSPolling)
            {
                updateTxtOutput("Automatic polling disabled");
                UPSPollTimer.Stop();
            } else
            {
                updateTxtOutput("Automatic polling enabled");
                UPSPollTimer.Start();
            }
            UPSPolling = !UPSPolling;
        }


    }

    
}
