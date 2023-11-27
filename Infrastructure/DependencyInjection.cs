using Application.Authorization;
using Application.Data;
using Domain.Artists;
using Domain.Releases;
using Domain.Users;
using Infrastructure.Authentication;
using Infrastructure.Caching;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IApplicationDbContext>(sp => 
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(sp => 
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IArtistRepository, ArtistRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IReleaseRepository, ReleaseRepository>();

        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddScoped<IHashProvider, HashProvider>();
        
        services.AddSingleton<IConnectionMultiplexer>(sp => 
            ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                EndPoints = {configuration.GetValue<string>("ConnectionStrings:CacheConnection") },
                AbortOnConnectFail = false
            }));

        services.AddScoped<ICacheService, CacheService>();
        
        return services;
    }
}
