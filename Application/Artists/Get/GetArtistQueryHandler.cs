using Application.Data;
using Domain.Abstractions;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Artists.Get;

public sealed class GetArtistQueryHandler: IRequestHandler<GetArtistQuery, ArtistResponse>
{
    private readonly IApplicationDbContext _context;

    public GetArtistQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<ArtistResponse> Handle(GetArtistQuery request, CancellationToken cancellationToken)
    {
        var artist = await _context.Artists
            .Where(a => a.Id == request.Id)
            .Select(a => new ArtistResponse(a.Id, a.Name, a.Description))
            .FirstOrDefaultAsync(cancellationToken);

        if (artist is null)
        {
            throw new ArtistNotFoundException(request.Id);
        }

        return artist;
    }
}