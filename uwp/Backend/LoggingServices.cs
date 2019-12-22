using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MetroLog;
using MetroLog.Targets;

namespace nuttyupsclient.Backend
{
    public class LoggingServices
    {
        #region Properties

        public static LoggingServices Instance { get; }

        public static int RetainDays { get; } = 30; // 30 days

        public static bool Enabled { get; set; } = true;

        #endregion

        #region Constructors

        static LoggingServices()
        {
            Instance = Instance ?? new LoggingServices();

#if DEBUG

            LogManagerFactory.DefaultConfiguration.AddTarget(LogLevel.Trace, LogLevel.Fatal, new StreamingFileTarget { RetainDays = RetainDays });
#else
            LogManagerFactory.DefaultConfiguration.AddTarget(LogLevel.Info, LogLevel.Fatal, new StreamingFileTarget { RetainDays = RetainDays });
#endif

        }

        #endregion

        #region Public Methods

        public void WriteLine<T>(string message, LogLevel logLevel = LogLevel.Trace, Exception exception = null)
        {
            if (Enabled)
            {
                var logger = LogManagerFactory.DefaultLogManager.GetLogger<T>();

                if (logLevel == LogLevel.Trace && logger.IsTraceEnabled)
                {
                    logger.Trace(message);
                }
                if (logLevel == LogLevel.Debug && logger.IsTraceEnabled)
                {
                    System.Diagnostics.Debug.WriteLine($"{DateTime.Now.TimeOfDay.ToString()} {message}");
                    logger.Debug(message);
                }
                if (logLevel == LogLevel.Error && logger.IsTraceEnabled)
                {
                    logger.Error(message);
                }
                if (logLevel == LogLevel.Fatal && logger.IsTraceEnabled)
                {
                    logger.Fatal(message);
                }
                if (logLevel == LogLevel.Info && logger.IsTraceEnabled)
                {
                    logger.Info(message);
                }
                if (logLevel == LogLevel.Warn && logger.IsTraceEnabled)
                {
                    logger.Warn(message);
                }



            }
        }

        #endregion
    }
}
