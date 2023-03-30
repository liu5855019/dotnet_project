namespace DM.Log.Biz
{
    using DM.Log.Common;
    using DM.Log.Dal;

    public class BaseService : BaseWriteLogService
    {
        protected readonly LogDBContext dBContext;

        public BaseService(
            RequestInfo requestInfo,
            LogDBContext logDBContext
            ) : base(requestInfo)
        {
            this.dBContext = logDBContext;
        }



        #region TryCatch

        //public ResultModel<TResult> TryCatch<TResult>(Func<TResult> action)
        //{
        //    var result = new ResultModel<TResult>();
        //    try
        //    {
        //        var t = action();
        //        result.SetSuccess(t);
        //    }
        //    catch (FgmsException ex)
        //    {
        //        result.SetFailure(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (DBContext.Database.CurrentTransaction != null)
        //        {
        //            DBContext.RollbackTransaction();
        //        }
        //        LogError(ex, ex.Message);
        //        result.SetFailure(ex.Message);
        //    }
        //    return result;
        //}


        //public async Task<ResultModel<TResult>> TryCatchAsync<TResult>(Func<Task<TResult>> action)
        //{
        //    var result = new ResultModel<TResult>();
        //    try
        //    {
        //        var t = await action();
        //        result.SetSuccess(t);
        //    }
        //    catch (FgmsException ex)
        //    {
        //        result.SetFailure(ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (DBContext.Database.CurrentTransaction != null)
        //        {
        //            DBContext.RollbackTransaction();
        //        }
        //        LogError(ex, ex.Message);
        //        result.SetFailure(ex.Message);
        //    }
        //    return result;
        //}
        #endregion
    }
}
