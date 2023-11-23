using Application.Common;
using MediatR;

namespace Application.Artists.Update;

public record UpdateArtistCommand(Guid Id, string Name, string? Description): IRequest<ResultType<bool>>;

public record UpdateArtistRequest(string Name, string? Description);
