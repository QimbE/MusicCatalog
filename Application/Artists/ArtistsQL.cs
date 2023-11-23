using System.Reflection;
using Application.Data;
using Domain.Artists;
using HotChocolate.Authorization;
using HotChocolate.Types.Descriptors;

namespace Application.Artists;

public class ArtistsQL
{
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    // [Authorize(Role.RoleNames.Default)]
    public async Task<IQueryable<Artist>> GetArtists([Service(ServiceKind.Resolver)] IApplicationDbContext context)
    {
        return context.Artists;
    }
}

