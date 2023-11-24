using Application.Data;
using Domain.Artists;
using Domain.Releases;

namespace Application.GraphQL;

public class Endpoint
{
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    // [Authorize(Role.RoleNames.Default)]
    public async Task<IQueryable<Artist>> GetArtists([Service(ServiceKind.Resolver)] IApplicationDbContext context, CancellationToken cancellationToken)
    {
        return context.Artists;
    }
    
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    // [Authorize(Role.RoleNames.Default)]
    public async Task<IQueryable<Release>> GetReleases([Service(ServiceKind.Resolver)] IApplicationDbContext context, CancellationToken cancellationToken)
    {
        return context.Releases;
    }
}

