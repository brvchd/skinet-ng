using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext _dbContext;
    public ProductRepository(StoreContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Product> GetProductByIdAsync(int id)
    {
        var product = await _dbContext.Products
            .Include(e => e.ProductType)
            .Include(e => e.ProductBrand)
            .FirstOrDefaultAsync(e => e.Id == id);
        return product;
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await _dbContext.Products
            .Include(e => e.ProductType)
            .Include(e => e.ProductBrand)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<ProductBrand>> GetProductsBrands()
    {
        return await _dbContext.ProductBrands.ToListAsync();
    }

    public async Task<IReadOnlyList<ProductType>> GetProductsTypes()
    {
        return await _dbContext.ProductTypes.ToListAsync();
    }
}