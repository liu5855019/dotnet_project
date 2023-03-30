
namespace DM.Log.Common
{
    using System;
    using Microsoft.AspNetCore.Http;

    public static class HttpContextAccessorExtension
    {

        public static string GetOpeatorId(this IHttpContextAccessor contextAccessor)
        {
            try
            {
                var operatorId = contextAccessor.HttpContext.User.Identity.Name;
                if (string.IsNullOrEmpty(operatorId))
                {
                    throw new DmException(message: "The caller does not have permission to execute the specified operation.");
                }
                return operatorId;
            }
            catch
            {
                throw new DmException(message: "The caller does not have permission to execute the specified operation.");
            }
        }

        public static string TryGetOpeatorId(this IHttpContextAccessor contextAccessor)
        {
            try
            {
                return GetOpeatorId(contextAccessor);
            }
            catch
            {
                return "DM";
            }
        }


        public static string GetJwtToken(this IHttpContextAccessor contextAccessor)
        {
            try
            {
                var ctx = contextAccessor.HttpContext;
                return ctx.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
            }
            catch
            {
                throw new DmException(message: "No jwt token.");
            }
        }

        public static string TryGetJwtToken(this IHttpContextAccessor contextAccessor)
        {
            try
            {
                return GetJwtToken(contextAccessor);
            }
            catch
            {
                return "";
            }
        }


        public static int GetRequestId(this IHttpContextAccessor contextAccessor)
        {
            try
            {
                var ctx = contextAccessor.HttpContext;
                return int.Parse(ctx.Request.Headers["x-requestid"]);
            }
            catch
            {
                throw new DmException(message: "No request id.");
            }
        }

        public static int TryGetRequestId(this IHttpContextAccessor contextAccessor)
        {
            try
            {
                return GetRequestId(contextAccessor);
            }
            catch
            {
                return new Random().Next();
            }
        }

    }
}
