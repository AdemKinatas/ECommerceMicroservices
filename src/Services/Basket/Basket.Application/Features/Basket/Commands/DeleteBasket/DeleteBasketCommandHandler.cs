using Basket.Application.Interfaces;
using MediatR;

namespace Basket.Application.Features.Basket.Commands.DeleteBasket;

public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, bool>
{
    private readonly IBasketService _basketService;

    public DeleteBasketCommandHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<bool> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        var result = await _basketService.DeleteBasketAsync(request.CustomerId);
        return result;
    }
}
