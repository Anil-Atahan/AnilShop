using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AnilShop.OrderProcessing;

public static class OrderModuleExtensions
{
    public static IServiceCollection AddOrderProcessingModuleServices(this IServiceCollection services,
        ConfigurationManager config,
        ILogger logger, 
        List<Assembly> mediatRAssemblies)
    {
        string? connectionString = config.GetConnectionString("OrderProcessingConnectionString");
        services.AddDbContext<OrderProcessingDbContext>(options => 
            options.UseNpgsql(connectionString));
        
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<RedisOrderAddressCache>();
        services.AddScoped<IOrderAddressCache, ReadThroughOrderAddressCache>();
        
        mediatRAssemblies.Add(typeof(OrderModuleExtensions).Assembly);
        
        logger.Information("{Module} module services registered", "OrderProcessing");
        
        return services;
    }
}