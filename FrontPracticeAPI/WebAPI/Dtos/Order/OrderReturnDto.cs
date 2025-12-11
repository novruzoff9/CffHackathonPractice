namespace WebAPI.Dtos.Order;

public record OrderReturnDto(
    string Id,
    string ProductId,
    string ProductName,
    int Quantity,
    decimal TotalPrice,
    DateTime OrderDate,
    string UserEmail
);
