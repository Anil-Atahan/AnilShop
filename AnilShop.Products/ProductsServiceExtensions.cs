using System.Reflection;
using AnilShop.Products.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AnilShop.Products;

public static class ProductsServiceExtensions
{
    public static IServiceCollection AddProductsService(this IServiceCollection services,
        ConfigurationManager config,
        ILogger logger,
        List<Assembly> mediatRAssemblies)
    {
        string? connectionString = config.GetConnectionString("ProductsConnectionString");
        services.AddDbContext<ProductDbContext>(options => 
            options.UseNpgsql(connectionString));
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        
        mediatRAssemblies.Add(typeof(ProductsServiceExtensions).Assembly);
        
        logger.Information("{Module} module services registered", "Products");
        
        return services;
    }
}