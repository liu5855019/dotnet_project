namespace DM.Log.Common
{
    using DM.BaseEntity;
    using Newtonsoft.Json;
    using System;

    public static class UtilityFunc
    {


        #region MyRegion
        public static T SetInsertProperties<T>(this T t, RequestInfo requestInfo) where T : BaseEntity
        {
            t.CreateDt = DateTime.Now;
            t.UpdateDt = DateTime.Now;

            t.RequestId = requestInfo?.RequestId ?? 0;
            t.CreateBy = requestInfo?.OperatorId;
            t.UpdateBy = requestInfo?.OperatorId;

            return t;
        }

        public static T SetUpdateProperties<T>(this T t, RequestInfo requestInfo) where T : BaseEntity
        {
            t.UpdateDt = DateTime.Now;

            t.RequestId = requestInfo?.RequestId ?? 0;
            t.UpdateBy = requestInfo?.OperatorId;

            return t;
        }
        #endregion

    }
}
