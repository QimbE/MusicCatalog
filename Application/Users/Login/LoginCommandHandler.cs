using System.Security.Authentication;
using Application.Common;
using Domain.Users;
using MediatR;

namespace Application.Users.Login;

public sealed class LoginCommandHandler:IRequestHandler<LoginCommand, ResultType<string>>
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

    public async Task<ResultType<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userFromDb  = await _repository.GetByEmailAsync(request.Email, "Role");

        if (userFromDb is null || !
            _hashProvider.Verify(userFromDb.Password, request.Password))
        {
            return new InvalidCredentialException("Invalid credentials");
        }
        
        string token = _jwtProvider.Generate(userFromDb);

        return token;
    }
}