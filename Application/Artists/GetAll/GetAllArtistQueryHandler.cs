using Application.Artists.Get;
using Application.Common;
using Application.Data;
using Domain.Artists.Exceptions;
using MediatR;

namespace Application.Artists.GetAll;

public class GetAllArtistQueryHandler: IRequestHandler<GetAllArtistsQuery, Result<IEnumerable<ArtistResponse>>>
{
    private readonly IApplicationDbContext _context;

    public GetAllArtistQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    // todo: implement paging 
    public async Task<Result<IEnumerable<ArtistResponse>>> Handle(GetAllArtistsQuery request, CancellationToken cancellationToken)
    {
        var artists = await GetAllArtist();

        if (!artists.Any())
        {
            // todo: change to some new exception
            return new ArtistNotFoundException(nameof(artists));
        }

        return Result.From(artists);
    }

    private Task<IEnumerable<ArtistResponse>> GetAllArtist()
    {
        return Task.FromResult(_context.Artists
                .Select(a => new ArtistResponse(a.Id, a.Name, a.Description)).AsEnumerable()
            );
    }
}