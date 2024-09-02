using Basket.Application.Interfaces;
using Basket.Domain.Entities;
using MediatR;

namespace Basket.Application.Features.Basket.Queries.GetBasket;

public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, CustomerBasket>
{
    private readonly IBasketService _basketService;

    public GetBasketQueryHandler(IBasketService basketService)
    {
        _basketService = basketService;
    }

    public async Task<CustomerBasket> Handle(GetBasketQuery request, CancellationToken cancellationToken)
    {
        var basket = await _basketService.GetBasketAsync(request.CustomerId);
        return basket;
    }
}
