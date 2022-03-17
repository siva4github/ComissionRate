namespace ComissionRateApi.Helpers.Params;

public class CustomerParams : PaginationParams
{
    public string OrderBy { get; set; } = "name";
}