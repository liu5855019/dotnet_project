
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
        public async Task<LogDotaRun> AddLog(long DeviceId, long GroupId, bool IsShop)
        {
            var result = await this.dotaRunService.AddLogAsync(new LogDotaRun
            {
                DeviceId = DeviceId,
                GroupId = GroupId,
                IsShop = IsShop
            });

            logger.Debug($"{LogConsts.End}; AddLog(); result:{result.ToJsonString()}");
            return result;
        }



    }
}



