namespace WebAPI.Models;

public class Product(string name, string description, decimal price, string categoryId)
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public string Name { get; private set; } = name;
    public string Description { get; private set; } = description;
    public decimal Price { get; private set; } = price;
    public string CategoryId { get; private set; } = categoryId;
    public Category? Category { get; }
}