using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos.Auth;

public record RegisterDto(
    [Required, EmailAddress] string Email,
    [Required, MinLength(6)] string Password,
    [Required] string UserName
);
