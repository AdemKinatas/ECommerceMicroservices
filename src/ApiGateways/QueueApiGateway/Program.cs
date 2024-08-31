using QueueApiGateway;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices(builder);

var app = builder.Build();

app.UseApiMiddlewares();

app.Run();
