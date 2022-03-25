using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;

namespace ComissionRateApi.Interfaces;

public interface IDistributionRepo
{
    Task CreateAsync(Distribution distribution);
    Task<IEnumerable<DistributionReadDto>> DistributionsAsync();
    Task<IEnumerable<DistributionReadDto>> DistributionsByAsync(int companyId);
    Task<bool> IsExistAsync(string name);
    Task<DistributionReadDto> DistributionAsync(int id);

}