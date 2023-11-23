using Application.Common;
using MediatR;

namespace Application.Artists.Delete;

public record DeleteArtistCommand(Guid Id) : IRequest<ResultType<bool>>;