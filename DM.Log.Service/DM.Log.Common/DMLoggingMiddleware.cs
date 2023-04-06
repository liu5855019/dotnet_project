using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Log.Common
{
    public class DMLoggingMiddleware
    {
        public static LogDelegate logDelegate;

        private readonly RequestDelegate next;

        public DMLoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            using var ms = new MemoryStream();
            var oldBody = context.Response.Body;
            context.Response.Body = ms;
            var startDate = DateTime.Now;
            try
            {
                await next(context);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                ms.Seek(0, SeekOrigin.Begin);
                using var sr = new StreamReader(ms);
                string body = sr.ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);
                await ms.CopyToAsync(oldBody);
                context.Response.Body = oldBody;

                if (logDelegate != null)
                {
                    _ = logDelegate(new DMHttpContent(context, startDate, body));
                }
            }
        }

    }

    public class DMHttpContent
    {
        public string ConnectId { get; set; }
        public string ServerIpAddress { get; set; }
        public int ServerPort { get; set; }
        public string ClientIpAddress { get; set; }
        public int ClientPort { get; set; }

        public DateTime RequestDt { get; set; }
        public string RequestHost { get; set; }
        public string RequestMethod { get; set; }
        public string RequestPath { get; set; }
        public string RequestBody { get; set; }
        public string RequestHeaders { get; set; }
        public string RequestQueryString { get; set; }

        public DateTime ResponseDt { get; set; }
        public string ResponseHeaders { get; set; }
        public int ResponseStatusCode { get; set; }
        public string ResponseBody { get; set; }

        public DMHttpContent() { }

        public DMHttpContent(HttpContext context, DateTime requestDt, string responseBody = "", DateTime? responseDt = null)
        {
            try
            {
                this.ConnectId = context.Connection.Id;
                this.ServerIpAddress = context.Connection.LocalIpAddress.ToString();
                this.ServerPort = context.Connection.LocalPort;
                this.ClientIpAddress = context.Connection.RemoteIpAddress.ToString();
                this.ClientPort = context.Connection.RemotePort;

                this.RequestDt = requestDt;
                this.RequestHost = context.Request.Host.Host;
                this.RequestMethod = context.Request.Method;
                this.RequestPath = context.Request.Path;
                this.RequestHeaders = context.Request.Headers.ToJsonString();
                this.RequestQueryString = context.Request.QueryString.Value;
                if (context.Request.Body.CanRead)
                {
                    using var sr = new StreamReader(context.Request.Body);
                    var strBody = sr.ReadToEndAsync().Result;
                    this.RequestBody = strBody;
                }

                this.ResponseDt = responseDt ?? DateTime.Now;
                this.ResponseHeaders = context.Response.Headers.ToJsonString();
                this.ResponseStatusCode = context.Response.StatusCode;
                this.ResponseBody = responseBody;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }


    public delegate Task LogDelegate(DMHttpContent content);

}
