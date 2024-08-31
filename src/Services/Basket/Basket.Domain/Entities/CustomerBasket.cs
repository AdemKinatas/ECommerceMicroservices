namespace Basket.Domain.Entities;

public class CustomerBasket
{
    public string CustomerId { get; set; }
    public List<BasketItem> Items { get; set; } = new List<BasketItem>();
}
