using System.ComponentModel.DataAnnotations;

namespace ComissionRateApi.Dtos;

public class DistributionCreateDto
{ 
    [Required, StringLength(200)] public string Name { get; set; }
    public int CompanyId { get; set; }
}