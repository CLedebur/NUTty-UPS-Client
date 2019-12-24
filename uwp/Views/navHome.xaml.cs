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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nuttyupsclient.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class navHome : Page
    {


        public navHome()
        {
            this.InitializeComponent();

            InitializeValues();

        }

        public void InitializeValues()
        {
            Backend.NUT_Background.debugLog.Trace("[UI:MAIN] Updating statistics text");
            TXTUPSStatus = Backend.NUT_Processor.UPSStatistics();
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
    }
}
