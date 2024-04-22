using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductsController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        return Ok(await _productRepository.GetProductsAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _productRepository.GetProductByIdAsync(id);
        return product is not null ? Ok(product) : NotFound();
    }
    
    [HttpGet("brands")]
    public async Task<ActionResult<Product>> GetProductBrands()
    {
        return Ok(await _productRepository.GetProductsBrands());
    }
    
    [HttpGet("types")]
    public async Task<ActionResult<Product>> GetProductTypes()
    {
        return Ok(await _productRepository.GetProductsTypes());
    }
}