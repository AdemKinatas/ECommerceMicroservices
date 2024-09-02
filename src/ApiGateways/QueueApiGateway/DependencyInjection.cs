using NLog.Extensions.Logging;
using QueueApiGateway.Endpoints;
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

        builder.Services.AddLogging(logging =>
        {
            logging.ClearProviders();
            logging.SetMinimumLevel(LogLevel.Trace);
        });

        builder.Services.AddSingleton<ILoggerProvider, NLogLoggerProvider>();

        #endregion

        #region [ Swagger ]

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        #endregion

        #region [ CORS ]

        builder.Services.AddCors(opt => 
        {
            opt.AddPolicy("gwSecretKey", builder => 
            {
                builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
            });
        });

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

        app.UseCors("gwSecretKey");

        app.MapBasketEndpoints();
        app.MapOrderEndpoints();

        app.UseMiddleware<ErrorHandlingMiddleware>();

        return app;
    }
}
