using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;
using ComissionRateApi.Helpers;
using ComissionRateApi.Helpers.Params;

namespace ComissionRateApi.Interfaces;

public interface ICustomerRepo
{
    Task<PagedList<CustomerReadDto>> CustomersAsync(CustomerParams customerParams);
    Task<IEnumerable<CustomerWithOrdersReadDto>> CustomersWithOrdersAsync();
    Task CreateAsync(Customer customer);
    void Update(Customer customer);
    Task<CustomerReadDto> CustomerAsync(int id);
    Task<IEnumerable<CustomerReadDto>> CustomerAsync(string name);
    Task<bool> CompleteAsync();
    Task<bool> IsExistAsync(string name, string companyName);

}