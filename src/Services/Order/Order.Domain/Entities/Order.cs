using Order.Domain.Common;

namespace Order.Domain.Entities;

public class Order : MongoBaseModel
{
    public string CustomerId { get; set; } 
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public List<OrderItem> Items { get; set; } = new();
}
