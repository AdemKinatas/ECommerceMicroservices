using Basket.Application.Features.Basket.Commands.DeleteBasket;
using Mapster;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Messages.Basket;

namespace Basket.Application.Consumers;

public class OrderProcessedConsumer : IConsumer<OrderProcessedMessage>
{
    private readonly ILogger<OrderProcessedConsumer> _logger;
    private readonly IMediator _mediator;

    public OrderProcessedConsumer(ILogger<OrderProcessedConsumer> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<OrderProcessedMessage> context)
    {
        var message = context.Message;

        var command = message.Adapt<DeleteBasketCommand>();
        var result = await _mediator.Send(command);

        if (result)
            _logger.LogInformation("Sepet başarıyla silindi: Müşteri ID: {CustomerId}", message.CustomerId);
        else
            _logger.LogWarning("Sepet silinemedi: Müşteri ID: {CustomerId}", message.CustomerId);
    }
}
