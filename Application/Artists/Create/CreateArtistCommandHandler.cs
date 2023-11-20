using Application.Common;
using Application.Data;
using Domain.Artists;
using Domain.Artists.Exceptions;
using Domain.Exceptions;
using MediatR;

namespace Application.Artists.Create;

public sealed class CreateArtistCommandHandler : IRequestHandler<CreateArtistCommand, Result<Guid>>
{
    private readonly IArtistRepository _artistRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cache;
    private readonly Mapper _mapper;

    public CreateArtistCommandHandler(IArtistRepository artistRepository, IUnitOfWork unitOfWork, ICacheService cache, Mapper mapper)
    {
        _artistRepository = artistRepository;
        _unitOfWork = unitOfWork;
        _cache = cache;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(CreateArtistCommand request, CancellationToken cancellationToken)
    {
        if (await _artistRepository.Any(a=> a.Name == request.Name))
        {
            return new ArtistWithTheSameNameException(request.Name);
        }
        
        var artist = Artist.Create(request.Name, request.Description);
        
        _artistRepository.Add(artist);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // caching
        await _cache.SetDataAsync(
            CachingKeys.ArtistResponsePrefix + artist.Id,
            _mapper.MapToResponse(artist),
            DateTimeOffset.UtcNow.AddMinutes(1),
            cancellationToken
        );
        
        return artist.Id;
    }
}