using System;
using MetroLog;

namespace nuttyupsclient.Backend
{
    public interface ILoggingServices
    {

        void WriteLine<T>(string message, LogLevel logLevel = LogLevel.Trace, Exception exception = null);

    }
}
