using AutoMapper;
using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;
using ComissionRateApi.Extensions;
using ComissionRateApi.Helpers.Params;
using ComissionRateApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComissionRateApi.Controllers;

public class OrdersController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IOrderRepo _orderRepo;
    private readonly ICustomerRepo _customerRepo;
    public OrdersController(IOrderRepo orderRepo, ICustomerRepo customerRepo, IMapper mapper)
    {
        _customerRepo = customerRepo;
        _orderRepo = orderRepo;
        _mapper = mapper;
    }

    // GET: api/Orders
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderReadDto>>> GetOrders([FromQuery]OrderParams orderParams)
    {
        var orders = await _orderRepo.OrdersAsync(orderParams);

        if (orders == null)
        {
            return NotFound();
        }

        Response.AddPaginationHeader(orders.CurrentPage, orders.PageSize, orders.TotalCount, orders.TotalPages);
        return Ok(orders);
    }

    // GET: api/Orders/5
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderReadDto>> GetOrderById(int id)
    {
        var order = await _orderRepo.OrderAsync(id);

        if (order == null)
        {
            return NotFound();
        }

        return Ok(order);
    }

    // GET: api/Orders/laptop
    [HttpGet("OrdersBy/{shipName}")]
    public async Task<ActionResult<IEnumerable<CustomerReadDto>>> GetOrdersByName(string shipName)
    {
        var orders = await _orderRepo.OrderAsync(shipName);

        if (orders == null)
        {
            return NotFound();
        }

        return Ok(orders);
    }

    // GET: api/Orders/laptop
    [HttpGet("OrdersByCustomerId/{id}")]
    public async Task<ActionResult<IEnumerable<CustomerReadDto>>> GetOrdersByCustomer(int id)
    {
        var orders = await _orderRepo.OrderByCustomerAsync(id);

        if (orders == null)
        {
            return NotFound();
        }

        return Ok(orders);
    }

    // PUT: api/Orders/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutOrder(int id, OrderUpdateDto orderDto)
    {
        var actualOrder = await _orderRepo.OrderAsync(id);
        if (actualOrder == null) return BadRequest("Order not found with id " + id);

        var actualCustomer = await _customerRepo.CustomerAsync(orderDto.CustomerId);
        if (actualCustomer == null) return BadRequest("Customer not found with id " + orderDto.CustomerId);

        var order = _mapper.Map<Order>(orderDto);
        order.ShipAddress = actualOrder.ShipAddress;
        order.ShipCity = actualOrder.ShipCity;
        order.ShipRegion = actualOrder.ShipRegion;
        order.ShipPostalCode = actualOrder.ShipPostalCode;
        order.ShipCountry = actualOrder.ShipCountry;
        order.Id = id;

        _orderRepo.Update(order);
        if (await _orderRepo.CompleteAsync()) return NoContent();

        return BadRequest("Failed to update order");
    }

    // POST: api/orders
    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderCreateDto orderDto)
    {

        var actualCustomer = await _customerRepo.CustomerAsync(orderDto.CustomerId);
        if (actualCustomer == null) return BadRequest("Customer not found with id " + orderDto.CustomerId);

        var customer = _mapper.Map<Order>(orderDto);

        await _orderRepo.CreateAsync(customer);

        if (await _orderRepo.CompleteAsync())
            return Ok("Order added successfully");

        return BadRequest("Failed to create a new order");
    }

}