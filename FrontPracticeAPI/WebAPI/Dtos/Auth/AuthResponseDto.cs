namespace WebAPI.Dtos.Auth;

public record AuthResponseDto(
    string Token,
    string Email,
    string UserName,
    string UserId
);
