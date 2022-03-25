using System.ComponentModel.DataAnnotations;

namespace ComissionRateApi.Dtos;

public class ProductUpdateDto
{
    [Required, StringLength(200)] public string Name { get; set; }
    [Required] public int Location { get; set; }
    [Required, StringLength(50)] public string Code { get; set; }
    [Required] public int DistributionId { get; set; }
}