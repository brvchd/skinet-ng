using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductToReturnDto>()
            .ForMember(
                x => x.ProductBrand,
                o => o.MapFrom(s => s.ProductBrand.Name))
            .ForMember(
                x => x.ProductType,
                o => o.MapFrom(s => s.ProductType.Name))
            .ForMember(
                x => x.PictureUrl,
                o => o.MapFrom<ProductUrlResolver>());
    }
}