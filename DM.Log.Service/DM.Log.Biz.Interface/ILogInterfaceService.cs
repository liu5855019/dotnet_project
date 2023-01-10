namespace DM.Log.Biz.Interface
{
    using DM.Log.Common;
    using DM.Log.Entity;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILogInterfaceService : IRequestInfo
    {
        Task<LogInterface> AddLogAsync(LogInterface logInterface);

        Task<List<LogInterface>> SearchLogAsync(string service, string name);
    }
}