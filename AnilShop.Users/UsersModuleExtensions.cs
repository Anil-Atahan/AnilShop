using System.Reflection;
using AnilShop.Users.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace AnilShop.Users;

public static class UsersModuleExtensions
{
    public static IServiceCollection AddUserModuleServices(this IServiceCollection services,
        ConfigurationManager config,
        ILogger logger, 
        List<Assembly> mediatRAssemblies)
    {
        string? connectionString = config.GetConnectionString("UsersConnectionString");
        services.AddDbContext<UsersDbContext>(options => 
            options.UseNpgsql(connectionString));

        services.AddIdentityCore<ApplicationUser>()
            .AddEntityFrameworkStores<UsersDbContext>();
        
        services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();
        services.AddScoped<IReadOnlyUserStreetAddressRepository, UserStreetAddressRepository>();

        services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();
        
        mediatRAssemblies.Add(typeof(UsersModuleExtensions).Assembly);
        
        logger.Information("{Module} module services registered", "Users");
        
        return services;
    }
}