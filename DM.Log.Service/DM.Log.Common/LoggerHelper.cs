namespace DM.Log.Common
{
    using System;
    using NLog;

    public static class LoggerHelper
    {
        public static void TestLog(this ILogger logger)
        {
            logger.Trace(1, "Trace log");
            logger.Debug(2, "Debug log");
            logger.Info(3, "Info log {requestid}");
            logger.Warn("Warn log", (long)4);
            logger.Error("Error log", 5);
        }

        public static void Log(this ILogger logger, LogLevel level, string message, long requestId, Exception ex = null)
        {
            var eventInfo = new LogEventInfo(level, "", message ?? string.Empty);

            eventInfo.Properties["requestid"] = requestId;
            eventInfo.Exception = ex;

            logger?.Log(typeof(LoggerHelper), eventInfo);
        }

        public static void Trace(this ILogger logger, long requestId, string message)
        {
            Log(logger, LogLevel.Trace, message, requestId);
        }

        public static void Debug(this ILogger logger, long requestId, string message)
        {
            Log(logger, LogLevel.Debug, message, requestId);
        }

        public static void Info(this ILogger logger, long requestId, string message)
        {
            Log(logger, LogLevel.Info, message, requestId);
        }

        public static void Warn(this ILogger logger, long requestId, string message)
        {
            Log(logger, LogLevel.Warn, message, requestId);
        }

        public static void Error(this ILogger logger, long requestId, Exception ex, string message)
        {
            Log(logger, LogLevel.Error, message, requestId, ex);
        }

        public static void Error(this ILogger logger, long requestId, string message)
        {
            Log(logger, LogLevel.Error, message, requestId, null);
        }
    }
}
