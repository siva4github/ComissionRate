
using AutoMapper;
using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;

namespace ComissionRateApi.Profiles;

public class DistributionMappingProfile : Profile
{
    public DistributionMappingProfile()
    {
        // Source --> Destination

        CreateMap<Distribution, DistributionReadDto>();
        CreateMap<DistributionUpdateDto, Distribution>();
        CreateMap<DistributionCreateDto, Distribution>();

    }
}