using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Authorization;

public static class AuthorizationPoliciesDependencyInjection
{
    public static AuthorizationBuilder AddRoleAuthorizationPolicies(this IServiceCollection services)
    {
        return services.AddAuthorizationBuilder()
            .AddRoleDefaultUserPolicy()
            .AddRoleDataBaseAdminPolicy()
            .AddRoleAdminPolicy();
    }
}