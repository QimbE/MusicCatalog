using Application.Behaviors;
using Application.Common;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
        services.AddSingleton<Mapper, Mapper>();
        
        services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(assembly);
                configuration.AddPipeLineBehaviors();
            });
        
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}