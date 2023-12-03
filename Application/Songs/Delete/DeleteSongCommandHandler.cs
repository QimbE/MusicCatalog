using Application.Common;
using Application.Data;
using Domain.Songs;
using Domain.Songs.Exceptions;
using MediatR;

namespace Application.Songs.Delete;

public class DeleteSongCommandHandler : IRequestHandler<DeleteSongCommand, ResultType<bool>>
{
    private readonly ISongRepository _repository;
    private readonly IUnitOfWork _unitOfWork;


    public DeleteSongCommandHandler(ISongRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultType<bool>> Handle(DeleteSongCommand request, CancellationToken cancellationToken)
    {
        var song = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (song is null)
        {
            return new SongNotFoundException(nameof(request.Id));
        }
        
        _repository.Remove(song);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}