using Application.Common;
using Application.Data;
using Domain.Artists;
using Domain.Artists.Exceptions;
using Domain.Exceptions;
using MediatR;

namespace Application.Artists.Delete;

public sealed class DeleteArtistCommandHandler : IRequestHandler<DeleteArtistCommand, Result<bool>>
{
    private readonly IArtistRepository _artistRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cache;

    public DeleteArtistCommandHandler(IArtistRepository artistRepository, IUnitOfWork unitOfWork, ICacheService cache)
    {
        _artistRepository = artistRepository;
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    public async Task<Result<bool>> Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = await _artistRepository.GetArtistByIdAsync(request.Id);

        if (artist is null)
        {
            return new ArtistNotFoundException(nameof(request.Id));
        }

        _artistRepository.Remove(artist);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _cache.RemoveDataAsync(CachingKeys.ArtistResponsePrefix + artist.Id, cancellationToken);

        return true;
    }
}