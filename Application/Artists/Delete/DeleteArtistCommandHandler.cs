using Application.Data;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Artists.Delete;

public sealed class DeleteArtistCommandHandler : IRequestHandler<DeleteArtistCommand>
{
    private readonly IArtistRepository _artistRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteArtistCommandHandler(IArtistRepository artistRepository, IUnitOfWork unitOfWork)
    {
        _artistRepository = artistRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = await _artistRepository.GetArtistByIdAsync(request.Id);

        if (artist is null)
        {
            throw new ArtistNotFoundException(request.Id);
        }

        _artistRepository.Remove(artist);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}