using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using nuttyupsclient.Backend;
using System.Threading.Tasks;
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

        private void BtnSave(object sender, RoutedEventArgs e)
        {

            NUT_Config.SetConfig("IP Address", txtIPAddress.Text);
            NUT_Config.SetConfig("Port", txtPort.Text);
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

        private void TestConnection(object sender, RoutedEventArgs e)
        {

            if (ValidateSettings())
            {
                TestingRing.IsActive = true;
                bool PausePoll = false;
                if (NUT_Background.isPolling)
                {
                    NUT_Background.debugLog.Debug("[SETTINGS] Polling is active. Temporarily paused for the duration of this test.");
                    PausePoll = true;
                    NUT_Background.isPolling = false;
                }

                //Temporarily changing the button and disabling it for the duration of this test
                btnConnect.Content = "Testing...";
                btnConnect.IsEnabled = false;
                Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Wait, 10);

                NUT_Background.debugLog.Info("[SETTINGS] Testing connection settings");
                //bool ValidationTest = NUT_Poller.ValidateNUTServer(txtIPAddress.Text, Convert.ToUInt16(txtPort.Text));

                bool ValidationTest = false;
                try
                {

                    Task ValidationTestTask = Task.Factory.StartNew(async () =>
                    {
                        NUT_Background.debugLog.Trace("[SETTINGS] Executing telnet client task");
                        ValidationTest = await NUT_Poller.ValidateNUTServer(txtIPAddress.Text, Convert.ToUInt16(txtPort.Text));
                    });
                    
                    Task.WaitAll(ValidationTestTask);
                    NUT_Background.debugLog.Info(ValidationTestTask.Status.ToString());
                    if (ValidationTestTask.IsFaulted) NUT_Background.debugLog.Fatal(ValidationTestTask.Exception.ToString());
                    ValidationTestTask.Dispose();
                }
                catch (Exception eTest)
                {
                    NUT_Background.debugLog.Fatal("[SETTINGS] An error occurred while trying to test the connection:\n" + eTest + "\n" + e);
                    ValidationTest = false;
                }

                if (ValidationTest)
                {
                    ContentDialog TestDialog = new ContentDialog
                    {
                        //Title = "Server validation successful",
                        Content = "Connection to the NUT server was successful",
                        CloseButtonText = "OK"
                    };
                    TestDialog.ShowAsync();
                }
                else
                {
                    ContentDialog TestDialog = new ContentDialog
                    {
                        //Title = "Server validation failed",
                        Content = "Connection to the NUT server has failed. Please confirm the settings and try again.",
                        CloseButtonText = "OK"
                    };
                    TestDialog.ShowAsync();
                }

                if (PausePoll)
                {
                    NUT_Background.debugLog.Debug("[SETTINGS] Polling has resumed.");
                    NUT_Background.isPolling = true;
                }

                btnConnect.Content = "Test Connection";
                btnConnect.IsEnabled = true;
                TestingRing.IsActive = false;
                Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 10);

            }



        }

        private bool ValidateSettings()
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
                InvalidData.ShowAsync();
                return false;
            }

            // Then validate the port number
            if (!validator.ValidatePort(txtPort.Text))
            {
                ContentDialog InvalidData = new ContentDialog
                {
                    Title = "Invalid Port Number",
                    Content = "The port number that was entered does not appear to be correct. Please correct it and try again. It is typically 3493.",
                    CloseButtonText = "OK"
                };
                InvalidData.ShowAsync();
                return false;
            }

            // Finally we validate the polling frequency
            if (!validator.ValidatePollInterval(txtPollFrequency.Text))
            {
                ContentDialog InvalidData = new ContentDialog
                {
                    Title = "Invalid Poll Interval",
                    Content = "The polling interval does not appear to be correct. It should be 5 seconds at minimum.",
                    CloseButtonText = "OK"
                };
                InvalidData.ShowAsync();
                return false;
            }
            return true;

        }
    }
}
