namespace ComissionRateApi.Dtos;

public class CustomerWithOrdersReadDto
{
    public int Id { get; set; }    
    public string Name { get; set; }
    public string CompanyName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Phone { get; set; }
    public List<OrderReadDto> Orders { get; set; }
}