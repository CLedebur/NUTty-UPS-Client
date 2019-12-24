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

namespace nuttyupsclient
{   
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();

            // Triggers the initialization process
            // Backend.NUT_Background.InitializeBg();


            // Below is for testing purposes only
            /*Tuple<String, bool> NUTData;
            NUTData = Backend.NUT_Poller.PollNUTServer("192.168.253.6",3493);
            Backend.NUT_Processor.ParseNUTOutput(NUTData.Item1);           
            */
            
        }

        public void InitializeValues()
        {
            Backend.NUT_Background.debugLog.Trace("[UI:HOME] Updating battery charge status");
            TXTChargeText = Backend.NUT_Processor.ParseUPSVariables();
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
        /*
        public string TXTChargeColor
        {
            get { return (string)GetValue(TXTChargeColorProperty); }
            set { SetValue(TXTChargeColorProperty, value); }
        }

        #region TXTChargeColor DP
        private const string TXTChargeColorName = "TXTChargeColor";
        private static readonly DependencyProperty _TXTChargeColorProperty =
            DependencyProperty.Register(TXTChargeColorName, typeof(string), typeof(navDebugging), new PropertyMetadata(""));

        public static DependencyProperty TXTChargeColorProperty { get { return _TXTChargeColorProperty; } }
        #endregion
    */

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
