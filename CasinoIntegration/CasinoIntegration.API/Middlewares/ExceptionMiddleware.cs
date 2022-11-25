using System.Net;
using CasinoIntegration.BusinessLayer.DTO.Response;
using CasinoIntegration.BusinessLayer.Logger.Interfaces;

namespace CasinoIntegration.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;
        public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        /// <summary>
        /// Invoke action
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Handle exception
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <param name="exception">Handled exception</param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware."
            }.ToString());
        }
    }
}
