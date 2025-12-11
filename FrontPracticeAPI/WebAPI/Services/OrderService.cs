using Microsoft.EntityFrameworkCore;
using WebAPI.Contexts;
using WebAPI.Dtos.Order;
using WebAPI.Exceptions;
using WebAPI.Models;

namespace WebAPI.Services;

public interface IOrderService
{
    Task<OrderReturnDto> CreateOrderAsync(OrderCreateDto orderDto, string userId);
    Task<List<OrderReturnDto>> GetUserOrdersAsync(string userId);
    Task<List<OrderReturnDto>> GetAllOrdersAsync();
}

public class OrderService(ApplicationDbContext dbContext) : IOrderService
{
    public async Task<OrderReturnDto> CreateOrderAsync(OrderCreateDto orderDto, string userId)
    {
        var product = await dbContext.Products.FindAsync(orderDto.ProductId);
        if (product == null)
            throw new NotFoundException($"Product not found with id {orderDto.ProductId}");

        var user = await dbContext.Users.FindAsync(userId);
        if (user == null)
            throw new NotFoundException("User not found");

        var order = new Order(userId, orderDto.ProductId, orderDto.Quantity);
        await dbContext.Orders.AddAsync(order);
        await dbContext.SaveChangesAsync();

        var totalPrice = product.Price * orderDto.Quantity;

        return new OrderReturnDto(
            order.Id,
            product.Id,
            product.Name,
            order.Quantity,
            totalPrice,
            order.OrderDate,
            user.Email!
        );
    }

    public async Task<List<OrderReturnDto>> GetUserOrdersAsync(string userId)
    {
        var orders = await dbContext.Orders
            .Include(o => o.Product)
            .Include(o => o.User)
            .Where(o => o.UserId == userId)
            .Select(o => new OrderReturnDto(
                o.Id,
                o.Product!.Id,
                o.Product.Name,
                o.Quantity,
                o.Product.Price * o.Quantity,
                o.OrderDate,
                o.User!.Email!
            ))
            .ToListAsync();

        return orders;
    }

    public async Task<List<OrderReturnDto>> GetAllOrdersAsync()
    {
        var orders = await dbContext.Orders
            .Include(o => o.Product)
            .Include(o => o.User)
            .Select(o => new OrderReturnDto(
                o.Id,
                o.Product!.Id,
                o.Product.Name,
                o.Quantity,
                o.Product.Price * o.Quantity,
                o.OrderDate,
                o.User!.Email!
            ))
            .ToListAsync();

        return orders;
    }
}
