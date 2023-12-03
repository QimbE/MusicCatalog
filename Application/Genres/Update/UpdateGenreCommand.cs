using Application.Common;
using MediatR;

namespace Application.Genres.Update;

public record UpdateGenreCommand(Guid Id, string Name):IRequest<ResultType<bool>>;