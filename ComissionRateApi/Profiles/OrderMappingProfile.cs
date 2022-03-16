using AutoMapper;
using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;

namespace ComissionRateApi.Profiles;

public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        // Source --> Destination

        CreateMap<Order, OrderReadDto>().ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name));

        CreateMap<OrderUpdateDto, Order>();
        CreateMap<OrderCreateDto, Order>();
    }
}