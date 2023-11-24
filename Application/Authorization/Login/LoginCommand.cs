using Application.Common;
using MediatR;

namespace Application.Authorization.Login;

public record LoginCommand(string Email, string Password): IRequest<ResultType<string>>
{
    
}