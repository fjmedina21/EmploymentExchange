using EmploymentExchangeAPI.Models;
using System.Net;

namespace EmploymentExchangeAPI.Middlewares
{
    public class GlobalErrorHandler
    {
        private readonly ILogger<GlobalErrorHandler> logger;
        private readonly RequestDelegate next;

        public GlobalErrorHandler(ILogger<GlobalErrorHandler> logger, RequestDelegate next)
        {
            this.logger = logger;
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContent)
        {
            try
            {
                await next(httpContent);
            }
            catch (Exception ex)
            {
                Guid TraceId = Guid.NewGuid();
                logger.LogError(ex, $"{TraceId} : {ex.Message}");
                httpContent.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                object error = new { TraceId, ErrorMessage = "Internal Error" };

                APIResponse response = new()
                {
                    Ok = false,
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Total = 1,
                    Data = error
                };

                await httpContent.Response.WriteAsJsonAsync(response);
            }
        }

    }
}
