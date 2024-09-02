using MediatR;

namespace Basket.Application.Features.Basket.Commands.AddBasket;

public class AddBasketCommand : IRequest<bool>
{
    public string CustomerId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

