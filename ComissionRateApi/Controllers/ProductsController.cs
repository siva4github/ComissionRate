using AutoMapper;
using ComissionRateApi.Dtos;
using ComissionRateApi.Entities;
using ComissionRateApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComissionRateApi.Controllers;

public class ProductsController : BaseApiController
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    // GET: api/Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetProducts()
    {
        var products = await _unitOfWork.ProductRepo.ProductsAsync();

        if (products == null)
        {
            return NotFound();
        }

        return Ok(products);
    }

    // POST: api/Products
    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(ProductCreateDto productDto)
    {
        if (await _unitOfWork.ProductRepo.IsExistAsync(productDto.Name))
            return BadRequest($"Product already exists with name: {productDto.Name} ");

        var actualDistribution = await _unitOfWork.DistributionRepo.DistributionAsync(productDto.DistributionId);
        if (actualDistribution == null) return BadRequest("Distribution not found with id " + productDto.DistributionId);

        var product = _mapper.Map<Product>(productDto);

        await _unitOfWork.ProductRepo.CreateAsync(product);

        if (await _unitOfWork.CompleteAsync())
            return Ok();

        return BadRequest("Failed to create a new product");
    }

    // PUT: api/Products/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, ProductUpdateDto productDto)
    {
        var actualProduct = await _unitOfWork.ProductRepo.ProductAsync(id);
        if (actualProduct == null) return BadRequest("Product not found with id " + id);

        var actualDistribution = await _unitOfWork.DistributionRepo.DistributionAsync(productDto.DistributionId);
        if (actualDistribution == null) return BadRequest("Distribution not found with id " + productDto.DistributionId);

        var product = _mapper.Map<Product>(productDto);
        product.Id = id;

        _unitOfWork.Update(product);
        if (await _unitOfWork.CompleteAsync()) return NoContent();

        return BadRequest("Failed to update distribution");
    }


}