using MongoDB.Driver;
using Order.Domain.Entities;
using Order.Domain.Interfaces;

namespace Order.Infrastructure.Repositories;

public class OrderRepository : GenericRepository<Domain.Entities.Order>, IOrderRepository
{
    public OrderRepository(IMongoClient mongoClient)
        : base(mongoClient, "OrderDb", "Orders")
    {
    }

    public async Task<Domain.Entities.Order> GetOrderByCustomerIdAsync(string customerId)
    {
        var filter = Builders<Domain.Entities.Order>.Filter.Eq(o => o.CustomerId, customerId);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
}
