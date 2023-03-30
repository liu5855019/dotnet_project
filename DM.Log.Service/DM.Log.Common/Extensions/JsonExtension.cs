namespace DM.Log.Common
{
    using Newtonsoft.Json;

    public static class JsonExtension
    {
        private static readonly JsonSerializerSettings StaticSerializerSettings = new()
        {
            NullValueHandling = NullValueHandling.Ignore, 
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize 
        };

        public static string ToJsonString(
            this object obj, 
            Formatting formatting = Formatting.None,
            JsonSerializerSettings settings = null
            ) => JsonConvert.SerializeObject(obj, formatting, settings ?? StaticSerializerSettings);



        public static T ToJsonObj<T>(this string json) => JsonConvert.DeserializeObject<T>(json);

    }
}
