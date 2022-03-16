using AutoMapper;
using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;

namespace ComissionRateApi.Profiles;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        // Source --> Destination

        CreateMap<Customer, CustomerReadDto>();
        CreateMap<Customer, CustomerWithOrdersReadDto>();
        CreateMap<CustomerUpdateDto, Customer>();
        CreateMap<CustomerCreateDto, Customer>();
    }
}