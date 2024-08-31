using Order.API;
using Order.Application;
using Order.Infrastructure;
using Shared.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder);

var app = builder.Build();


app.UseApiMiddlewares();

app.Run();