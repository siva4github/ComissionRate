using System.ComponentModel.DataAnnotations;

namespace ComissionRateApi.Entities;

public class Customer
{
    public int Id { get; set; }    
    [Required, StringLength(100)]public string Name { get; set; }
    [Required, StringLength(500)]public string CompanyName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    [Required] public string Phone { get; set; }
    public ICollection<Order> Orders { get; set; }
    
}