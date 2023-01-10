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

    public class LogInterfaceService : ILogInterfaceService
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

        public async Task<LogInterface> AddLogAsync(LogInterface logInterface)
        {
            logger.Debug($"{LogConsts.Start}; AddLogAsync(); logInterface:{logInterface.ToJsonString()}");
            try
            {
                logInterface.Service.CheckPara(nameof(logInterface.Service));
                logInterface.Name.CheckPara(nameof(logInterface.Name));

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
                                 .WhereIf(w => w.Name == name, !string.IsNullOrWhiteSpace(name))
                                 .ToListAsync();

            logger.Debug($"{LogConsts.End}; AddLogAsync(); Count:{list.Count}");

            return list;
        }
    }
}