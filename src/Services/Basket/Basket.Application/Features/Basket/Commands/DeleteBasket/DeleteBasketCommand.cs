using MediatR;

namespace Basket.Application.Features.Basket.Commands.DeleteBasket;
public class DeleteBasketCommand : IRequest<bool>
{
    public string CustomerId { get; set; }
}
