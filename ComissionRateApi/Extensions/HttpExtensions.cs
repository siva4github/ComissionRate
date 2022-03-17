using System.Text.Json;
using ComissionRateApi.Helpers;

namespace ComissionRateApi.Extensions;

public static class HttpExtensions
{
    public static void AddPaginationHeader(this HttpResponse response, int currentPage, int itemsPerPge, int totalItems, int totalPges)
    {
        var paginationHeader = new PaginationHeader(currentPage, itemsPerPge, totalItems, totalPges);

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, options));
        response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
    }
}