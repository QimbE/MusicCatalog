using Application.Common;
using MediatR;

namespace Application.Genres.Create;

public record CreateGenreCommand(string Name): IRequest<ResultType<Guid>>;