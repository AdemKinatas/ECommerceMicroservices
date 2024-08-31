using Mapster;
using MassTransit;
using QueueApiGateway.Models;
using Shared.Messages.Order;

namespace QueueApiGateway.Endpoints;

public static class OrderEndpoints
{
    public static void MapOrderEndpoints(this WebApplication app)
    {
        app.MapPost("/order/send", async (SendOrderRequest request, IPublishEndpoint publishEndpoint) =>
        {
            var message = request.Adapt<SendOrderMessage>();

            await publishEndpoint.Publish(message);

            return Results.Ok($"Sipariş işlemi başlatıldı: Müşteri ID: {request.CustomerId}");
        })
        .WithName("SendOrder")
        .Produces(200)
        .Produces(400);
    }
}