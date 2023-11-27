﻿using Application.Common;
using Application.Data;
using Domain.Artists;
using Domain.Artists.Exceptions;
using Domain.Releases;
using MediatR;

namespace Application.Releases.Create;

public sealed class CreateReleaseCommandHandler: IRequestHandler<CreateReleaseCommand, ResultType<Guid>>
{
    private readonly IReleaseRepository _releaseRepository;
    private readonly IArtistRepository _artistRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cache;
    private readonly IMapper _mapper;

    public CreateReleaseCommandHandler(IReleaseRepository releaseRepository, IUnitOfWork unitOfWork, ICacheService cache, IMapper mapper, IArtistRepository artistRepository)
    {
        _releaseRepository = releaseRepository;
        _unitOfWork = unitOfWork;
        _cache = cache;
        _mapper = mapper;
        _artistRepository = artistRepository;
    }
    
    public async Task<ResultType<Guid>> Handle(CreateReleaseCommand request, CancellationToken cancellationToken)
    {
        // If Author of the release is not exist
        if (!await _artistRepository.Any(a => a.Id == request.AuthorId, cancellationToken))
        {
            return new ArtistNotFoundException(nameof(request.AuthorId));
        }
        
        var release = _mapper.MapToEntity(request);
        
        _releaseRepository.Add(release);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        await _cache.SetDataAsync(
            CachingKeys.ReleaseResponsePrefix + release.Id,
            _mapper.MapToResponse(release),
            DateTimeOffset.UtcNow.AddMinutes(1),
            cancellationToken
        );

        return release.Id;
    }
}