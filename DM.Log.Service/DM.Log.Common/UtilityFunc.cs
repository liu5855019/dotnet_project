namespace DM.Log.Common
{
    using DM.BaseEntity;
    using Newtonsoft.Json;
    using System;

    public static class UtilityFunc
    {
        #region Json

        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ReferenceLoopHandling = ReferenceLoopHandling.Serialize };

        public static string ToJsonString(this object obj, Formatting formatting = Formatting.None) => JsonConvert.SerializeObject(obj, formatting, JsonSerializerSettings);

        public static T ToJsonObj<T>(this string json) => JsonConvert.DeserializeObject<T>(json);

        #endregion

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
