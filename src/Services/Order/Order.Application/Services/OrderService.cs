using Order.Application.Interfaces;
using Order.Domain.Interfaces;

namespace Order.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task CreateOrderAsync(Domain.Entities.Order order)
    {
        await _orderRepository.AddAsync(order);
    }

    public async Task<Domain.Entities.Order> GetOrderByIdAsync(Guid orderId)
    {
        return await _orderRepository.GetByIdAsync(orderId);
    }
}