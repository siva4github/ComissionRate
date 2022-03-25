using ComissionRateApi.Profiles;

namespace ComissionRateApi.Extensions;

public static class AutoMapperExtensions
{
    public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(configuration => {

           configuration.AddProfile<CustomerMappingProfile>(); 
           configuration.AddProfile<OrderMappingProfile>(); 
           configuration.AddProfile<CompanyMappingProfile>();
           configuration.AddProfile<DistributionMappingProfile>();
           configuration.AddProfile<ProductMappingProfile>();
        });

        return services;
    }
} 