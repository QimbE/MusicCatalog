using Application.Common;
using MediatR;

namespace Application.Releases.Delete;

public record DeleteReleaseCommand(Guid Id):IRequest<ResultType<bool>>;