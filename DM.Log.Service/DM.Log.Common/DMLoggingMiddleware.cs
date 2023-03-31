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
        private readonly RequestDelegate next;

        public DMLoggingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            await next(httpContext);

            Stream responseBody = httpContext.Response.Body;

            using (StreamReader reader = new StreamReader(responseBody))
            {
                string bodyText = await reader.ReadToEndAsync();

                // Reset
                responseBody.Seek(0, SeekOrigin.Begin);

                Console.WriteLine(bodyText);
            }
        }
    }
}
