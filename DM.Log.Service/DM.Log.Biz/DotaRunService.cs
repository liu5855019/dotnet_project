namespace DM.Log.Biz
{
    using DM.Log.Biz.Interface;
    using DM.Log.Common;
    using DM.Log.Dal;
    using DM.Log.Entity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class DotaRunService : BaseService, IDotaRunService
    {
        public DotaRunService(
            RequestInfo requestInfo,
            LogDBContext dBContext
            ) : base( requestInfo, dBContext ) 
        { 
        
        }

        public async Task<LogDotaRun> AddLogAsync(LogDotaRun logDotaRun)
        {
            LogInfo($"{LogConsts.Start}; AddLogAsync(); logDotaRun:{logDotaRun.ToJsonString()}");
            try
            {
                logDotaRun.DeviceId.CheckPara(nameof(logDotaRun.DeviceId));
                logDotaRun.GroupId.CheckPara(nameof(logDotaRun.GroupId));

                logDotaRun.SetInsertProperties(this.RequestInfo);

                await this.dBContext.AddAsync(logDotaRun);
                var count = await this.dBContext.SaveChangesAsync();

                LogInfo($"{LogConsts.End}; AddLogAsync(); Count:{count}");
            }
            catch (Exception ex)
            {
                LogError(ex, $"{LogConsts.End}; AddLogAsync(); Error:{ex.Message}");
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
                                 .OrderBy(w => w.CreateDt)
                                 .ToListAsync();

            logger.Debug($"{LogConsts.End}; AddLogAsync(); Count:{list.Count}");

            return list;
        }

        public async Task<Response<List<string>>> GetDeviceListAsync()
        {
            var response = new Response<List<string>>();
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
                return response.SetSuccess(list);
            }
            catch (Exception ex)
            {
                logger.Error($"GetDeviceListAsync(); Error:{ex.Message}");
                return response.SetFailed(ex.Message);
            }
        }

        public async Task<Response<List<NameAndCount>>> GetGroupListByDeviceIdAsync(string deviceId)
        {
            var response = new Response<List<NameAndCount>>();
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
                return response.SetSuccess(list);
            }
            catch (Exception ex)
            {
                logger.Error($"GetGroupListByDeviceIdAsync(); Error:{ex.Message}");
                return response.SetFailed(ex.Message);
            }
        }



    }
}