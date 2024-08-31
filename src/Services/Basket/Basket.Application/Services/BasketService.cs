using Basket.Application.Interfaces;
using Basket.Domain.Entities;
using Basket.Domain.Interfaces;

namespace Basket.Application.Services;

public class BasketService : IBasketService
{
    private readonly IBasketRepository _basketRepository;

    public BasketService(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<CustomerBasket> GetBasketAsync(string customerId)
    {
        return await _basketRepository.GetBasketAsync(customerId);
    }

    public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
    {
        return await _basketRepository.UpdateBasketAsync(basket);
    }

    public async Task<bool> DeleteBasketAsync(string customerId)
    {
        return await _basketRepository.DeleteBasketAsync(customerId);
    }
}
