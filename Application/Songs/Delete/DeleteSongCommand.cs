using Application.Common;
using MediatR;

namespace Application.Songs.Delete;

public record DeleteSongCommand(Guid Id):IRequest<ResultType<bool>>;