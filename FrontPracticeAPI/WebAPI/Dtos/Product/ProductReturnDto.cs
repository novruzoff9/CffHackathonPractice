namespace WebAPI.Dtos.Product;

public record ProductReturnDto(string Id, string Name, string Description, decimal Price, string CategoryName);
public record ProductCreateDto(string Name, string Description, decimal Price, string CategoryId);
