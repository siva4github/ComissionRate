using AutoMapper;
using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;

namespace ComissionRateApi.Profiles;

public class CompanyMappingProfile : Profile
{
    public CompanyMappingProfile()
    {
        // Source --> Destination

        CreateMap<Company, CompanyReadDto>();
    }
}