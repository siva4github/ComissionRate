using System.ComponentModel.DataAnnotations;

namespace ComissionRateApi.Entities;

public class Product
{
    public int Id { get; set; }
    [Required, StringLength(200)] public string Name { get; set; }
    [Required] public int Location { get; set; }
    [Required, StringLength(50)] public string Code { get; set; }
    public Distribution Distribution { get; set; }
    public int DistributionId { get; set; }
}