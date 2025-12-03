using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos.Category;
using WebAPI.ResponseModels;
using WebAPI.Services;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categorys = await categoryService.GetCategoriesAsync();
        var response = Response<List<CategoryReturnDto>>.Success(categorys, 200);
        return Ok(response);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var category = await categoryService.GetCategoryAsync(id);
        var response = Response<CategoryReturnDto>.Success(category, 200);
        return Ok(response);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CategoryCreateDto createDto)
    {
        var result = await categoryService.CreateCategoryAsync(createDto);
        var response = Response<CategoryReturnDto>.Success(result, 204);
        return Ok(response);
    }
}
