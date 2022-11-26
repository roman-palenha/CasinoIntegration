using CasinoIntegration.API.Middlewares;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using CasinoIntegration.BusinessLayer.DTO.Response;
using CasinoIntegration.BusinessLayer.Logger.Interfaces;

namespace CasinoIntegration.API.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Extension for configuring exception handler
        /// </summary>
        /// <param name="app">Our application builder</param>
        /// <param name="logger">Logger where information will be logged</param>
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }

        public static void ConfigureCustomExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
