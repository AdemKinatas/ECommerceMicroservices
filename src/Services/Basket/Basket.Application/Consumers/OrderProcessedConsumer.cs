using Basket.Application.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shared.Messages.Basket;

namespace Basket.Application.Consumers;

public class OrderProcessedConsumer : IConsumer<OrderProcessedMessage>
{
    private readonly IBasketService _basketService;
    private readonly ILogger<OrderProcessedConsumer> _logger;

    public OrderProcessedConsumer(IBasketService basketService, ILogger<OrderProcessedConsumer> logger)
    {
        _basketService = basketService;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderProcessedMessage> context)
    {
        var message = context.Message;

        var result = await _basketService.DeleteBasketAsync(message.CustomerId);

        if (result)
        {
            _logger.LogInformation("Sepet başarıyla silindi: Müşteri ID: {CustomerId}", message.CustomerId);
        }
        else
        {
            _logger.LogWarning("Sepet silinemedi: Müşteri ID: {CustomerId}", message.CustomerId);
        }
    }
}
