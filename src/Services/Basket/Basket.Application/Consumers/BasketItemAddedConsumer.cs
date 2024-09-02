using Basket.Application.Features.Basket.Commands.AddBasket;
using Mapster;
using MassTransit;
using MediatR;
using Shared.Messages.Basket;

namespace Basket.Application.Consumers;

public class BasketItemAddedConsumer : IConsumer<AddBasketMessage>
{
    private readonly IMediator _mediator;

    public BasketItemAddedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<AddBasketMessage> context)
    {
        var message = context.Message;

        var command = message.Adapt<AddBasketCommand>();

        await _mediator.Send(command);
    }
}
