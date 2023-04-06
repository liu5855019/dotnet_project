
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
    public class LogInterfaceController : ControllerBase
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private readonly ILogInterfaceService service;

        public LogInterfaceController(ILogInterfaceService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<LogInterface> AddLog(string service, string name, string value, string remark)
        {
            var result = await this.service.AddLogAsync(new LogInterface
            { 
                Service = service,
                RequestPath = name,
                RequestPara = value,
                Remark = remark
            });

            logger.Debug($"{LogConsts.End}; AddLog(); result:{result.ToJsonString()}");
            return result;
        }


        [HttpPost]
        public async Task<LogInterface> AddLogInterface(LogInterface logInterface)
        {
            var result = await this.service.AddLogAsync(logInterface);

            logger.Debug($"{LogConsts.End}; AddLog(); result:{result.ToJsonString()}");
            return result;
        }

        [HttpGet]
        public async Task<List<LogInterface>> SearchLog(string service, string name)
        {
            return await this.service.SearchLogAsync(service, name);
        }
    }
}



