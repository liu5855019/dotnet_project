namespace DM.Log.Biz.Interface
{
    using DM.Log.Common;
    using DM.Log.Entity;
    using System.Threading.Tasks;

    public interface IDotaRunService : IRequestInfo
    {
        Task<LogDotaRun> AddLogAsync(LogDotaRun logDotaRun);
    }
}