using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos.Order;
using WebAPI.ResponseModels;
using WebAPI.Services;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderCreateDto orderDto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await orderService.CreateOrderAsync(orderDto, userId!);
        var response = Response<OrderReturnDto>.Success(result, 201);
        return Ok(response);
    }

    [HttpGet("my-orders")]
    public async Task<IActionResult> GetMyOrders()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var orders = await orderService.GetUserOrdersAsync(userId!);
        var response = Response<List<OrderReturnDto>>.Success(orders, 200);
        return Ok(response);
    }

    [HttpGet("all")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await orderService.GetAllOrdersAsync();
        var response = Response<List<OrderReturnDto>>.Success(orders, 200);
        return Ok(response);
    }
}
