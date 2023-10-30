using Application.Common;
using Application.Data;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Artists.Create;

public sealed class CreateArtistCommandHandler : IRequestHandler<CreateArtistCommand, Result<Guid>>
{
    private readonly IArtistRepository _artistRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateArtistCommandHandler(IArtistRepository artistRepository, IUnitOfWork unitOfWork)
    {
        _artistRepository = artistRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = new Artist(Guid.NewGuid(), request.Name, request.Description);

        var artistWithTheSameName = _artistRepository.GetArtistByNameAsync(request.Name);

        if (artistWithTheSameName is not null)
        {
            return new ArtistWithTheSameNameException(request.Name);
        }
        
        _artistRepository.Add(artist);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return artist.Id;
    }
}