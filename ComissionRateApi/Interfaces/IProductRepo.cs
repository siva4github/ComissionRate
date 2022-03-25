using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;
using ComissionRateApi.Helpers;
using ComissionRateApi.Helpers.Params;

namespace ComissionRateApi.Interfaces;

public interface IProductRepo
{
    Task CreateAsync(Product product);
    Task<IEnumerable<ProductReadDto>> ProductsAsync();
    Task<IEnumerable<ProductReadDto>> ProductsByAsync(int distributionId);
    Task<PagedList<ProductReadDto>> ProductsAsync(ProductParams productParams);
    Task<bool> IsExistAsync(string name);
    Task<ProductReadDto> ProductAsync(int id);
    
}