﻿using Application.Artists.Get;
using Application.Common;
using Application.Data;
using Domain.Artists.Exceptions;
using MediatR;

namespace Application.Artists.GetAll;

public class GetAllArtistQueryHandler: IRequestHandler<GetAllArtistsQuery, Result<IEnumerable<ArtistResponse>>>
{
    private readonly IApplicationDbContext _context;

    private readonly ICacheService _cache;

    public GetAllArtistQueryHandler(IApplicationDbContext context, ICacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    // todo: implement paging 
    public async Task<Result<IEnumerable<ArtistResponse>>> Handle(GetAllArtistsQuery request, CancellationToken cancellationToken)
    {
        // check cache data
        var artists = await _cache.GetDataAsync<IEnumerable<ArtistResponse>>(CachingKeys.ListOfUsersCacheKey, cancellationToken);

        // if cache is not empty
        if (artists is not null && artists.Any())
        {
            return Result.From(artists);
        }
        
        artists = await GetAllArtist();
        
        // if there is no artists in db
        if (!artists.Any())
        {
            // todo: change to some new exception
            return new ArtistNotFoundException(nameof(artists));
        }
        
        // caching
        await _cache.SetDataAsync(CachingKeys.ListOfUsersCacheKey, artists,
            DateTimeOffset.UtcNow.AddMinutes(1), cancellationToken);

        return Result.From(artists);
    }

    private Task<IEnumerable<ArtistResponse>> GetAllArtist()
    {
        return Task.FromResult(_context.Artists
                .Select(a => new ArtistResponse(a.Id, a.Name, a.Description)).AsEnumerable()
            );
    }
}