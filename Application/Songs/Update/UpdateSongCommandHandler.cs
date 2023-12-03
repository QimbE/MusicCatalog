using Application.Common;
using Application.Data;
using Domain.Artists;
using Domain.Artists.Exceptions;
using Domain.Releases;
using Domain.Releases.Exceptions;
using Domain.Songs;
using Domain.Songs.Exceptions;
using MediatR;

namespace Application.Songs.Update;

public class UpdateSongCommandHandler:IRequestHandler<UpdateSongCommand, ResultType<bool>>
{
    private readonly IReleaseRepository _releaseRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IArtistRepository _artistRepository;
    private readonly ISongRepository _songRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateSongCommandHandler(IReleaseRepository releaseRepository, IGenreRepository genreRepository, IArtistRepository artistRepository, ISongRepository songRepository, IUnitOfWork unitOfWork)
    {
        _releaseRepository = releaseRepository;
        _genreRepository = genreRepository;
        _artistRepository = artistRepository;
        _songRepository = songRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResultType<bool>> Handle(UpdateSongCommand request, CancellationToken cancellationToken)
    {
        var song = await _songRepository.GetByIdAsync(request.Id, cancellationToken);

        if (song is null)
        {
            return new SongNotFoundException(nameof(request.Id));
        }
        
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
        
        song.Update(request.ReleaseId, request.GenreId, request.Name, request.AudioLink, request.ArtistOnFeatIds);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}