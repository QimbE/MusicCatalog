using MediatR;

namespace Application.Artists.Get;

public record GetArtistQuery(Guid Id): IRequest<ArtistResponse>;

public record ArtistResponse(Guid Id, string Name, string? Description);