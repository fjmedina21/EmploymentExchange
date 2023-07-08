using EmploymentExchange.Models;
using System.Net;

namespace EmploymentExchange.Middlewares
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
                Guid ErrorId = Guid.NewGuid();
                logger.LogError(ex, $"{ErrorId} : {ex.Message}");
                httpContent.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                
                object error = new { ErrorId, ErrorMessage = "Something went wrong"};
                await httpContent.Response.WriteAsJsonAsync(new APIResponse(error, 500, false));
            }
        }

    }
}
