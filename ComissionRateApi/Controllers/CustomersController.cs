using AutoMapper;
using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;
using ComissionRateApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComissionRateApi.Controllers;

public class CustomersController : BaseApiController
{
    private readonly ICustomerRepo _customerRepo;
    private readonly IMapper _mapper;
    public CustomersController(ICustomerRepo customerRepo, IMapper mapper)
    {
        _mapper = mapper;
        _customerRepo = customerRepo;
    }

    // GET: api/Customers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CustomerReadDto>>> GetCustomers()
    {
        var customers = await _customerRepo.CustomersAsync();

        if (customers == null)
        {
            return NotFound();
        }

        return Ok(customers);
    }

    // GET: api/Customers
    [HttpGet("CustomersWithOrders")]
    public async Task<ActionResult<IEnumerable<CustomerWithOrdersReadDto>>> GetCustomersWithOrders()
    {
        var customers = await _customerRepo.CustomersWithOrdersAsync();

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
        var customer = await _customerRepo.CustomerAsync(id);

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
        var customers = await _customerRepo.CustomerAsync(name);

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
        var actualCustomer = await _customerRepo.CustomerAsync(id);

        if (actualCustomer == null) return BadRequest("Customer not found with id " + id);

        var customer = _mapper.Map<Customer>(customerDto);
        customer.Id = id;

        _customerRepo.Update(customer);
        if(await _customerRepo.CompleteAsync()) return NoContent();

        return BadRequest("Failed to update customer");
    }

    // POST: api/customers
    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CustomerCreateDto customerDto)
    {
        if(await _customerRepo.IsExistAsync(customerDto.Name, customerDto.CompanyName))
        return BadRequest($"Customer already exists with name: {customerDto.Name}, company name: {customerDto.CompanyName}");

        var customer = _mapper.Map<Customer>(customerDto);

        await _customerRepo.CreateAsync(customer);

        if (await _customerRepo.CompleteAsync()) 
                return Ok("Customer added successfully");

        return BadRequest("Failed to create a new customer");
    }

}