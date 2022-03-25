using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;
using ComissionRateApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComissionRateApi.Data;

public class DistributionRepo : IDistributionRepo
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public DistributionRepo(DataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<IEnumerable<DistributionReadDto>> DistributionsAsync()
    {
        return await _context.Distributions
             .ProjectTo<DistributionReadDto>(_mapper.ConfigurationProvider)
             .AsNoTracking()
             .ToListAsync();
             
    }

    public async Task CreateAsync(Distribution distribution)
    {
        if(distribution == null) throw new ArgumentNullException(nameof(distribution));
        await _context.Distributions.AddAsync(distribution);
    }

    public async Task<bool> IsExistAsync(string name)
    {
       var customer = await _context.Distributions.Where(c => c.Name == name).FirstOrDefaultAsync();

        if(customer == null) return false;

        return true;
    }

    public async Task<IEnumerable<DistributionReadDto>> DistributionsByAsync(int companyId)
    {
        return await _context.Distributions
            .Where(d => d.CompanyId == companyId)
            .ProjectTo<DistributionReadDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<DistributionReadDto> DistributionAsync(int id)
    {
        return await _context.Distributions
            .Where(d => d.Id == id)
            .ProjectTo<DistributionReadDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
}