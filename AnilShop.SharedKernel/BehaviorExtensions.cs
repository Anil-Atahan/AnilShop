using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AnilShop.SharedKernel;

public static class BehaviorExtensions
{
    public static IServiceCollection AddMediatRLoggingBehavior(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>),
          typeof(LoggingBehaviour<,>));
        return services;
    }
    
    public static IServiceCollection AddMediatRFluentValidationBehavior(
        this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>),
            typeof(FluentValidationBehavior<,>));

        return services;
    }

    public static IServiceCollection AddValidatorsFromAssemblyContaining<T>(
        this IServiceCollection services)
    {
        var assembly = typeof(T).GetTypeInfo().Assembly;

        var validatorTypes = assembly.GetTypes()
            .Where(t => t.GetInterfaces()
                .Any(i => i.IsGenericType && 
                          i.GetGenericTypeDefinition() == typeof(IValidator<>)))
            .ToList();

        foreach (var validatorType in validatorTypes)
        {
            var implementedInterfaces = validatorType.GetInterfaces()
                .Where(i => i.IsGenericType && 
                            i.GetGenericTypeDefinition() == typeof(IValidator<>));

            foreach (var implementedInterface in implementedInterfaces)
            {
                services.AddTransient(implementedInterface, validatorType);
            }
        }

        return services;
    }
}