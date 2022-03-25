using ComissionRateApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComissionRateApi.Data;

public static class MigrateAndSeed
{
    public static async Task ProcessMigrationsAndSeedAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<DataContext>();

        // it will check the migrations and run not proceesed if any
        await context.Database.MigrateAsync();

        // any seed data 
        await SeedData(context);
    }

    private static async Task SeedData(DataContext context)
    {
        if(!await context.Customers.AnyAsync())
        {
            var customer = new Customer()
            {
                Address = "Address1",
                City = "City1",
                CompanyName = "ABC",
                Country = "US",
                Name = "Hanuman",
                Phone = "+1(123)-1234",
                PostalCode = "75012",
                Region = "US"
            };

            await context.Customers.AddAsync(customer);
            await context.SaveChangesAsync();
        }

        if(!await context.Companies.AnyAsync())
        {
            var companies = new List<Company> { 
                new Company { Name = "Loyal" },
                new Company { Name = "Arlic" },
                new Company { Name = "Chlic" },
            };

            await context.Companies.AddRangeAsync(companies);
            await context.SaveChangesAsync();
        }

        if(!await context.Distributions.AnyAsync())
        {
            var loyalCompany = await context.Companies.FirstOrDefaultAsync(c => c.Name == "Loyal");
            var arlicCompany = await context.Companies.FirstOrDefaultAsync(c => c.Name == "Arlic");
            var chlicCompany = await context.Companies.FirstOrDefaultAsync(c => c.Name == "Chlic");

            var distributions = new List<Distribution> { 
                new Distribution { Name = "NMO", Company = loyalCompany },
                new Distribution { Name = "ASB", Company = loyalCompany },
                new Distribution { Name = "AMBA", Company = loyalCompany },
                new Distribution { Name = "TROY", Company = arlicCompany },
                new Distribution { Name = "AON", Company = arlicCompany },
                new Distribution { Name = "TOWERS", Company = arlicCompany },
                new Distribution { Name = "HII", Company = arlicCompany },
                new Distribution { Name = "DAVID DEAN", Company = chlicCompany },
                new Distribution { Name = "BUCK", Company = chlicCompany },
                new Distribution { Name = "MERCER", Company = chlicCompany },
            };

            await context.Distributions.AddRangeAsync(distributions);
            await context.SaveChangesAsync();
        }

        if(!await context.Products.AnyAsync())
        {
            var nmoDis = await context.Distributions.FirstOrDefaultAsync(d => d.Name == "NMO");
            var troyDis = await context.Distributions.FirstOrDefaultAsync(d => d.Name == "TROY");
            var buckDis = await context.Distributions.FirstOrDefaultAsync(d => d.Name == "BUCK");

            var products = new List<Product>() { 
                new Product { Name = "2015 Loyal Medicare Supplement Exchang", Location = 12, Code="AAX", Distribution = nmoDis },
                new Product { Name = "2013 Loyal Medicare Supplement", Location = 12, Code="AAX", Distribution = nmoDis },
                new Product { Name = "2015 Loyal Medicare Supplement Exchang", Location = 12, Code="AAX", Distribution = troyDis },
                new Product { Name = "2013 Loyal Medicare Supplement", Location = 12, Code="AAX", Distribution = troyDis },
                new Product { Name = "2015 Loyal Medicare Supplement Exchang", Location = 12, Code="AAX", Distribution = buckDis },
                new Product { Name = "22013 Loyal Medicare Supplement", Location = 12, Code="AAX", Distribution = buckDis },
            };

            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }

    }
}