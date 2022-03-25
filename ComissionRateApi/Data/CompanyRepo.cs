using AutoMapper;
using AutoMapper.QueryableExtensions;
using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;
using ComissionRateApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComissionRateApi.Data;

public class CompanyRepo : ICompanyRepo
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public CompanyRepo(DataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<IEnumerable<CompanyReadDto>> CompaniesAsync()
    {
        return await _context.Companies
             .ProjectTo<CompanyReadDto>(_mapper.ConfigurationProvider)
             .AsNoTracking()
             .ToListAsync();
             
    }

    public async Task CreateAsync(Company company)
    {
        if(company == null) throw new ArgumentNullException(nameof(company));
        await _context.Companies.AddAsync(company);
    }

    public async Task<CompanyReadDto> CompanyAsync(int id)
    {
        return await _context.Companies
            .Where(c => c.Id == id)
            .ProjectTo<CompanyReadDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> IsExistAsync(string name)
    {
       var customer = await _context.Companies.Where(c => c.Name == name).FirstOrDefaultAsync();

        if(customer == null) return false;

        return true;
    }
    
}