using NLog.Extensions.Logging;
using Shared.Middlewares;

namespace Order.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
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

        return app;
    }
}