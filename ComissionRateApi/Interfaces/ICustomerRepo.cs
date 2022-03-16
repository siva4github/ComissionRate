using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;

namespace ComissionRateApi.Interfaces;

public interface ICustomerRepo
{
    Task<IEnumerable<CustomerReadDto>> CustomersAsync();
    Task<IEnumerable<CustomerWithOrdersReadDto>> CustomersWithOrdersAsync();
    Task CreateAsync(Customer customer);
    void Update(Customer customer);
    Task<CustomerReadDto> CustomerAsync(int id);
    Task<IEnumerable<CustomerReadDto>> CustomerAsync(string name);
    Task<bool> CompleteAsync();
    Task<bool> IsExistAsync(string name, string companyName);

}