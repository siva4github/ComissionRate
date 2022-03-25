using AutoMapper;
using AutoMapper.QueryableExtensions;
using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;
using ComissionRateApi.Helpers;
using ComissionRateApi.Helpers.Params;
using ComissionRateApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComissionRateApi.Data;

public class ProductRepo : IProductRepo
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public ProductRepo(DataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task CreateAsync(Product product)
    {
        if(product == null) throw new ArgumentNullException(nameof(product));
        await _context.Products.AddAsync(product);
    }

    public async Task<PagedList<ProductReadDto>> ProductsAsync(ProductParams productParams)
    {
        var query = _context.Products.AsQueryable();

        query = productParams.OrderBy switch
        {
            "code" => query.OrderBy(p => p.Code),
            _=> query.OrderBy(p => p.Name)
        };

        return await PagedList<ProductReadDto>.CreateAsync(query.ProjectTo<ProductReadDto>(_mapper.ConfigurationProvider)
            .AsNoTracking(), productParams.PageNumber, productParams.PageSize);

        // return await _context.Products
        //     .ProjectTo<ProductReadDto>(_mapper.ConfigurationProvider)
        //     .ToListAsync();
    }

    public async Task<IEnumerable<ProductReadDto>> ProductsAsync()
    {
        return await _context.Products
            .ProjectTo<ProductReadDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductReadDto>> ProductsByAsync(int distributionId)
    {
        return await _context.Products
            .Where(p => p.DistributionId == distributionId)
            .ProjectTo<ProductReadDto>(_mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> IsExistAsync(string name)
    {
        var customer = await _context.Products.Where(p => p.Name == name).FirstOrDefaultAsync();

        if(customer == null) return false;

        return true;
    }

    public async Task<ProductReadDto> ProductAsync(int id)
    {
       return await _context.Products
            .Where(p => p.Id == id)
            .ProjectTo<ProductReadDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
}