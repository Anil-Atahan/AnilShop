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
}