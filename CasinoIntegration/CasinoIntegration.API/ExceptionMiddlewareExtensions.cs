using CasinoIntegration.API.CasinoIntegrations.Middlewares;
using CasinoIntegration.BusinessLayer.CasinoIntegration.DTO;
using CasinoIntegration.BusinessLayer.CasinoIntegration.Logger.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace CasinoIntegration.API
{
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// Extension for configuring exception handler
        /// </summary>
        /// <param name="app">Our application builder</param>
        /// <param name="logger">Loggere where information will be logged</param>
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
