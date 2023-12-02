using Application.Common;
using Application.Data;
using Domain.Artists;
using Domain.Artists.Exceptions;
using Domain.Releases;
using Domain.Releases.Exceptions;
using Domain.Songs;
using Domain.Songs.Exceptions;
using MediatR;

namespace Application.Songs.Create;

public class CreateSongCommandHandler:IRequestHandler<CreateSongCommand, ResultType<Guid>>
{
    private readonly IReleaseRepository _releaseRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IArtistRepository _artistRepository;
    private readonly ISongRepository _songRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public CreateSongCommandHandler(
        IReleaseRepository releaseRepository,
        IGenreRepository genreRepository,
        IArtistRepository artistRepository,
        IMapper mapper,
        ISongRepository songRepository,
        IUnitOfWork unitOfWork
        )
    {
        _releaseRepository = releaseRepository;
        _genreRepository = genreRepository;
        _artistRepository = artistRepository;
        _mapper = mapper;
        _songRepository = songRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultType<Guid>> Handle(CreateSongCommand request, CancellationToken cancellationToken)
    {
        var release = await _releaseRepository.GetByIdAsync(request.ReleaseId, cancellationToken);
        
        if (release is null)
        {
            return new ReleaseNotFoundException(nameof(request.ReleaseId));
        }

        if (request.ArtistOnFeatIds.Contains(release.AuthorId))
        {
            return new AuthorOnFeatException();
        }

        if (!await _genreRepository.Any(g => g.Id == request.GenreId, cancellationToken))
        {
            return new GenreNotFoundException(nameof(request.GenreId));
        }

        if (await _artistRepository.Count(
                a => request.ArtistOnFeatIds.Contains(a.Id), 
                cancellationToken
                ) !=
            request.ArtistOnFeatIds.Count)
        {
            return new ArtistNotFoundException(nameof(request.ArtistOnFeatIds));
        }

        var song = _mapper.MapToEntity(request);
        
        _songRepository.Add(song);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return song.Id;
    }
}