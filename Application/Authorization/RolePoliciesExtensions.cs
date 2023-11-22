using Domain.Users;
using Microsoft.AspNetCore.Authorization;

namespace Application.Authorization;

public static class RolePoliciesExtensions
{
    public static AuthorizationBuilder AddRoleAdminPolicy(this AuthorizationBuilder builder)
    {
        return builder.AddPolicy(Role.Admin.Name, policy =>
        {
            policy.RequireRole(Role.Admin.Name);
        });
    }
    
    public static AuthorizationBuilder AddRoleDataBaseAdminPolicy(this AuthorizationBuilder builder)
    {
        return builder.AddPolicy(Role.DatabaseAdmin.Name, policy =>
        {
            policy.RequireRole(Role.Admin.Name, Role.DatabaseAdmin.Name);
        });
    }
    
    public static AuthorizationBuilder AddRoleDefaultUserPolicy(this AuthorizationBuilder builder)
    {
        return builder.AddPolicy(Role.Default.Name, policy =>
        {
            policy.RequireRole(Role.Admin.Name, Role.DatabaseAdmin.Name, Role.Default.Name);
        });
    }
}