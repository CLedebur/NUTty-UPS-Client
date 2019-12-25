using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Timers;
using Windows.UI.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nuttyupsclient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class navHome : Page
    {

        Timer UpdateUPSStatistics;

        public navHome()
        {
            this.InitializeComponent();

            // Sets up the timer that updates the UPS statistics at the set interval
            UpdateUPSStatistics = new Timer(Backend.NUT_Background.PollFrequency);
            UpdateUPSStatistics.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            UpdateUPSStatistics.AutoReset = true;
            UpdateUPSStatistics.Enabled = true;

            // Updates the initial values
            InitializeValues();

        }


        void OnTimedEvent(Object sender, ElapsedEventArgs e)
        {
            if (Backend.NUT_Background.isPolling)
            {
                Backend.NUT_Background.debugLog.Trace("[UI:HOME] Timer fired");
                InitializeValues();
            }
        }

        public async void InitializeValues()
        {
            Backend.NUT_Background.debugLog.Trace("[UI:HOME] Updating statistics text");
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    try
                    {
                        TXTUPSStatus = (Backend.NUT_Processor.UPSStatistics());
                    }
                    catch (Exception e)
                    {
                        Backend.NUT_Background.debugLog.Fatal("[UI:HOME] Error trying to update the statistics text.\n" + e);

                    }
                }
            );
                
        }


        public string TXTUPSStatus
        {
            get { return (string)GetValue(TXTUPSStatusProperty); }
            set { SetValue(TXTUPSStatusProperty, value); }
        }

        #region TXTUPSStatus DP
        private const string TXTUPSStatusName = "TXTUPSStatus";
        private static readonly DependencyProperty _TXTUPSStatusProperty =
            DependencyProperty.Register(TXTUPSStatusName, typeof(string), typeof(navDebugging), new PropertyMetadata(""));

        public static DependencyProperty TXTUPSStatusProperty { get { return _TXTUPSStatusProperty; } }

        #endregion

        private void Button_ContextCanceled(UIElement sender, RoutedEventArgs args)
        {

        }

        private void BTNPollUPS_Click(object sender, RoutedEventArgs e)
        {
            InitializeValues();
        }
    }
}
