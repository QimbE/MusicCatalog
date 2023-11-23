using System.Data;
using Application.Common;
using Application.Data;
using Domain.Users;
using Domain.Users.Exceptions;
using MediatR;

namespace Application.Users.Register;

public class RegisterCommandHandler: IRequestHandler<RegisterCommand, ResultType<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IHashProvider _hashProvider;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IHashProvider hashProvider, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _hashProvider = hashProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultType<string>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.Any(u=> u.Email == request.Email))
        {
            return new UserWithTheSameEmailException();
        }

        var hashedPassword = _hashProvider.Hash(request.Password);

        var user = User.Create(
            request.Username,
            request.Email,
            hashedPassword,
            Role.Default
            );
        
        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _jwtProvider.Generate(user);
    }
}