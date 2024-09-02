using Basket.Application.Features.Basket.Queries.GetBasket;
using Basket.Application.Interfaces;
using Mapster;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Shared.Messages.Order;

namespace Basket.Application.Consumers;

public class SendOrderConsumer : IConsumer<SendOrderMessage>
{
    private readonly IBasketService _basketService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMediator _mediator;
    private readonly ILogger<SendOrderConsumer> _logger;

    public SendOrderConsumer(IBasketService basketService, IPublishEndpoint publishEndpoint, ILogger<SendOrderConsumer> logger, IMediator mediator)
    {
        _basketService = basketService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<SendOrderMessage> context)
    {
        var message = context.Message;

        var query = message.Adapt<GetBasketQuery>();
        var basket = await _mediator.Send(query);

        if (basket == null)
        {
            _logger.LogWarning($"Sepet bulunamadı: Müşteri ID: {message.CustomerId}");
            return;
        }

        _logger.LogInformation($"Sepet alındı: Müşteri ID: {message.CustomerId}, Cart: {JsonConvert.SerializeObject(basket)}");

        var items = basket.Items.Adapt<List<OrderItem>>();
        _logger.LogInformation($"Sepet öğeleri {items.Count} adet sipariş öğesine dönüştürüldü.");

        var orderMessage = new CreateOrderMessage
        {
            CustomerId = message.CustomerId,
            Items = items,
            TotalAmount = basket.Items.Sum(item => item.Quantity * item.Price)
        };

        await _publishEndpoint.Publish(orderMessage);

        await _basketService.DeleteBasketAsync(message.CustomerId);

        _logger.LogInformation($"Sipariş gönderildi ve sepet silindi: Müşteri ID: {message.CustomerId}");
    }
}
