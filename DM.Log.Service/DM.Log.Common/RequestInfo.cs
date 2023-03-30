namespace DM.Log.Common
{
    using Microsoft.AspNetCore.Http;

    public class RequestInfo
    {
        public int RequestId { get; set; } = 0;

        public string Jwt { get; set; } = string.Empty;

        public string OperatorId { get; set; } = string.Empty;

        public string ConsoleId { get; set; } = string.Empty;

        public readonly IHttpContextAccessor httpContext;


        public RequestInfo(
            IHttpContextAccessor httpContext
            )
        {
            this.httpContext = httpContext;

            this.Reset();
        }

        private void Reset()
        {
            this.RequestId = this.httpContext.TryGetRequestId();
            this.Jwt = this.httpContext.TryGetJwtToken();
            this.OperatorId = this.httpContext.TryGetOpeatorId();
        }
    }
}
