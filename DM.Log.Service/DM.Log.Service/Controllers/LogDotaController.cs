
namespace DM.Log.Service.Controllers
{
    using DM.Log.Biz.Interface;
    using DM.Log.Common;
    using DM.Log.Entity;
    using Microsoft.AspNetCore.Mvc;
    using NLog;
    using System.Collections.Generic;
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
        public async Task<LogDotaRun> AddLog(string deviceId, string groupId, bool isShop)
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
        public async Task<List<LogDotaRun>> SearchLog(string deviceId, string groupId)
        {
            return await this.dotaRunService.SearchLogAsync(deviceId, groupId);
        }

        [HttpGet]
        public async Task<Response<List<string>>> GetDeviceList()
        {
            return await this.dotaRunService.GetDeviceListAsync();
        }

        [HttpGet]
        public async Task<Response<List<NameAndCount>>> GetGroupListByDeviceId(string deviceId)
        { 
            return await this.dotaRunService.GetGroupListByDeviceIdAsync(deviceId);
        }

    }
}



