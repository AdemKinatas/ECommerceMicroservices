using Mapster;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Interfaces;

namespace Order.Application.Features.Order.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, bool>
{
    private readonly IOrderService _orderService;
    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(IOrderService orderService, ILogger<CreateOrderCommandHandler> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    public async Task<bool> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Sipariş oluşturuluyor: Müşteri ID: {request.CustomerId}");

        var order = request.Adapt<Domain.Entities.Order>();
        
        await _orderService.CreateOrderAsync(order);

        _logger.LogInformation("Sipariş başarıyla kaydedildi: Müşteri ID: {CustomerId}", request.CustomerId);

        return true;
    }
}
