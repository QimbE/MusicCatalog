using Application.Common;
using MediatR;

namespace Application.Users;

public record LoginCommand(string Username, string Email): IRequest<Result<string>>
{
    
}