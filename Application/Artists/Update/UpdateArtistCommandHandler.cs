using Application.Common;
using Application.Data;
using Domain.Artists;
using Domain.Artists.Exceptions;
using Domain.Exceptions;
using MediatR;

namespace Application.Artists.Update;

public sealed class UpdateArtistCommandHandler: IRequestHandler<UpdateArtistCommand, Result<bool>>
{
    private readonly IArtistRepository _artistRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cache;
    private readonly IMapper _mapper;

    public UpdateArtistCommandHandler(IArtistRepository artistRepository, IUnitOfWork unitOfWork, ICacheService cache, IMapper mapper)
    {
        _artistRepository = artistRepository;
        _unitOfWork = unitOfWork;
        _cache = cache;
        _mapper = mapper;
    }

    public async Task<Result<bool>> Handle(UpdateArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = await _artistRepository.GetArtistByIdAsync(request.Id);

        if (artist is null)
        {
           return new ArtistNotFoundException(nameof(request.Id));
        }

        if (await _artistRepository.Any(a=> a.Name == request.Name))
        {
            return new ArtistWithTheSameNameException(request.Name);
        }

        artist.Update(request.Name, request.Description);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        await _cache.SetDataAsync(
            CachingKeys.ArtistResponsePrefix + artist.Id,
            _mapper.MapToResponse(artist),
            DateTimeOffset.UtcNow.AddMinutes(1),
            cancellationToken
            );

        return true;
    }
}