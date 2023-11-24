using Application.Common;
using MediatR;

namespace Application.Authorization.Register;

public record RegisterCommand(string Username, string Email, string Password): IRequest<ResultType<string>>;