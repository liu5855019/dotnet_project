namespace DM.Log.Biz
{
    using DM.Log.Biz.Interface;
    using DM.Log.Common;
    using DM.Log.Dal;
    using DM.Log.Entity;
    using NLog;
    using System.Threading.Tasks;

    public class DotaRunService : IDotaRunService
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly LogDBContext dBContext;

        public RequestInfo RequestInfo { get; set; }

        public DotaRunService(
            LogDBContext dBContext
            )
        {
            this.dBContext = dBContext;
        }

        //public async Task<LogDotaRun> AddLogAsync(LogDotaRun logDotaRun)
        //{
        //    logger.Trace($"{LogConsts.Start}; AddIncidentLogAsync(); incLog:{logDotaRun.ToJsonString()}");
        //    try
        //    {


        //        // 初始化字段
        //        incLog.Id = 0;
        //        incLog.EventDT = DateTime.Now;
        //        incLog.CreationDT = DateTime.Now;
        //        incLog.LastUpdateDT = DateTime.Now;
        //        incLog.OperationId = string.IsNullOrEmpty(incLog.OperationId) ? this.RequestInfo.OperatorId : incLog.OperationId;
        //        incLog.WorkStationId = string.IsNullOrEmpty(incLog.WorkStationId) ? this.RequestInfo.ConsoleId : incLog.WorkStationId;

        //        if (incLog.LogFields?.Any() == true)
        //        {
        //            foreach (var logField in incLog.LogFields)
        //            {
        //                logField.Id = 0;
        //            }
        //        }

        //        await this.incidentLogRepository.AddAsync(incLog);
        //        var count = await this.dBContext.SaveChangesAsync();

        //        logger.Trace(this.RequestInfo.RequestId, $"{LogConsts.LOG_END}; AddIncidentLogAsync(); Count:{count}");
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(this.RequestInfo.RequestId, ex, $"{LogConsts.LOG_END}; AddIncidentLogAsync(); Error:{ex.Message}");
        //        throw;
        //    }

        //    return incLog;
        //}

        //public async Task<List<IncidentLog>> AddIncidentLogsAsync(List<IncidentLog> incLogs)
        //{
        //    logger.Trace(this.RequestInfo.RequestId, $"{LogConsts.LOG_BEGIN}; AddIncidentLogsAsync(); {nameof(IncidentLog)} : {incLogs.ToJsonString()}");
        //    try
        //    {
        //        foreach (var incidentLog in incLogs)
        //        {
        //            incidentLog.IncidentNo.CheckPara(nameof(incidentLog.IncidentNo));
        //            incidentLog.EventCode.CheckPara(nameof(incidentLog.EventCode));
        //            incidentLog.EventDetail.CheckPara(nameof(incidentLog.EventDetail));

        //            var eventCode = this.cacheService.GetFirstOrDefault<SysEventCode>(i => i.EventCode == incidentLog.EventCode);
        //            eventCode.CheckData($"Event Code: {incidentLog.EventCode}");

        //            // 初始化字段
        //            incidentLog.Id = 0;
        //            incidentLog.EventDT = DateTime.Now;
        //            incidentLog.CreationDT = DateTime.Now;
        //            incidentLog.LastUpdateDT = DateTime.Now;
        //            incidentLog.OperationId = string.IsNullOrEmpty(incidentLog.OperationId) ? this.RequestInfo.OperatorId : incidentLog.OperationId;
        //            incidentLog.WorkStationId = string.IsNullOrEmpty(incidentLog.WorkStationId) ? this.RequestInfo.ConsoleId : incidentLog.WorkStationId;

        //            if (incidentLog.LogFields?.Any() == true)
        //            {
        //                foreach (var logField in incidentLog.LogFields)
        //                {
        //                    logField.Id = 0;
        //                }
        //            }

        //            await this.incidentLogRepository.AddAsync(incidentLog);
        //        }

        //        var count = await this.dBContext.SaveChangesAsync();

        //        logger.Trace(this.RequestInfo.RequestId, $"{LogConsts.LOG_END}; AddIncidentLogsAsync(); Count:{count}");
        //        return incLogs;
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(this.RequestInfo.RequestId, ex, $"{LogConsts.LOG_END}; AddIncidentLogsAsync(); Error:{ex.Message}");
        //        throw;
        //    }
        //}

        //public async Task<ResultModel<bool>> TryAddIncidentLogAsync(IncidentLog incLog)
        //{
        //    var result = new ResultModel<bool>();
        //    try
        //    {
        //        _ = await this.AddIncidentLogAsync(incLog);

        //        return result.SetSuccess(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(this.RequestInfo.RequestId, ex, $"{LogConsts.LOG_END}; Error: {ex.Message}");

        //        return result.SetFailure(ex.Message);
        //    }
        //}

        //public async Task<List<IncidentLog>> TryAddIncidentLogsAsync(List<IncidentLog> incLogs)
        //{
        //    try
        //    {
        //        var list = await this.AddIncidentLogsAsync(incLogs);

        //        return list;
        //    }
        //    catch
        //    {
        //        return incLogs;
        //    }
        //}

        //public async Task TryAddIncidentLogFromMessageBus(CommandMessage message)
        //{
        //    this.RequestInfo.RequestId = (int)message.RequestId;
        //    logger.Trace(this.RequestInfo.RequestId, $"{LogConsts.LOG_BEGIN}; TryAddIncidentLogFromMessagebus(); source:{message.Source}");
        //    try
        //    {
        //        var incidentLogProto = new IncidentLogProto();
        //        incidentLogProto.MergeFrom(message.Data);
        //        if (string.IsNullOrEmpty(incidentLogProto.OperationId) && !string.IsNullOrEmpty(message.JWT))
        //        {
        //            incidentLogProto.OperationId = JwtExtension.TryGetOperatorId(message.JWT);
        //        }
        //        if (string.IsNullOrEmpty(incidentLogProto.WorkStationId) && !string.IsNullOrEmpty(message.JWT))
        //        {
        //            incidentLogProto.WorkStationId = JwtExtension.TryGetConsoleId(message.JWT);
        //        }

        //        var incidentLog = incidentLogProto.Adapt<IncidentLog>();

        //        _ = await this.AddIncidentLogAsync(incidentLog);
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(this.RequestInfo.RequestId, ex, $"{LogConsts.LOG_END}; TryAddIncidentLogFromMessagebus(); Error:{ex.Message}");
        //    }
        //}
    }
}