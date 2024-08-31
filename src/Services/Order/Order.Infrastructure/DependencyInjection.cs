using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Order.Domain.Interfaces;
using Order.Infrastructure.Repositories;

namespace Order.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(sp =>
        {
            var mongoUrl = configuration.GetConnectionString("MongoDb");
            return new MongoClient(mongoUrl);
        });

        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}
