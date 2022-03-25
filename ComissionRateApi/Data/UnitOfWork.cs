using AutoMapper;
using ComissionRateApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComissionRateApi.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public UnitOfWork(DataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }
    public ICompanyRepo CompanyRepo => new CompanyRepo(_context, _mapper);
    public ICustomerRepo CustomerRepo => new CustomerRepo(_context, _mapper);
    public IDistributionRepo DistributionRepo => new DistributionRepo(_context, _mapper);
    public IOrderRepo OrderRepo => new OrderRepo(_context, _mapper);
    public IProductRepo ProductRepo => new ProductRepo(_context, _mapper);

    public async Task<bool> CompleteAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public bool HasChanges()
    {
        return _context.ChangeTracker.HasChanges();
    }

    public void Update<TEntity>(TEntity entity)
    {
         _context.Entry(entity).State = EntityState.Modified;
    }
}