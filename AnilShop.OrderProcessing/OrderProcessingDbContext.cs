using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace AnilShop.OrderProcessing;

internal class OrderProcessingDbContext : DbContext
{
    public OrderProcessingDbContext(DbContextOptions<OrderProcessingDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("OrderProcessing");

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
}