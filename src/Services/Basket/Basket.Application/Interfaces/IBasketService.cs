using Basket.Domain.Entities;

namespace Basket.Application.Interfaces;

public interface IBasketService
{
    Task<CustomerBasket> GetBasketAsync(string customerId);
    Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
    Task<bool> DeleteBasketAsync(string customerId);
}