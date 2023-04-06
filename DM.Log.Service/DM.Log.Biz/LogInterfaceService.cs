namespace DM.Log.Biz
{
    using DM.Log.Biz.Interface;
    using DM.Log.Common;
    using DM.Log.Dal;
    using DM.Log.Entity;
    using Microsoft.EntityFrameworkCore;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class LogInterfaceService : BaseService, ILogInterfaceService
    {
        public LogInterfaceService(
            RequestInfo requestInfo,
            LogDBContext dBContext
            ): base(requestInfo, dBContext)
        {
        }

        public async Task<LogInterface> AddLogAsync(LogInterface logInterface)
        {
            logger.Debug($"{LogConsts.Start}; AddLogAsync(); logInterface:{logInterface.ToJsonString()}");
            try
            {
                logInterface.Service.CheckPara(nameof(logInterface.Service));
                logInterface.RequestPath.CheckPara(nameof(logInterface.RequestPath));

                logInterface.SetInsertProperties(this.RequestInfo);

                await this.dBContext.LogInterface.AddAsync(logInterface);
                var count = await this.dBContext.SaveChangesAsync();

                logger.Debug($"{LogConsts.End}; AddLogAsync(); Count:{count}");
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"{LogConsts.End}; AddLogAsync(); Error:{ex.Message}");
                throw;
            }

            return logInterface;
        }


        public async Task<List<LogInterface>> SearchLogAsync(string service, string name)
        {
            var list = await this.dBContext
                                 .LogInterface
                                 .WhereIf(w => w.Service == service, !string.IsNullOrWhiteSpace(service))
                                 .WhereIf(w => w.RequestPath == name, !string.IsNullOrWhiteSpace(name))
                                 .ToListAsync();

            logger.Debug($"{LogConsts.End}; AddLogAsync(); Count:{list.Count}");

            return list;
        }
    }
}