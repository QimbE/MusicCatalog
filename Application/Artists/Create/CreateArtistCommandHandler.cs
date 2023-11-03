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

    public CreateArtistCommandHandler(IArtistRepository artistRepository, IUnitOfWork unitOfWork)
    {
        _artistRepository = artistRepository;
        _unitOfWork = unitOfWork;
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

        return artist.Id;
    }
}