namespace Order.Domain.Interfaces;

public interface IOrderRepository : IGenericRepository<Entities.Order>
{
    Task<Entities.Order> GetOrderByCustomerIdAsync(string customerId);
}
