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
using MetroLog;
using MetroLog.Targets;
using Windows.Networking;

namespace nuttyupsclient
{   
    public sealed partial class MainPage : Page
    {
        public static ILogger debugLog = LogManagerFactory.DefaultLogManager.GetLogger<MainPage>();

        public MainPage()
        {
            this.InitializeComponent();
            debugLog.Trace("[MAIN] Application starting");

            // Triggers the initialization process
            // Backend.Background.InitializeBg();

            
            debugLog.Trace(Backend.NUT_Poller.PollNUTServer("192.168.253.6", 3493).ToString());
            
            
        }

        

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
