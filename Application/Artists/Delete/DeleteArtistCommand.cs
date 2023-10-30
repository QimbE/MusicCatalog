using MediatR;

namespace Application.Artists.Delete;

public record DeleteArtistCommand(Guid Id) : IRequest;