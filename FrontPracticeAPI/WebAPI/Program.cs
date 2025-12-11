using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using WebAPI.Contexts;
using WebAPI.Middlewares;
using WebAPI.Models;
using WebAPI.ResponseModels;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCors", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("default")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception is SecurityTokenExpiredException)
            {
                context.NoResult();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                context.Response.Headers["Token-Expired"] = "true";

                var response = Response<string>.Fail("Token expired", 401);

                var json = System.Text.Json.JsonSerializer.Serialize(response);

                return context.Response.WriteAsync(json);
            }
            if (context.Exception is SecurityTokenException)
            {
                context.NoResult();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                var response = Response<string>.Fail("Invalid token", 401);
                var json = System.Text.Json.JsonSerializer.Serialize(response);
                return context.Response.WriteAsync(json);
            }
            if (context.Exception != null)
            {
                context.NoResult();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                var response = Response<string>.Fail("Authentication failed", 401);
                var json = System.Text.Json.JsonSerializer.Serialize(response);
                return context.Response.WriteAsync(json);
            }
            return Task.CompletedTask;
        },
        OnChallenge = context =>
        {
            // Default davran??? l??v etm?k laz?md?r
            context.HandleResponse();

            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var response = Response<string>.Fail("Unauthorized – token is missing", 401);
            var json = System.Text.Json.JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(json);
        },
        OnForbidden = context =>
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";

            var response = Response<string>.Fail(
                "You do not have permission to access this resource.", 403
                );

            var json = System.Text.Json.JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(json);
        }
    };
});

builder.Services.AddAuthorization();

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrderService, OrderService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // Seed Roles
    var roles = new[] { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Seed Admin User
    var adminEmail = "admin@test.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            Email = adminEmail,
            UserName = "admin",
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(adminUser, "Admin123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }

    // Seed Regular User
    var userEmail = "user@test.com";
    var regularUser = await userManager.FindByEmailAsync(userEmail);
    if (regularUser == null)
    {
        regularUser = new ApplicationUser
        {
            Email = userEmail,
            UserName = "user",
            EmailConfirmed = true
        };
        var result = await userManager.CreateAsync(regularUser, "User123");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(regularUser, "User");
        }
    }
}

app.UseCors("AllowCors");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
