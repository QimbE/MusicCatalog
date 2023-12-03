using Application.Common;
using Application.Data;
using Domain.Users;
using Domain.Users.Exceptions;
using MediatR;

namespace Application.Users.Update;

public class UpdateUserCommandHandler:IRequestHandler<UpdateUserCommand, ResultType<bool>>
{
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUserRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultType<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetById(request.Id, cancellationToken);

        if (user is null)
        {
            return new UserNotFoundException(nameof(request.Id));
        }
        
        user.Update(request.Username, user.Email, user.Password, request.RoleId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}