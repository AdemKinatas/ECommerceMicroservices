using MediatR;
using Order.Domain.Entities;

namespace Order.Application.Features.Order.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<bool>
{
    public string CustomerId { get; set; }
    public List<OrderItem> Items { get; set; }
    public decimal TotalAmount { get; set; }
}
