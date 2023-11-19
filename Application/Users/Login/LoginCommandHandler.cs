using System.Security.Authentication;
using Application.Common;
using Domain.Users;
using MediatR;

namespace Application.Users.Login;

public sealed class LoginCommandHandler:IRequestHandler<LoginCommand, Result<string>>
{

    private readonly IUserRepository _repository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IHashProvider _hashProvider;

    public LoginCommandHandler(IUserRepository repository, IJwtProvider jwtProvider, IHashProvider hashProvider)
    {
        _repository = repository;
        _jwtProvider = jwtProvider;
        _hashProvider = hashProvider;
    }

    public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userFromDb  = await _repository.GetByEmailAsync(request.Email);

        if (userFromDb is null ||
            _hashProvider.Verify(userFromDb.Password, request.Password))
        {
            return new InvalidCredentialException("Invalid credentials");
        }
        
        string token = _jwtProvider.Generate(userFromDb);

        return token;
    }
}