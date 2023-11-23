using Application.Common;
using Application.Data;
using Domain.Artists;
using Domain.Artists.Exceptions;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Artists.Get;

public sealed class GetArtistQueryHandler: IRequestHandler<GetArtistQuery, ResultType<ArtistResponse>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICacheService _cache;

    public GetArtistQueryHandler(IApplicationDbContext context, ICacheService cache)
    {
        _context = context;
        _cache = cache;
    }


    public async Task<ResultType<ArtistResponse>> Handle(GetArtistQuery request, CancellationToken cancellationToken)
    {
        var artist = await _cache.GetDataAsync<ArtistResponse>(CachingKeys.ArtistResponsePrefix + request.Id, cancellationToken);

        if (artist is not null)
        {
            return artist;
        }
        
        artist = await _context.Artists
            .Where(a => a.Id == request.Id)
            .Select(a => new ArtistResponse(a.Id, a.Name, a.Description))
            .FirstOrDefaultAsync(cancellationToken);

        if (artist is null)
        {
            return new ArtistNotFoundException(nameof(request.Id));
        }

        await _cache.SetDataAsync(
            CachingKeys.ArtistResponsePrefix + artist.Id,
            artist,
            DateTimeOffset.UtcNow.AddMinutes(1),
            cancellationToken
            );

        return artist;
    }
}