using WebAPI.Contexts;
using WebAPI.Dtos.Category;
using WebAPI.Exceptions;
using WebAPI.Models;

namespace WebAPI.Services;

public interface ICategoryService
{
    Task<CategoryReturnDto> CreateCategoryAsync(CategoryCreateDto createDto);
    Task<CategoryReturnDto> GetCategoryAsync(string id);
    Task<List<CategoryReturnDto>> GetCategoriesAsync();
}


public class CategoryService(ApplicationDbContext dbContext) : ICategoryService
{
    public async Task<CategoryReturnDto> CreateCategoryAsync(CategoryCreateDto createDto)
    {
        var exsistCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Name == createDto.Name);
        if (exsistCategory is not null)
            throw new ConflictException($"Category exsist with name {createDto.Name}");
        var category = new Category(createDto.Name);
        await dbContext.Categories.AddAsync(category);
        await dbContext.SaveChangesAsync();
        var categoryDto = new CategoryReturnDto(category.Id, category.Name);
        return categoryDto;
    }

    public async Task<List<CategoryReturnDto>> GetCategoriesAsync()
    {
        var categories = await dbContext.Categories
            .Select(x => new CategoryReturnDto(x.Id, x.Name))
            .ToListAsync();
        return categories;
    }

    public async Task<CategoryReturnDto> GetCategoryAsync(string id)
    {
        var category = await dbContext.Categories
            .Select(x => new CategoryReturnDto(x.Id, x.Name))
            .FirstOrDefaultAsync(x => x.Id == id);

        if (category == null)
            throw new NotFoundException("Category not found");
        return category;
    }
}
