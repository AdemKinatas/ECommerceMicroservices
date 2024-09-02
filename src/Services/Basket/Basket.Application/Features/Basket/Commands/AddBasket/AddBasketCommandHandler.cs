using Basket.Application.Interfaces;
using Basket.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Basket.Application.Features.Basket.Commands.AddBasket;

public class AddBasketCommandHandler : IRequestHandler<AddBasketCommand, bool>
{
    private readonly IBasketService _basketService;
    private readonly ILogger<AddBasketCommandHandler> _logger;

    public AddBasketCommandHandler(IBasketService basketService, ILogger<AddBasketCommandHandler> logger)
    {
        _basketService = basketService;
        _logger = logger;
    }

    public async Task<bool> Handle(AddBasketCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Command işleniyor: Müşteri ID: {command.CustomerId}");

        var basket = await _basketService.GetBasketAsync(command.CustomerId);

        if (basket == null)
        {
            _logger.LogInformation($"Yeni bir sepet oluşturuluyor: Müşteri ID: {command.CustomerId}");
            basket = new CustomerBasket
            {
                CustomerId = command.CustomerId,
                Items = new List<BasketItem>()
            };
        }

        var basketItem = new BasketItem
        {
            ProductName = command.ProductName,
            Quantity = command.Quantity,
            Price = command.Price
        };

        basket.Items.Add(basketItem);
        _logger.LogInformation($"Ürün sepete eklendi: Ürün: {command.ProductName}, Müşteri ID: {command.CustomerId}");

        await _basketService.UpdateBasketAsync(basket);
        _logger.LogInformation($"Sepet güncellendi.");

        return true;
    }
}
