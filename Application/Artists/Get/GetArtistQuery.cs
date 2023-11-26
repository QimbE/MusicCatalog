using Application.Common;
using Application.DTO;
using MediatR;

namespace Application.Artists.Get;

public record GetArtistQuery(Guid Id): IRequest<ResultType<ArtistResponse>>;
