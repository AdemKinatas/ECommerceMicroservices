namespace Order.Application.Interfaces;

public interface IOrderService
{
    Task CreateOrderAsync(Domain.Entities.Order order);
    Task<Domain.Entities.Order> GetOrderByIdAsync(Guid orderId);
}
