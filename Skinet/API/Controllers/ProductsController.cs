using API.DTOs;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IGenericRepository<ProductBrand> _productBrandRepo;
    private readonly IGenericRepository<ProductType> _productTypeRepo;
    private readonly IGenericRepository<Product> _productRepo;
    private readonly IMapper _mapper;

    public ProductsController(
        IGenericRepository<Product> productRepo,
        IGenericRepository<ProductType> productTypeRepo,
        IGenericRepository<ProductBrand> productBrandRepo,
        IMapper mapper)
    {
        _productRepo = productRepo ?? throw new NullReferenceException();
        _productTypeRepo = productTypeRepo ?? throw new NullReferenceException();
        _productBrandRepo = productBrandRepo ?? throw new NullReferenceException();
        _mapper = mapper ?? throw new NullReferenceException();
    }

    [HttpGet]
    public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
    {
        var spec = new ProductsWithTypesAndBrandsSpecifications();
        var products = await _productRepo.ListAsync(spec);
        return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecifications(id);
        var product = await _productRepo.GetEntityWithSpecAsync(spec);
        if (product is null) return NotFound(new ApiResponse(StatusCodes.Status404NotFound)); 
        return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
    }

    [HttpGet("brands")]
    public async Task<ActionResult<Product>> GetProductBrands()
    {
        return Ok(await _productBrandRepo.ListAllAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<Product>> GetProductTypes()
    {
        return Ok(await _productTypeRepo.ListAllAsync());
    }
}