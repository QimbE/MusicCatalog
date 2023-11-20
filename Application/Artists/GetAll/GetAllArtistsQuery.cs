using Application.Artists.Get;
using Application.Common;
using MediatR;

namespace Application.Artists.GetAll;

public record GetAllArtistsQuery() : IRequest<Result<IEnumerable<ArtistResponse>>>;