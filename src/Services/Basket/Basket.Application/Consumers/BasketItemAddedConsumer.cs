using Basket.Application.Interfaces;
using Basket.Domain.Entities;
using Mapster;
using MassTransit;
using Newtonsoft.Json;
using Shared.Messages.Basket;

namespace Basket.Application.Consumers;

public class BasketItemAddedConsumer : IConsumer<AddBasketMessage>
{
    private readonly IBasketService _basketService;

    public BasketItemAddedConsumer(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task Consume(ConsumeContext<AddBasketMessage> context)
    {
        var message = context.Message;

        var basket = await _basketService.GetBasketAsync(message.CustomerId);

        if (basket == null)
        {
            basket = new CustomerBasket
            {
                CustomerId = message.CustomerId,
                Items = new List<BasketItem>()
            };
        }

        var basketItem = message.Adapt<BasketItem>();

        basket.Items.Add(basketItem);

        var cart = await _basketService.UpdateBasketAsync(basket);

        await Console.Out.WriteLineAsync($"Sepet: {JsonConvert.SerializeObject(cart)}");
        await Console.Out.WriteLineAsync($"Ürün sepete eklendi: {message.ProductName}, Miktar: {message.Quantity}, Müşteri ID: {message.CustomerId}");
    }
}
