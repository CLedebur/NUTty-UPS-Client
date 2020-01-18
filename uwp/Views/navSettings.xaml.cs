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
using Windows.UI.Core;
using Windows.ApplicationModel.Core;

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
            if (NUTInitialization.NUTConnectionSettings.Item1 == null) txtIPAddress.PlaceholderText = "127.0.0.1";
            else txtIPAddress.Text = NUTInitialization.NUTConnectionSettings.Item1;

            // Port field
            if (NUTInitialization.NUTConnectionSettings.Item2 == 0) txtPort.Text = "3493";
            else txtPort.Text = NUTInitialization.NUTConnectionSettings.Item2.ToString();

            // Poll Interval field
            if (NUTInitialization.NUTConnectionSettings.Item3 == 0) txtPollFrequency.Text = "5";
            else txtPollFrequency.Text = NUTInitialization.NUTConnectionSettings.Item3.ToString();

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

            NUTConfig.SetConfig("IP Address", txtIPAddress.Text);
            NUTConfig.SetConfig("Port", txtPort.Text);
            NUTConfig.SetConfig("Poll Interval", txtPollFrequency.Text);

            // We'll also update the public variable here
            NUTInitialization.NUTConnectionSettings = Tuple.Create(txtIPAddress.Text, Convert.ToUInt16(txtPort.Text), Convert.ToUInt32(txtPollFrequency.Text) * 1000);
            NUTInitialization.NeedConfig = false;

            

            var poller = new NUTPoller();
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
                if (NUTInitialization.isPolling)
                {
                    NUTInitialization.debugLog.Debug("[SETTINGS] Polling is active. Temporarily paused for the duration of this test.");
                    PausePoll = true;
                    NUTInitialization.isPolling = false;
                }

                //Temporarily changing the button and disabling it for the duration of this test
                btnConnect.Content = "Testing...";
                btnConnect.IsEnabled = false;
                Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Wait, 10);

                NUTInitialization.debugLog.Info("[SETTINGS] Testing connection settings");
                //bool ValidationTest = NUTPoller.ValidateNUTServer(txtIPAddress.Text, Convert.ToUInt16(txtPort.Text));

                bool ValidationTest = false;
                try
                {
                    string testIP = txtIPAddress.Text;
                    ushort testPort = Convert.ToUInt16(txtPort.Text);
                    IAsyncAction ValidationTestTask = Windows.System.Threading.ThreadPool.RunAsync(async (workItem) =>
                    {
                        NUTInitialization.debugLog.Trace("[SETTINGS] Executing telnet client task");
                        try
                        {
                            ValidationTest = await NUTPoller.ValidateNUTServer(testIP, testPort).ConfigureAwait(true);
                        }
                        catch (System.AggregateException eAggregate)
                        {
                            ValidationTest = false;
                        }
                    });
                    
                    ValidationTestTask.Completed = new AsyncActionCompletedHandler(async (IAsyncAction asyncInfo, AsyncStatus asyncStatus) =>
                    {
                        await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, new DispatchedHandler(() =>
                         {
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
                                 NUTInitialization.debugLog.Debug("[SETTINGS] Polling has resumed.");
                                 NUTInitialization.isPolling = true;
                             }

                             btnConnect.Content = "Test Connection";
                             btnConnect.IsEnabled = true;
                             TestingRing.IsActive = false;
                             Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 10);
                         }));

                    });

                }
                catch (Exception eTest)
                {
                    NUTInitialization.debugLog.Fatal("[SETTINGS] An error occurred while trying to test the connection:\n" + eTest + "\n" + e);
                    ValidationTest = false;
                }


            }



        }

        private bool ValidateSettings()
        {
            // We're going to validate all the settings first to ensure that they'll work
            var validator = new NUTValidator();

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
