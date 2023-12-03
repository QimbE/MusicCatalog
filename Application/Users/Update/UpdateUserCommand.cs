using Application.Common;
using MediatR;

namespace Application.Users.Update;

public record UpdateUserCommand(Guid Id, string Username, int RoleId):IRequest<ResultType<bool>>;