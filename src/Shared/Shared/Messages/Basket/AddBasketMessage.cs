namespace Shared.Messages.Basket;

public class AddBasketMessage
{
    public string CustomerId { get; set; }
    public string ProductName { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
