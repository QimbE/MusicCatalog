﻿using Application.Data;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Artists.Create;

public sealed class CreateArtistCommandHandler : IRequestHandler<CreateArtistCommand>
{
    private readonly IArtistRepository _artistRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateArtistCommandHandler(IArtistRepository artistRepository, IUnitOfWork unitOfWork)
    {
        _artistRepository = artistRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = new Artist(Guid.NewGuid(), request.Name, request.Description);

        var artistWithTheSameName = _artistRepository.GetArtistByNameAsync(request.Name);

        if (artistWithTheSameName is not null)
        {
            throw new ArtistWithTheSameNameException(request.Name);
        }
        
        _artistRepository.Add(artist);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}