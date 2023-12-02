using Application.Common;
using MediatR;

namespace Application.Songs.Create;

public record CreateSongCommand(Guid ReleaseId, Guid GenreId, string Name, string AudioLink, List<Guid> ArtistOnFeatIds): IRequest<ResultType<Guid>>;