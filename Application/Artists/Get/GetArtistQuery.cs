using Application.Common;
using Application.DTO;
using Application.DTO.Artist;
using MediatR;

namespace Application.Artists.Get;

public record GetArtistQuery(Guid Id): IRequest<ResultType<ArtistResponse>>;
