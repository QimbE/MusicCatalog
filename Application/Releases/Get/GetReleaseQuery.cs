using Application.Common;
using Application.DTO.Release;
using MediatR;

namespace Application.Releases.Get;

public record GetReleaseQuery(Guid Id): IRequest<ResultType<ReleaseResponse>>;