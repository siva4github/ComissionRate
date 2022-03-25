namespace ComissionRateApi.Helpers.Params;

public class ProductParams : PaginationParams
{
    public string OrderBy { get; set; } = "name";
    public int CompanyId { get; set; } = 0;
    public int DistributionId { get; set; } = 0;
}