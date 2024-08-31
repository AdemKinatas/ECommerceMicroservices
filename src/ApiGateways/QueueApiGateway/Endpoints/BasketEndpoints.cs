using Mapster;
using MassTransit;
using QueueApiGateway.Models;
using Shared.Messages.Basket;

namespace QueueApiGateway.Endpoints;

public static class BasketEndpoints
{
    public static void MapBasketEndpoints(this WebApplication app)
    {
        app.MapPost("/basket/add", async (AddBasketRequest request, IPublishEndpoint publishEndpoint) =>
        {
            var message = request.Adapt<AddBasketMessage>();

            await publishEndpoint.Publish(message);

            return Results.Ok($"Sepete ürün eklendi: {request.ProductName}, Miktar: {request.Quantity}");
        })
        .WithName("AddBasket")
        .Produces(200)
        .Produces(400);
    }
}
