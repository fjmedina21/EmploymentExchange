using EmploymentExchangeAPI.Models;
using System.Net;

namespace EmploymentExchangeAPI.Middlewares
{
    public class GlobalErrorHandler : IMiddleware
    {
        private readonly ILogger<GlobalErrorHandler> logger;

        public GlobalErrorHandler(ILogger<GlobalErrorHandler> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                Guid traceId = Guid.NewGuid();
                int statusCode = (int)HttpStatusCode.InternalServerError;
                string message = $"{traceId} : {ex.Message}";

                logger.LogError(ex, message);
                context.Response.StatusCode = statusCode;

                object error = new { traceId, message = "Internal Server Error" };
                APIErrorResponse response = new(statusCode, error);

                await context.Response.WriteAsJsonAsync(response);
            }
        }

    }
}
