using Application.Common;
using Application.Data;
using Domain.Abstractions;
using Domain.Exceptions;
using MediatR;

namespace Application.Artists.Update;

public sealed class UpdateArtistCommandHandler: IRequestHandler<UpdateArtistCommand, Result<bool>>
{
    private readonly IArtistRepository _artistRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateArtistCommandHandler(IArtistRepository artistRepository, IUnitOfWork unitOfWork)
    {
        _artistRepository = artistRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(UpdateArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = await _artistRepository.GetArtistByIdAsync(request.Id);

        if (artist is null)
        {
           return new ArtistNotFoundException(request.Id.ToString());
        }

        var artistWithTheSameName = _artistRepository.GetArtistByNameAsync(request.Name);

        if (artistWithTheSameName is not null)
        {
            return new ArtistWithTheSameNameException(request.Name);
        }

        artist.Update(request.Name, request.Description);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}