
namespace DM.Log.Service.Controllers
{
    using DM.Log.Biz.Interface;
    using DM.Log.Common;
    using DM.Log.Entity;
    using Microsoft.AspNetCore.Mvc;
    using NLog;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]/[action]")]
    public class LogDotaController : ControllerBase
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private readonly IDotaRunService dotaRunService;

        public LogDotaController(IDotaRunService dotaRunService)
        {
            this.dotaRunService = dotaRunService;
        }

        [HttpGet]
        public async Task<LogDotaRun> AddLog(long deviceId, long groupId, bool isShop)
        {
            var result = await this.dotaRunService.AddLogAsync(new LogDotaRun
            {
                DeviceId = deviceId,
                GroupId = groupId,
                IsShop = isShop
            });

            logger.Debug($"{LogConsts.End}; AddLog(); result:{result.ToJsonString()}");
            return result;
        }

        [HttpGet]
        public async Task<List<LogDotaRun>> SearchLog(long deviceId, long groupId)
        {
            return await this.dotaRunService.SearchLogAsync(deviceId, groupId);
        }

    }
}



