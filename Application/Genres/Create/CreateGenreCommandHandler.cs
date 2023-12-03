using Application.Common;
using Application.Data;
using Domain.Songs;
using Domain.Songs.Exceptions;
using MediatR;

namespace Application.Genres.Create;

public class CreateGenreCommandHandler: IRequestHandler<CreateGenreCommand, ResultType<Guid>>
{
    private readonly IGenreRepository _genreRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateGenreCommandHandler(IGenreRepository genreRepository, IUnitOfWork unitOfWork)
    {
        _genreRepository = genreRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultType<Guid>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        if (await _genreRepository.Any(g => g.Name == request.Name, cancellationToken))
        {
            return new GenreWithTheSameNameException(request.Name);
        }

        var genre = Genre.Create(request.Name);
        
        _genreRepository.Add(genre);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return genre.Id;
    }
}