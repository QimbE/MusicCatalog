using Application.Common;
using MediatR;

namespace Application.Artists.Create;

public record CreateArtistCommand(string Name, string? Description) : IRequest<ResultType<Guid>>
{
    
}