namespace DM.Log.Biz
{
    using DM.Log.Common;
    using NLog;
    using System;
    using System.Runtime.CompilerServices;

    public class BaseWriteLogService : IRequestInfo
    {
        protected readonly ILogger logger;

        public RequestInfo RequestInfo { get; set; }

        public BaseWriteLogService(
            RequestInfo requestInfo
            )
        {
            this.RequestInfo = requestInfo;
            this.logger = LogManager.GetLogger(this.GetType().Name);
        }


        #region Log
        public void LogTrace(string message,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0) => LogWrite(LogLevel.Trace, message, callerMemberName, callerFilePath, callerLineNumber);

        public void LogDebug(string message,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0) => LogWrite(LogLevel.Debug, message, callerMemberName, callerFilePath, callerLineNumber);

        public void LogInfo(string message,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0) => LogWrite(LogLevel.Info, message, callerMemberName, callerFilePath, callerLineNumber);

        public void LogWarn(string message,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0) => LogWrite(LogLevel.Warn, message, callerMemberName, callerFilePath, callerLineNumber);

        public void LogError(string message,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0) => LogWrite(LogLevel.Error, message, callerMemberName, callerFilePath, callerLineNumber);

        public void LogError(Exception ex, string message,
            [CallerMemberName] string callerMemberName = "",
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = 0) => LogWrite(LogLevel.Error, message, callerMemberName, callerFilePath, callerLineNumber, ex);

        private void LogWrite(LogLevel level, string message, string callerMemberName = "", string callerFilePath = "", int callerLineNumber = 0, Exception ex = null)
        {
            var eventInfo = new LogEventInfo(level, this.logger.Name, message)
            {
                Exception = ex
            };
            eventInfo.Properties["requestid"] = this.RequestInfo.RequestId;
            eventInfo.SetCallerInfo(this.GetType().FullName, callerMemberName, callerFilePath, callerLineNumber);
            this.logger.Log(eventInfo);
        }

        #endregion
    }
}
