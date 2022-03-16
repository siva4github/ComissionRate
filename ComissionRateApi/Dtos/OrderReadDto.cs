namespace ComissionRateApi.Dtos;
public class OrderReadDto
{
    public int Id { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? RequiredDate { get; set; }
    public string ShipName { get; set; }
    public string ShipAddress { get; set; }
    public string ShipCity { get; set; }
    public string ShipRegion { get; set; }
    public string ShipPostalCode { get; set; }
    public string ShipCountry { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
}