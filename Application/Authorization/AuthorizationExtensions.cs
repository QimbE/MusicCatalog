using Domain.Users;
using Microsoft.AspNetCore.Builder;

namespace Application.Authorization;

public static class AuthorizationExtensions
{
    public static RouteHandlerBuilder UserShouldBeAtLeast(this RouteHandlerBuilder builder, Role role)
    {
        return builder.RequireAuthorization(role.Name);
    }
}