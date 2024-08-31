namespace Order.Domain.Entities;

public class OrderItem
{
    public string ProductName { get; set; } 
    public int Quantity { get; set; } 
    public decimal Price { get; set; } 
}
