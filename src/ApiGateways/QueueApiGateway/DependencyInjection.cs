using NLog.Web;
using QueueApiGateway.Endpoints;
using Shared.Logging;
using Shared.Messages.MassTransit;
using Shared.Middlewares;

namespace QueueApiGateway;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        #region [ MessageBroker ]

        builder.Services.AddMessageBroker(builder.Configuration);

        #endregion

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

        app.MapBasketEndpoints();
        app.MapOrderEndpoints();

        app.UseMiddleware<ErrorHandlingMiddleware>();

        LoggingService.LogInfo("Application started successfully.");

        return app;
    }
}
