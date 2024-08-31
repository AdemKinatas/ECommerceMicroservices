using Basket.Domain.Entities;

namespace Basket.Domain.Interfaces;

public interface IBasketRepository
{
    Task<CustomerBasket> GetBasketAsync(string customerId);
    Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
    Task<bool> DeleteBasketAsync(string customerId);
}
