using System.ComponentModel.DataAnnotations;

namespace ComissionRateApi.Dtos;
public class OrderUpdateDto
{
    public DateTime? OrderDate { get; set; }
    public DateTime? RequiredDate { get; set; }
    [Required, StringLength(200)] public string ShipName { get; set; }
    [Required] public int CustomerId { get; set; }
}