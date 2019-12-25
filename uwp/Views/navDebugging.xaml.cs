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
using Windows.UI.Core;
using System.Timers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace nuttyupsclient.Views
{
    public sealed partial class navDebugging : Page
    {
        Timer UPSRawData;
        public navDebugging()
        {
            this.InitializeComponent();

            UPSRawData = new System.Timers.Timer(Backend.NUT_Background.PollFrequency);
            UPSRawData.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            UPSRawData.AutoReset = true;
            UPSRawData.Enabled = true;

            InitializeValues();
        }

        void OnTimedEvent(Object sender, ElapsedEventArgs e)
        {
            Backend.NUT_Background.debugLog.Trace("[UI:DEBUGGING] Timer fired");
            InitializeValues();
        }


        public async void InitializeValues()
        {
            Backend.NUT_Background.debugLog.Trace("[UI:DEBUGGING] Updating raw output text");
            await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                try
                {
                    TXTDebugRawOutput = Backend.NUT_Processor.ParseUPSVariables();
                }
                catch (Exception e)
                {
                    Backend.NUT_Background.debugLog.Fatal("[UI:DEBUGGING] Error trying to update the raw output text.\n" + e);

                }
            }
            );
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
