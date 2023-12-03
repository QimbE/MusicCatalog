using Application.Common;
using MediatR;

namespace Application.Songs.Update;

public record UpdateSongCommand(Guid Id, Guid ReleaseId, Guid GenreId, string Name, string AudioLink,
    List<Guid> ArtistOnFeatIds) : IRequest<ResultType<bool>>;