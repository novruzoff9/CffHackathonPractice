using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos.Product;
using WebAPI.ResponseModels;
using WebAPI.Services;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await productService.GetProductsAsync();
        var response = Response<List<ProductReturnDto>>.Success(products, 200);
        return Ok(response);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var product = await productService.GetProductAsync(id);
        var response = Response<ProductReturnDto>.Success(product, 200);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDto createDto)
    {
        var result = await productService.CreateProductAsync(createDto);
        var response = Response<ProductReturnDto>.Success(result, 204);
        return Ok(response);
    }
    [HttpGet("category/{id}")]
    public async Task<IActionResult> GetByCategory(string id)
    {
        var products = await productService.GetProductsOfCategoryAsync(id);
        var response = Response<List<ProductReturnDto>>.Success(products, 200);
        return Ok(response);
    }
}
