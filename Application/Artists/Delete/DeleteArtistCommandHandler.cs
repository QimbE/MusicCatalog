using Application.Common;
using Application.Data;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Artists.Delete;

public sealed class DeleteArtistCommandHandler : IRequestHandler<DeleteArtistCommand, Result<bool>>
{
    private readonly IArtistRepository _artistRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteArtistCommandHandler(IArtistRepository artistRepository, IUnitOfWork unitOfWork)
    {
        _artistRepository = artistRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteArtistCommand request, CancellationToken cancellationToken)
    {
        var artist = await _artistRepository.GetArtistByIdAsync(request.Id);

        if (artist is null)
        {
            return new ArtistNotFoundException(request.Id.ToString());
        }

        _artistRepository.Remove(artist);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return true;
    }
}