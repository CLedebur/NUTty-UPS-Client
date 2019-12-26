using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using nuttyupsclient.Backend;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nuttyupsclient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class navSettings : Page
    {
        public navSettings()
        {
            this.InitializeComponent();
        }

        private void InitializeValues()
        {
            // IP Address field
            if (NUT_Background.NUTConnectionSettings.Item1 == null) txtIPAddress.PlaceholderText = "127.0.0.1";
            else txtIPAddress.Text = NUT_Background.NUTConnectionSettings.Item1;

            // Port field
            if (NUT_Background.NUTConnectionSettings.Item2 == 0) txtPort.Text = "3493";
            else txtPort.Text = NUT_Background.NUTConnectionSettings.Item2.ToString();

            // Poll Interval field
            if (NUT_Background.NUTConnectionSettings.Item3 == 0) txtPollFrequency.Text = "5";
            else txtPollFrequency.Text = NUT_Background.NUTConnectionSettings.Item3.ToString();

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (cmbAlarmAction.SelectedIndex.Equals(3))
            {
                pnlPath.Visibility = Visibility.Visible;
            }
            else
            {
                pnlPath.Visibility = Visibility.Collapsed;
            }
            
        }

        private async void BtnSave(object sender, RoutedEventArgs e)
        {
            // We're going to validate all the settings first to ensure that they'll work
            var validator = new NUT_Validator();

            // First, validate that the IP address is correct

            if (!validator.ValidateIPAddress(txtIPAddress.Text))
            {
                ContentDialog InvalidData = new ContentDialog
                {
                    Title = "Invalid IP address",
                    Content = "The IP address that was entered does not appear to be correct. Please correct it and try again.",
                    CloseButtonText = "OK"
                };
                await InvalidData.ShowAsync();
                return;
            }
            NUT_Config.SetConfig("IP Address",txtIPAddress.Text);

            // Then validate the port number
            if (!validator.ValidatePort(txtPort.Text))
            {
                ContentDialog InvalidData = new ContentDialog
                {
                    Title = "Invalid Port Number",
                    Content = "The port number that was entered does not appear to be correct. Please correct it and try again. It is typically 3493.",
                    CloseButtonText = "OK"
                };
                await InvalidData.ShowAsync();
                return;
            }
            NUT_Config.SetConfig("Port", txtPort.Text);

            // Finally we validate the polling frequency
            if (!validator.ValidatePollInterval(txtPollFrequency.Text))
            {
                ContentDialog InvalidData = new ContentDialog
                {
                    Title = "Invalid Poll Interval",
                    Content = "The polling interval does not appear to be correct. It should be 5 seconds at minimum.",
                    CloseButtonText = "OK"
                };
                await InvalidData.ShowAsync();
                return;
            }
            NUT_Config.SetConfig("Poll Interval", txtPollFrequency.Text);

            // We'll also update the public variable here
            NUT_Background.NUTConnectionSettings = Tuple.Create(txtIPAddress.Text, Convert.ToUInt16(txtPort.Text), Convert.ToUInt32(txtPollFrequency.Text) * 1000);
            NUT_Background.NeedConfig = false;
            var poller = new NUT_Poller();
            poller.ResumeUPSPolling();
            return;
        }

        private void OnLoading(FrameworkElement sender, object args)
        {
            InitializeValues();

        }
    }
}
