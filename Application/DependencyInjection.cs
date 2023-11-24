using System.Text;
using Application.Authorization;
using Application.Behaviors;
using Application.Common;
using Application.ExceptionHandling;
using Application.GraphQL;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
        services.AddSingleton<IMapper, Mapper>();
        
        services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(assembly);
                configuration.AddPipeLineBehaviors();
            });
        
        services.AddValidatorsFromAssembly(assembly);

        var secretKey = Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!);
        
        var tokenValidationParameter = new TokenValidationParameters
        {
            ValidIssuer = configuration["JwtSettings:Issuer"],
            ValidAudience = configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = tokenValidationParameter;
        });

        services.AddAuthorization();

        services.AddRoleAuthorizationPolicies();

        services.AddGraphQLServer()
            .ConfigureHotChocolateTypes()
            .ConfigurePipeline();

        services.AddTransient<GlobalExceptionHandlingMiddleware>();
        
        return services;
    }
}