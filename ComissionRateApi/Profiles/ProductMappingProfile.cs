using AutoMapper;
using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;

namespace ComissionRateApi.Profiles;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        // Source --> Destination

        // Source --> Destination

        CreateMap<Product, ProductReadDto>()
        .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Distribution.Company.Name))
        .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.Distribution.Company.Id));
        CreateMap<ProductUpdateDto, Product>();
        CreateMap<ProductCreateDto, Product>();
        
    }
}