using Application.Artists.Get;
using Application.Common;
using Application.DTO;
using MediatR;

namespace Application.Artists.GetAll;

public record GetAllArtistsQuery() : IRequest<ResultType<IEnumerable<ArtistResponse>>>;