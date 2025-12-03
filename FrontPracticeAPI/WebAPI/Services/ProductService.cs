using WebAPI.Contexts;
using WebAPI.Dtos.Product;
using WebAPI.Exceptions;

namespace WebAPI.Services;

public interface IProductService
{
    Task<ProductReturnDto> CreateProductAsync(ProductCreateDto createDto);
    Task<ProductReturnDto> GetProductAsync(string id);
    Task<List<ProductReturnDto>> GetProductsAsync();
    Task<List<ProductReturnDto>> GetProductsOfCategoryAsync(string Id);
}


public class ProductService(ApplicationDbContext dbContext) : IProductService
{
    public async Task<ProductReturnDto> CreateProductAsync(ProductCreateDto createDto)
    {
        var exsistProduct = await dbContext.Products.FirstOrDefaultAsync(x => x.Name == createDto.Name);
        if (exsistProduct is not null)
            throw new ConflictException($"Product exsist with name {createDto.Name}");
        var product = new Product(createDto.Name, createDto.Description, createDto.Price, createDto.CategoryId);
        await dbContext.Products.AddAsync(product);
        await dbContext.SaveChangesAsync();
        var productDto = new ProductReturnDto(product.Id, product.Name, product.Description, product.Price, product.Category.Name);
        return productDto;
    }

    public async Task<List<ProductReturnDto>> GetProductsAsync()
    {
        var products = await dbContext.Products
            .Include(x => x.Category)
            .Select(x => new ProductReturnDto(x.Id, x.Name, x.Description, x.Price, x.Category.Name))
            .ToListAsync();
        return products;
    }

    public async Task<ProductReturnDto> GetProductAsync(string id)
    {
        var product = await dbContext.Products
            .Include(x => x.Category)
            .Select(x => new ProductReturnDto(x.Id, x.Name, x.Description, x.Price, x.Category.Name))
            .FirstOrDefaultAsync(x => x.Id == id);
        if(product == null)
            throw new NotFoundException("Product not found");
        return product;
    }

    public async Task<List<ProductReturnDto>> GetProductsOfCategoryAsync(string Id)
    {
        var products = await dbContext.Products
            .Include(x => x.Category)
            .Where(x => x.CategoryId == Id)
            .Select(x => new ProductReturnDto(x.Id, x.Name, x.Description, x.Price, x.Category.Name))
            .ToListAsync();
        return products;
    }
}
