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
    Task<string> RemoveCategoryAsync(string id);
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
            .Where(x => x.Id == id)
            .Select(x => new CategoryReturnDto(x.Id, x.Name))
            .FirstOrDefaultAsync();

        if (category == null)
            throw new NotFoundException("Category not found");
        return category;
    }

    public async Task<string> RemoveCategoryAsync(string id)
    {
        var category = await dbContext.Categories
            .Include(x=>x.Products)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (category == null)
            throw new NotFoundException($"{id}-li Category yoxdur");
        if (category.Products.Count > 0)
            throw new ConflictException("Bu kateqoriyaya aid məhsullar var");
        dbContext.Categories.Remove(category);
        await dbContext.SaveChangesAsync();
        return category.Id;
    }
}
