using AutoMapper;
using AutoMapper.QueryableExtensions;
using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;
using ComissionRateApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComissionRateApi.Data;

public class OrderRepo : IOrderRepo
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    public OrderRepo(DataContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<bool> CompleteAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task CreateAsync(Order order)
    {
        if(order == null) throw new ArgumentNullException(nameof(order));
        await _context.Orders.AddAsync(order);
    }

    public async Task<IEnumerable<OrderReadDto>> OrdersAsync()
    {
        return await _context.Orders
            .ProjectTo<OrderReadDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<OrderReadDto> OrderAsync(int id)
    {
        return await _context.Orders
            .Where(o => o.Id == id)
            .ProjectTo<OrderReadDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<OrderReadDto>> OrderAsync(string shipName)
    {
        return await _context.Orders
            .Where(o => o.ShipName.Contains(shipName))
            .ProjectTo<OrderReadDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public void Update(Order order)
    {
        _context.Entry(order).State = EntityState.Modified;
    }
}