using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public static class StoreContextSeed
{
    public static async Task SeedDataAsync(this StoreContext context)
    {
        if (!context.ProductBrands.Any())
        {
            var brandsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            context.ProductBrands.AddRange(brands);
        }
        
        if (!context.ProductTypes.Any())
        {
            var productTypesData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/types.json");
            var productTypes = JsonSerializer.Deserialize<List<ProductType>>(productTypesData);
            context.ProductTypes.AddRange(productTypes);
        }
        
        if (!context.Products.Any())
        {
            var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            context.Products.AddRange(products);
        }
        
        if(context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
    }
}