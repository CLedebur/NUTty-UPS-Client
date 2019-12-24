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
    public sealed partial class navDebugging : Page
    {
        public navDebugging()
        {
            this.InitializeComponent();
            InitializeValues();
        }

        public void InitializeValues()
        {
            Backend.NUT_Background.debugLog.Trace("[UI:DEBUGGING] Updating raw output text");
            TXTDebugRawOutput = Backend.NUT_Processor.ParseUPSVariables();
        }

        public string TXTDebugRawOutput
        {
            get { return (string)GetValue(TXTDebugRawOutputProperty); }
            set { SetValue(TXTDebugRawOutputProperty, value); }
        }

        #region TXTDebugRawOutput DP
        private const string TXTDebugRawOutputName = "TXTDebugRawOutput";
        private static readonly DependencyProperty _TXTDebugRawOutputProperty =
            DependencyProperty.Register(TXTDebugRawOutputName, typeof(string), typeof(navDebugging), new PropertyMetadata(""));

        public static DependencyProperty TXTDebugRawOutputProperty { get { return _TXTDebugRawOutputProperty; } }
        #endregion


    }
}
