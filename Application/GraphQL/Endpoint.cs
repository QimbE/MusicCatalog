using Application.Data;
using Domain.Artists;
using Domain.Releases;
using Domain.Songs;
using Domain.Users;
using HotChocolate.Authorization;

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
    
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    // [Authorize(Role.RoleNames.Default)]
    public async Task<IQueryable<Song>> GetSongs([Service(ServiceKind.Resolver)] IApplicationDbContext context, CancellationToken cancellationToken)
    {
        return context.Songs;
    }
    
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    // [Authorize(Role.RoleNames.Default)]
    public async Task<IQueryable<Genre>> GetGenres([Service(ServiceKind.Resolver)] IApplicationDbContext context, CancellationToken cancellationToken)
    {
        return context.Genres;
    }
    
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    // [Authorize(Role.RoleNames.Admin)]
    public async Task<IQueryable<User>> GetUsers([Service(ServiceKind.Resolver)] IApplicationDbContext context, CancellationToken cancellationToken)
    {
        return context.Users;
    }
}

