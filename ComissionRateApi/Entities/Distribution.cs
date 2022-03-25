using System.ComponentModel.DataAnnotations;

namespace ComissionRateApi.Entities;

public class Distribution
{
    public int Id { get; set; }
    [Required, StringLength(200)] public string Name { get; set; }
    public Company Company { get; set; }
    public int CompanyId { get; set; }

}