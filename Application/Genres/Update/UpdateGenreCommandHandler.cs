using Application.Common;
using Application.Data;
using Domain.Songs;
using Domain.Songs.Exceptions;
using MediatR;

namespace Application.Genres.Update;

public class UpdateGenreCommandHandler:IRequestHandler<UpdateGenreCommand, ResultType<bool>>
{
    private readonly IGenreRepository _genreRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateGenreCommandHandler(IUnitOfWork unitOfWork, IGenreRepository genreRepository)
    {
        _unitOfWork = unitOfWork;
        _genreRepository = genreRepository;
    }

    public async Task<ResultType<bool>> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var genre = await _genreRepository.GetById(request.Id, cancellationToken);

        if (genre is null)
        {
            return new GenreNotFoundException(nameof(request.Id));
        }
        
        if (await _genreRepository.Any(g => g.Name == request.Name, cancellationToken))
        {
            return new GenreWithTheSameNameException(request.Name);
        }
        
        genre.Update(request.Name);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}