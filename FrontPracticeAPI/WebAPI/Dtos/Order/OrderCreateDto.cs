using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos.Order;

public record OrderCreateDto(
    [Required] string ProductId,
    [Required, Range(1, int.MaxValue)] int Quantity
);
