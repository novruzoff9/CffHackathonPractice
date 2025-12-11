namespace WebAPI.Models;

public class Order
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public string UserId { get; private set; }
    public ApplicationUser? User { get; private set; }
    public string ProductId { get; private set; }
    public Product? Product { get; private set; }
    public int Quantity { get; private set; }
    public DateTime OrderDate { get; private set; }

    private Order() { }

    public Order(string userId, string productId, int quantity)
    {
        UserId = userId;
        ProductId = productId;
        Quantity = quantity;
        OrderDate = DateTime.UtcNow;
    }
}
