using AutoMapper;
using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;
using ComissionRateApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComissionRateApi.Controllers;

public class DistributionsController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public DistributionsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // GET: api/Distributions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DistributionReadDto>>> GetDistributions()
    {
        var distributions = await _unitOfWork.DistributionRepo.DistributionsAsync();

        if (distributions == null)
        {
            return NotFound();
        }

        return Ok(distributions);
    }

    [HttpGet("{companyId}")]
    public async Task<ActionResult<IEnumerable<DistributionReadDto>>> GetDistributionsFor(int companyId)
    {
        var distributions = await _unitOfWork.DistributionRepo.DistributionsByAsync(companyId);

        if (distributions == null)
        {
            return NotFound();
        }

        return Ok(distributions);
    }

    // POST: api/Distributions
    [HttpPost]
    public async Task<IActionResult> CreateDistributionAsync(DistributionCreateDto distributionDto)
    {
        if (await _unitOfWork.DistributionRepo.IsExistAsync(distributionDto.Name))
            return BadRequest($"Ditribution already exists with name: {distributionDto.Name} ");

        var actualCompany = await _unitOfWork.CompanyRepo.CompanyAsync(distributionDto.CompanyId);
        if (actualCompany == null) return BadRequest("Company not found with id " + distributionDto.CompanyId);

        var distribution = _mapper.Map<Distribution>(distributionDto);

        await _unitOfWork.DistributionRepo.CreateAsync(distribution);

        if (await _unitOfWork.CompleteAsync())
            return Ok();

        return BadRequest("Failed to create a new distribution");
    }

    // PUT: api/Distributions/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDistribution(int id, DistributionUpdateDto distributionDto)
    {
        var actualDistribution = await _unitOfWork.DistributionRepo.DistributionAsync(id);
        if (actualDistribution == null) return BadRequest("Distribution not found with id " + id);

        var actualCompany = await _unitOfWork.CompanyRepo.CompanyAsync(distributionDto.CompanyId);
        if (actualCompany == null) return BadRequest("Company not found with id " + distributionDto.CompanyId);

        var distribution = _mapper.Map<Distribution>(distributionDto);
        distribution.Id = id;

        _unitOfWork.Update(distribution);
        if (await _unitOfWork.CompleteAsync()) return NoContent();

        return BadRequest("Failed to update distribution");
    }


}