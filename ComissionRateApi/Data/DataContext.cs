using ComissionRateApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComissionRateApi.Data;

public class DataContext : DbContext
{
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Company> Companies { get; set; }
    public virtual DbSet<Distribution> Distributions { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public DataContext()
    {
        
    }
    public DataContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}