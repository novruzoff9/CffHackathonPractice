using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Dtos.Auth;
using WebAPI.Exceptions;
using WebAPI.Models;

namespace WebAPI.Services;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
}

public class AuthService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IConfiguration configuration) : IAuthService
{
    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        var existingUser = await userManager.FindByEmailAsync(registerDto.Email);
        if (existingUser != null)
            throw new ConflictException("User with this email already exists");

        var user = new ApplicationUser
        {
            Email = registerDto.Email,
            UserName = registerDto.UserName,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new ConflictException($"Registration failed: {errors}");
        }

        await userManager.AddToRoleAsync(user, "User");

        var token = GenerateJwtToken(user, new List<string> { "User" });
        return new AuthResponseDto(token, user.Email!, user.UserName!, user.Id);
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await userManager.FindByEmailAsync(loginDto.Email);
        if (user == null)
            throw new NotFoundException("Invalid email or password");

        var result = await signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded)
            throw new NotFoundException("Invalid email or password");

        var roles = await userManager.GetRolesAsync(user);
        var token = GenerateJwtToken(user, roles.ToList());

        return new AuthResponseDto(token, user.Email!, user.UserName!, user.Id);
    }

    private string GenerateJwtToken(ApplicationUser user, List<string> roles)
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"];
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Name, user.UserName!)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
