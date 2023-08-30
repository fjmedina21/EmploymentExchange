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
                int serverErrorCode = (int)HttpStatusCode.InternalServerError;
                string message = $"{traceId} : {ex.Message}";

                logger.LogError(ex, message);
                context.Response.StatusCode = serverErrorCode;

                APIErrorResponse response = new(
                    serverErrorCode,
                    TraceId: traceId,
                    Message: "Internal Server Error"
                    );

                await context.Response.WriteAsJsonAsync(response);
            }
        }

    }
}
