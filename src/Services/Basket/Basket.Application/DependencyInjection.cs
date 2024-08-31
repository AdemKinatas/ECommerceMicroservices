﻿using Basket.Application.Interfaces;
using Basket.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Messages.MassTransit;
using System.Reflection;

namespace Basket.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
        services.AddScoped<IBasketService, BasketService>();

        return services;
    }
}
