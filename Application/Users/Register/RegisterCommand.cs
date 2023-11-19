using Application.Common;
using MediatR;

namespace Application.Users.Register;

public record RegisterCommand(string Username, string Email, string Password): IRequest<Result<string>>;