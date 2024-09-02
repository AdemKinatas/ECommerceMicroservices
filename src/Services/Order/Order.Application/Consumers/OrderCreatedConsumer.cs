using Mapster;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Features.Order.Commands.CreateOrder;
using Order.Application.Interfaces;
using Shared.Messages.Basket;
using Shared.Messages.Order;

namespace Order.Application.Consumers;

public class OrderCreatedConsumer : IConsumer<CreateOrderMessage>
{
    private readonly IOrderService _orderService; 
    private readonly IPublishEndpoint _publishEndpoint; 
    private readonly IMediator _mediator;
    private readonly ILogger<OrderCreatedConsumer> _logger;

    public OrderCreatedConsumer(IOrderService orderService, IPublishEndpoint publishEndpoint, ILogger<OrderCreatedConsumer> logger, IMediator mediator)
    {
        _orderService = orderService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<CreateOrderMessage> context)
    {
        var message = context.Message;

        var command = message.Adapt<CreateOrderCommand>();

        var result = await _mediator.Send(command);

        if (!result)
        {
            _logger.LogError($"Sipariş kaydedilirken hata oluştu: Müşteri ID: {message.CustomerId}");
        }

        var orderProcessedMessage = new OrderProcessedMessage
        {
            CustomerId = message.CustomerId
        };

        await _publishEndpoint.Publish(orderProcessedMessage);
    }
}
