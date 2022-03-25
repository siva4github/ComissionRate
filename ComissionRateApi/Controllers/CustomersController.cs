using AutoMapper;
using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;
using ComissionRateApi.Extensions;
using ComissionRateApi.Helpers.Params;
using ComissionRateApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComissionRateApi.Controllers;

public class CustomersController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public CustomersController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // GET: api/Customers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerReadDto>>> GetCustomers([FromQuery] CustomerParams customerParams)
    {
        var customers = await _unitOfWork.CustomerRepo.CustomersAsync(customerParams);

        if (customers == null)
        {
            return NotFound();
        }

        Response.AddPaginationHeader(customers.CurrentPage, customers.PageSize, customers.TotalCount, customers.TotalPages);

        return Ok(customers);
    }

    // GET: api/Customers
    [HttpGet("CustomersWithOrders")]
    public async Task<ActionResult<IEnumerable<CustomerWithOrdersReadDto>>> GetCustomersWithOrders()
    {
        var customers = await _unitOfWork.CustomerRepo.CustomersWithOrdersAsync();

        if (customers == null)
        {
            return NotFound();
        }

        return Ok(customers);
    }

    // GET: api/Customers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerReadDto>> GetCustomerById(int id)
    {
        var customer = await _unitOfWork.CustomerRepo.CustomerAsync(id);

        if (customer == null)
        {
            return NotFound();
        }

        return Ok(customer);
    }

    // GET: api/Customers/hanu
    [HttpGet("CustomersBy/{name}")]
    public async Task<ActionResult<IEnumerable<CustomerReadDto>>> GetCustomersByName(string name)
    {
        var customers = await _unitOfWork.CustomerRepo.CustomerAsync(name);

        if (customers == null)
        {
            return NotFound();
        }

        return Ok(customers);
    }

    // PUT: api/Customers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomer(int id, CustomerUpdateDto customerDto)
    {
        var actualCustomer = await _unitOfWork.CustomerRepo.CustomerAsync(id);

        if (actualCustomer == null) return BadRequest("Customer not found with id " + id);

        var customer = _mapper.Map<Customer>(customerDto);
        customer.Id = id;

        _unitOfWork.Update(customer);
        if (await _unitOfWork.CompleteAsync()) return NoContent();

        return BadRequest("Failed to update customer");
    }

    // POST: api/customers
    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CustomerCreateDto customerDto)
    {
        if (await _unitOfWork.CustomerRepo.IsExistAsync(customerDto.Name, customerDto.CompanyName))
            return BadRequest($"Customer already exists with name: {customerDto.Name}, company name: {customerDto.CompanyName}");

        var customer = _mapper.Map<Customer>(customerDto);

        await _unitOfWork.CustomerRepo.CreateAsync(customer);

        if (await _unitOfWork.CompleteAsync())
            return Ok();

        return BadRequest("Failed to create a new customer");
    }

}