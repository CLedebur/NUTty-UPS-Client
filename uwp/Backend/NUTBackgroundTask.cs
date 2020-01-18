using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace nuttyupsclient.Backend
{
    class NUTBackgroundTask
    {
        public sealed class PollingTask : IBackgroundTask
        {
            BackgroundTaskDeferral _deferral;
            public async void Run(IBackgroundTaskInstance taskInstance)
            {



                _deferral.Complete();
            }
        }
    }
}
