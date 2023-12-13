using Application.Common;
using Application.DTO;
using MediatR;

namespace Application.Users.Get;

public record GetThisUserQuery(Guid Id):IRequest<ResultType<ThisUserDTO>>;