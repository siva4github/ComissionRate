using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;
using ComissionRateApi.Helpers;
using ComissionRateApi.Helpers.Params;

namespace ComissionRateApi.Interfaces;

public interface ICompanyRepo
{
    
    Task CreateAsync(Company company);
    Task<IEnumerable<CompanyReadDto>> CompaniesAsync();
    Task<bool> IsExistAsync(string name);
    Task<CompanyReadDto> CompanyAsync(int id);
    
}