using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos.Auth;
using WebAPI.ResponseModels;
using WebAPI.Services;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var result = await authService.RegisterAsync(registerDto);
        var response = Response<AuthResponseDto>.Success(result, 201);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var result = await authService.LoginAsync(loginDto);
        var response = Response<AuthResponseDto>.Success(result, 200);
        return Ok(response);
    }
}
