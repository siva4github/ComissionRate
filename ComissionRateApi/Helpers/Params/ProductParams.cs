namespace ComissionRateApi.Helpers.Params;

public class ProductParams : PaginationParams
{
    public string OrderBy { get; set; } = "name";
}