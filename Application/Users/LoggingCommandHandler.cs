using System.Security.Authentication;
using Application.Common;
using Domain.Users;
using MediatR;

namespace Application.Users;

public sealed class LoggingCommandHandler:IRequestHandler<LoginCommand, Result<string>>
{

    private readonly IUserRepository _repository;
    private readonly IJwtProvider _jwtProvider;

    public LoggingCommandHandler(IUserRepository repository, IJwtProvider jwtProvider)
    {
        _repository = repository;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userFromDb  = await _repository.GetByEmailAsync(request.Email);

        if (userFromDb is null)
        {
            return new InvalidCredentialException("Invalid credentials");
        }
        
        //todo: password check
        
        string token = _jwtProvider.Generate(userFromDb);

        return token;
    }
}