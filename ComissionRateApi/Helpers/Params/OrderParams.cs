namespace ComissionRateApi.Helpers.Params;

public class OrderParams : PaginationParams
{
    public string OrderBy { get; set; } = "name";
}