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
    using System.Linq;
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

        public async Task<LogDotaRun> AddLogAsync(LogDotaRun logDotaRun)
        {
            logger.Debug( $"{LogConsts.Start}; AddLogAsync(); logDotaRun:{logDotaRun.ToJsonString()}");
            try
            {
                logDotaRun.DeviceId.CheckPara(nameof(logDotaRun.DeviceId));
                logDotaRun.GroupId.CheckPara(nameof(logDotaRun.GroupId));

                logDotaRun.SetInsertProperties(this.RequestInfo);

                await this.dBContext.AddAsync(logDotaRun);
                var count = await this.dBContext.SaveChangesAsync();

                logger.Debug($"{LogConsts.End}; AddLogAsync(); Count:{count}");
            }
            catch (Exception ex)
            {
                logger.Error(ex, $"{LogConsts.End}; AddLogAsync(); Error:{ex.Message}");
                throw;
            }

            return logDotaRun;
        }

        public async Task<List<LogDotaRun>> SearchLogAsync(string DeviceId, string GroupId)
        {
            DeviceId.CheckPara(nameof(DeviceId));

            var list = await this.dBContext
                                 .LogDotaRun
                                 .AsNoTracking()
                                 .Where(w => w.DeviceId == DeviceId)
                                 .WhereIf(w => w.GroupId == GroupId, !string.IsNullOrWhiteSpace(GroupId))
                                 .ToListAsync();

            logger.Debug($"{LogConsts.End}; AddLogAsync(); Count:{list.Count}");

            return list;
        }

        public async Task<List<string>> GetDeviceListAsync()
        {
            try
            {
                logger.Debug($"{LogConsts.End}; GetDeviceListAsync(); start sql");
                var list = await this.dBContext
                                 .LogDotaRun
                                 .AsNoTracking()
                                 .GroupBy(w => w.DeviceId)
                                 .Select(w => w.Key)
                                 .ToListAsync();

                logger.Debug($"{LogConsts.End}; GetDeviceListAsync(); Count:{list.Count}");
                return list;
            }
            catch (Exception ex)
            {
                logger.Error($"GetDeviceListAsync(); Error:{ex.Message}");
                throw;
            }
        }

        public async Task<List<NameAndCount>> GetGroupListByDeviceIdAsync(string deviceId)
        {
            try
            {
                logger.Debug($"{LogConsts.End}; GetGroupListByDeviceIdAsync(); start sql");
                var list = await this.dBContext
                                    .LogDotaRun
                                    .AsNoTracking()
                                    .Where(w => w.DeviceId == deviceId)
                                    .GroupBy(w => w.GroupId)
                                    .Select(w => new NameAndCount() { Name = w.Key, Count = w.Count() })
                                    .ToListAsync();

                logger.Debug($"{LogConsts.End}; GetGroupListByDeviceIdAsync(); Count:{list.Count}");
                return list;
            }
            catch (Exception ex)
            {
                logger.Error($"GetGroupListByDeviceIdAsync(); Error:{ex.Message}");
                throw;
            }
        }



    }
}