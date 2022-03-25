using System.ComponentModel.DataAnnotations;

namespace ComissionRateApi.Entities;

public class Company
{
    public int Id { get; set; }
    [Required, StringLength(200)] public string Name { get; set; }
}