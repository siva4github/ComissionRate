using System.ComponentModel.DataAnnotations;

namespace ComissionRateApi.Dtos;
public class OrderCreateDto
{
    public DateTime? OrderDate { get; set; }
    public DateTime? RequiredDate { get; set; }
    [Required, StringLength(200)] public string ShipName { get; set; }
    public string ShipAddress { get; set; }
    public string ShipCity { get; set; }
    public string ShipRegion { get; set; }
    public string ShipPostalCode { get; set; }
    public string ShipCountry { get; set; }
    [Required] public int CustomerId { get; set; }
}