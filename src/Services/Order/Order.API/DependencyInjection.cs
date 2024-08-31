using NLog.Web;
using Shared.Logging;
using Shared.Middlewares;

namespace Order.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        #region [ NLOG ]

        builder.Logging.ClearProviders();
        builder.Logging.SetMinimumLevel(LogLevel.Trace);
        builder.Host.UseNLog();

        #endregion

        #region [ Swagger ]

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        #endregion 

        return services;
    }

    public static WebApplication UseApiMiddlewares(this WebApplication app) 
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<ErrorHandlingMiddleware>();

        LoggingService.LogInfo("Application started successfully.");

        return app;
    }
}