using Application.Common;
using Application.Data;
using Domain.Releases;
using Domain.Releases.Exceptions;
using MediatR;

namespace Application.Releases.Delete;

public class DeleteReleaseCommandHandler:IRequestHandler<DeleteReleaseCommand, ResultType<bool>>
{
    private readonly IReleaseRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteReleaseCommandHandler(IReleaseRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultType<bool>> Handle(DeleteReleaseCommand request, CancellationToken cancellationToken)
    {
        var release = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (release is null)
        {
            return new ReleaseNotFoundException(nameof(request.Id));
        }
        
        _repository.Remove(release);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}