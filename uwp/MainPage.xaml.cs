using nuttyupsclient.Views;
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
using Windows.Networking;
using Windows.UI;
using Windows.UI.Core;
using System.Timers;

namespace nuttyupsclient
{   
    public sealed partial class MainPage : Page
    {
        System.Timers.Timer UpdateUPSCharge;

        public MainPage()
        {
            this.InitializeComponent();

            if (Backend.NUT_Background.NeedConfig)
            {
                // Empty configuration detected, so we're going to hold off on starting the polling process and switch focus to the Settings tab
                contentFrame.Navigate(typeof(navSettings));
            }
            else
            {
                UpdateUPSCharge = new System.Timers.Timer(Backend.NUT_Background.PollFrequency);
                UpdateUPSCharge.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                UpdateUPSCharge.AutoReset = true;
                UpdateUPSCharge.Enabled = true;
            }

            InitializeValues();

        }

        void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (Backend.NUT_Background.isPolling)
            {
                Backend.NUT_Background.debugLog.Trace("[UI:MAIN] Timer fired");
                InitializeValues();
            }
        }


        public async void InitializeValues()
        {
            Backend.NUT_Background.debugLog.Trace("[UI:MAIN] Updating battery charge status");

            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                try
                {
                    if (!Backend.NUT_Background.isPolling)
                    {
                        Backend.NUT_Background.debugLog.Trace("[UI:MAIN] No data from UPS. Will not update charge status.");
                        TXTChargeText = "Not connected to UPS";
                    }
                    else
                    {
                        Backend.NUT_Background.debugLog.Trace("[UI:MAIN] Was able to acquire data. Updating charge status.");
                        Tuple<string, int, double> ChargeStatus = Backend.NUT_Processor.ChargeStatus();
                        TXTChargeText = (ChargeStatus.Item3 + "%, " + ChargeStatus.Item1);
                    }
                }
                catch (Exception e)
                {
                    Backend.NUT_Background.debugLog.Fatal("[UI:MAIN] Error trying to update the charging status.\n" + e);

                }
            }
            );


        }

        public string TXTChargeText
        {
            get { return (string)GetValue(TXTChargeTextProperty); }
            set { SetValue(TXTChargeTextProperty, value); }
        }

        #region TXTChargeText DP
        private const string TXTChargeTextName = "TXTChargeText";
        private static readonly DependencyProperty _TXTChargeTextProperty =
            DependencyProperty.Register(TXTChargeTextName, typeof(string), typeof(navDebugging), new PropertyMetadata(""));

        public static DependencyProperty TXTChargeTextProperty { get { return _TXTChargeTextProperty; } }
        #endregion
        
        public string TXTChargeColor
        {
            get { return (string)GetValue(TXTChargeColorProperty); }
            set { SetValue(TXTChargeColorProperty, value); }
        }

        #region TXTChargeColor DP
        private const string TXTChargeColorName = "TXTChargeColor";
        private static readonly DependencyProperty _TXTChargeColorProperty =
            DependencyProperty.Register(TXTChargeColorName, typeof(Color), typeof(navDebugging), new PropertyMetadata(""));

        public static DependencyProperty TXTChargeColorProperty { get { return _TXTChargeColorProperty; } }
        #endregion
    

        #region NavigationView Event Handlers
        private void nvTopLevelNav_Loaded(object ssender, RoutedEventArgs e)
        {
            foreach(NavigationViewItemBase item in nvTopLevelNav.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "Home_Page")
                {
                    nvTopLevelNav.SelectedItem = item;
                    break;
                }
            }
            contentFrame.Navigate(typeof(navHome));
        }

        private void nvTopLevelNav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {

        }

        private void nvTopLevelNav_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            TextBlock ItemContent = args.InvokedItem as TextBlock;
            if (ItemContent != null)
            {
                switch (ItemContent.Tag)
                {
                    case "Nav_Home":
                        contentFrame.Navigate(typeof(navHome));
                        break;
                    case "Nav_Settings":
                        contentFrame.Navigate(typeof(navSettings));
                        break;
                    case "Nav_Debugging":
                        contentFrame.Navigate(typeof(navDebugging));
                        break;
                }
            }
        }

        #endregion

    }
}
