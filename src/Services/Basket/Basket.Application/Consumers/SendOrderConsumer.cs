using Basket.Application.Interfaces;
using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messages.Order;

namespace Basket.Application.Consumers;

public class SendOrderConsumer : IConsumer<SendOrderMessage>
{
    private readonly IBasketService _basketService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<SendOrderConsumer> _logger;

    public SendOrderConsumer(IBasketService basketService, IPublishEndpoint publishEndpoint, ILogger<SendOrderConsumer> logger)
    {
        _basketService = basketService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<SendOrderMessage> context)
    {
        var message = context.Message;

        var basket = await _basketService.GetBasketAsync(message.CustomerId);

        if (basket == null)
        {
            _logger.LogWarning("Sepet bulunamadı: Müşteri ID: {CustomerId}", message.CustomerId);
            return;
        }

        var items = basket.Items.Adapt<List<OrderItem>>();

        var orderMessage = new CreateOrderMessage
        {
            CustomerId = message.CustomerId,
            //Items = basket.Items.Select(item => new OrderItem
            //{
            //    ProductName = item.ProductName,
            //    Quantity = item.Quantity,
            //    Price = item.Price
            //}).ToList(),
            Items = items,
            TotalAmount = basket.Items.Sum(item => item.Quantity * item.Price)
        };

        await _publishEndpoint.Publish(orderMessage);

        await _basketService.DeleteBasketAsync(message.CustomerId);

        _logger.LogInformation("Sipariş gönderildi ve sepet silindi: Müşteri ID: {CustomerId}", message.CustomerId);
    }
}
