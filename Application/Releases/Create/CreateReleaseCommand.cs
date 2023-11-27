using Application.Common;
using MediatR;

namespace Application.Releases.Create;

public record CreateReleaseCommand(Guid AuthorId, int TypeId, string Name, string? Description, DateTime ReleaseDate, string LinkToCover): IRequest<ResultType<Guid>>
{
    
}