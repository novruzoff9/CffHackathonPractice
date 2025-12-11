using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos.Auth;

public record LoginDto(
    [Required, EmailAddress] string Email,
    [Required] string Password
);
