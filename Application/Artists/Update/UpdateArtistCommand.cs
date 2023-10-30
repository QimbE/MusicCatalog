using MediatR;

namespace Application.Artists.Update;

public record UpdateArtistCommand(Guid Id, string Name, string? Description): IRequest;

public record UpdateArtistRequest(string Name, string? Description);
