using Application.Data;
using Domain.Abstractions;
using Domain.Exceptions;
using MediatR;

namespace Application.Artists.Update;

public sealed class UpdateArtistCommandHandler: IRequestHandler<UpdateArtistCommand>
{
    private readonly IArtistRepository _artistRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateArtistCommandHandler(IArtistRepository artistRepository, IUnitOfWork unitOfWork)
    {
        _artistRepository = artistRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(UpdateArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = await _artistRepository.GetArtistByIdAsync(request.Id);

        if (artist is null)
        {
           throw new ArtistNotFoundException(request.Id);
        }

        var artistWithTheSameName = _artistRepository.GetArtistByNameAsync(request.Name);

        if (artistWithTheSameName is not null)
        {
            throw new ArtistWithTheSameNameException(request.Name);
        }

        artist.Update(request.Name, request.Description);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}