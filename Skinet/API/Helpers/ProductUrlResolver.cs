using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
{
    private readonly IConfiguration _configuration;

    public ProductUrlResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Resolve(
        Product source,
        ProductToReturnDto destination,
        string destMember,
        ResolutionContext context)
    {
        var launchUrl = _configuration["ApiUrl"];
        if (!string.IsNullOrEmpty(source.PictureUrl) && !string.IsNullOrEmpty(launchUrl))
        {
            return launchUrl + source.PictureUrl;
        }

        return null;
    }
}