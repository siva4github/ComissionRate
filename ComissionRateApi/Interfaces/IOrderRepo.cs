using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;

namespace ComissionRateApi.Interfaces;

public interface IOrderRepo
{
    Task<IEnumerable<OrderReadDto>> OrdersAsync();
    Task CreateAsync(Order order);
    void Update(Order order);
    Task<OrderReadDto> OrderAsync(int id);
    Task<IEnumerable<OrderReadDto>> OrderAsync(string shipName);
    Task<bool> CompleteAsync();

}