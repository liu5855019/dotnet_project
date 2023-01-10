﻿namespace DM.Log.Biz
{
    using DM.Log.Biz.Interface;
    using DM.Log.Common;
    using DM.Log.Dal;
    using DM.Log.Entity;
    using Microsoft.EntityFrameworkCore;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LogInterfaceService 
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly LogDBContext dBContext;

        public RequestInfo RequestInfo { get; set; }

        public LogInterfaceService(
            LogDBContext dBContext
            )
        {
            this.dBContext = dBContext;
        }

        //public async Task<LogDotaRun> AddLogAsync(LogInterface logInterface)
        //{
        //    logger.Debug( $"{LogConsts.Start}; AddLogAsync(); incLog:{logDotaRun.ToJsonString()}");
        //    try
        //    {
        //        logDotaRun.DeviceId.CheckNumber(nameof(logDotaRun.DeviceId));
        //        logDotaRun.GroupId.CheckNumber(nameof(logDotaRun.GroupId));

        //        logDotaRun.SetInsertProperties(this.RequestInfo);

        //        await this.dBContext.AddAsync(logDotaRun);
        //        var count = await this.dBContext.SaveChangesAsync();

        //        logger.Debug($"{LogConsts.End}; AddLogAsync(); Count:{count}");
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.Error(ex, $"{LogConsts.End}; AddLogAsync(); Error:{ex.Message}");
        //        throw;
        //    }

        //    return logDotaRun;
        //}

        //public async Task<List<LogDotaRun>> SearchLogAsync(long DeviceId, long GroupId)
        //{
        //    DeviceId.CheckNumber(nameof(DeviceId));

        //    var list = await this.dBContext
        //                         .LogDotaRun
        //                         .Where(w => w.DeviceId == DeviceId)
        //                         .WhereIf(w => w.GroupId == GroupId, GroupId > 0)
        //                         .ToListAsync();

        //    logger.Debug($"{LogConsts.End}; AddLogAsync(); Count:{list.Count}");

        //    return list;
        //}


    }
}