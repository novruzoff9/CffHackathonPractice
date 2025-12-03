namespace WebAPI.Models;

public class Category(string name)
{
    public string Id { get; init; } = Guid.NewGuid().ToString();
    public string Name { get; private set; } = name;
    public ICollection<Product>? Products { get; private set; }
}