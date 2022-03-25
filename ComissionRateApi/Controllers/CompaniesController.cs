using AutoMapper;
using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;
using ComissionRateApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComissionRateApi.Controllers;

public class CompaniesController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public CompaniesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // GET: api/Companies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyReadDto>>> GetCustomers()
    {
        var companies = await _unitOfWork.CompanyRepo.CompaniesAsync();

        if (companies == null)
        {
            return NotFound();
        }

        return Ok(companies);
    }

    // POST: api/Companies/name
    [HttpPost("{name}")]
    public async Task<IActionResult> CreateCompanyAsync(string name)
    {
        if (await _unitOfWork.CompanyRepo.IsExistAsync(name))
            return BadRequest($"Company already exists with name: {name} ");

        var company = new Company { Name = name };

        await _unitOfWork.CompanyRepo.CreateAsync(company);

        if (await _unitOfWork.CompleteAsync())
            return Ok();

        return BadRequest("Failed to create a new company");
    }

    // PUT: api/Companies/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompany(int id, string name)
    {
        var actualCompany = await _unitOfWork.CompanyRepo.CompanyAsync(id);

        if (actualCompany == null) return BadRequest("Company not found with id " + id);

        actualCompany.Name = name;

        _unitOfWork.Update(actualCompany);
        if (await _unitOfWork.CompleteAsync()) return NoContent();

        return BadRequest("Failed to update company");
    }


}