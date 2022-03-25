using ComissionRateApi.Data;
using ComissionRateApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ComissionRateApi.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    { 

        // Services


        // DB & Repositories
        services.AddDbContext<DataContext>(options => {
            options.UseSqlServer(configuration.GetConnectionString("ComissionRateDB"));
        });

        // services.AddScoped<ICustomerRepo, CustomerRepo>();
        // services.AddScoped<IOrderRepo, OrderRepo>();
        // services.AddScoped<ICompanyRepo, CompanyRepo>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        return services;
    }
}