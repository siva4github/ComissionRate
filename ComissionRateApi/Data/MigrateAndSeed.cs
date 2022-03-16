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
    }
}