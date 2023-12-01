using Application.Common;
using MediatR;

namespace Application.Releases.Update;

public record UpdateReleaseCommand(
    Guid Id,
    Guid AuthorId,
    string Name,
    string? Description,
    int TypeId,
    DateTime ReleaseDate,
    string LinkToCover
    ): IRequest<ResultType<bool>>;