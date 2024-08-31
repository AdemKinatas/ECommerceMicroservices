using Basket.Domain.Entities;
using Basket.Domain.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Basket.Infrastructure.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _database;

    public BasketRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<CustomerBasket> GetBasketAsync(string customerId)
    {
        var data = await _database.StringGetAsync(customerId);

        if (data.IsNullOrEmpty)
            return null;

        return JsonSerializer.Deserialize<CustomerBasket>(data);
    }

    public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
    {
        var created = await _database.StringSetAsync(basket.CustomerId, JsonSerializer.Serialize(basket), TimeSpan.FromMinutes(2)); // TTL 2 min

        if (!created)
            return null;

        return await GetBasketAsync(basket.CustomerId);
    }

    public async Task<bool> DeleteBasketAsync(string customerId)
    {
        return await _database.KeyDeleteAsync(customerId);
    }
}
