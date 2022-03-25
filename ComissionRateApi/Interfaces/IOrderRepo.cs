using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;
using ComissionRateApi.Helpers;
using ComissionRateApi.Helpers.Params;

namespace ComissionRateApi.Interfaces;

public interface IOrderRepo
{
    Task<PagedList<OrderReadDto>> OrdersAsync(OrderParams orderParams);
    Task CreateAsync(Order order);
    Task<OrderReadDto> OrderAsync(int id);
    Task<IEnumerable<OrderReadDto>> OrderAsync(string shipName);
    Task<IEnumerable<OrderReadDto>> OrderByCustomerAsync(int id);

}