using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;
using Order.Application.Interfaces;
using Shared.Messages.Basket;
using Shared.Messages.Order;

namespace Order.Application.Consumers;

public class OrderCreatedConsumer : IConsumer<CreateOrderMessage>
{
    private readonly IOrderService _orderService; 
    private readonly IPublishEndpoint _publishEndpoint; 
    private readonly ILogger<OrderCreatedConsumer> _logger; 

    public OrderCreatedConsumer(IOrderService orderService, IPublishEndpoint publishEndpoint, ILogger<OrderCreatedConsumer> logger)
    {
        _orderService = orderService;
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<CreateOrderMessage> context)
    {
        var message = context.Message;

        var items = message.Items.Adapt<List<Domain.Entities.OrderItem>>();
        var order = new Domain.Entities.Order
        {
            CustomerId = message.CustomerId,
            //Items = message.Items.Select(item => new Domain.Entities.OrderItem
            //{
            //    ProductName = item.ProductName,
            //    Quantity = item.Quantity,
            //    Price = item.Price
            //}).ToList(),
            Items = items,
            TotalAmount = message.TotalAmount
        };

        await _orderService.CreateOrderAsync(order);

        _logger.LogInformation("Sipariş başarıyla kaydedildi: Müşteri ID: {CustomerId}", message.CustomerId);

        var orderProcessedMessage = new OrderProcessedMessage
        {
            CustomerId = message.CustomerId
        };

        await _publishEndpoint.Publish(orderProcessedMessage);
    }
}
