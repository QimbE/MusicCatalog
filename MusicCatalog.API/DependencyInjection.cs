using Carter;

namespace MusicCatalog.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddCarter();
        return services;
    }
}
