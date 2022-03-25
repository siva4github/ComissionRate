using AutoMapper;
using AutoMapper.QueryableExtensions;
using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;
using ComissionRateApi.Helpers;
using ComissionRateApi.Helpers.Params;
using ComissionRateApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComissionRateApi.Data;

public class CustomerRepo : ICustomerRepo
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public CustomerRepo(DataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task CreateAsync(Customer customer)
    {
        if(customer == null) throw new ArgumentNullException(nameof(customer));
        await _context.Customers.AddAsync(customer);
    }

    public async Task<CustomerReadDto> CustomerAsync(int id)
    {
        return await _context.Customers
            .Where(c => c.Id == id)
            .ProjectTo<CustomerReadDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<CustomerReadDto>> CustomerAsync(string name)
    {
        return await _context.Customers
            .Where(c => c.Name.Contains(name))
            .ProjectTo<CustomerReadDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<PagedList<CustomerReadDto>> CustomersAsync(CustomerParams customerParams)
    {
        var query = _context.Customers.AsQueryable();

        query = customerParams.OrderBy switch
        {
            "companyName" => query.OrderBy(c => c.CompanyName),
            _=> query.OrderBy(c => c.Name)
        };

        return await PagedList<CustomerReadDto>.CreateAsync(query.ProjectTo<CustomerReadDto>(_mapper.ConfigurationProvider)
            .AsNoTracking(), customerParams.PageNumber, customerParams.PageSize);

        // return await _context.Customers
        //     .ProjectTo<CustomerReadDto>(_mapper.ConfigurationProvider)
        //     .ToListAsync();
    }

    public async Task<IEnumerable<CustomerWithOrdersReadDto>> CustomersWithOrdersAsync()
    {
        return await _context.Customers
            .ProjectTo<CustomerWithOrdersReadDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<bool> IsExistAsync(string name, string companyName)
    {
        var customer = await _context.Customers.Where(c => c.Name == name && c.CompanyName == companyName).FirstOrDefaultAsync();

        if(customer == null) return false;

        return true;
    }
}