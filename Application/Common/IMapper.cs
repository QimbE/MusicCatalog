using Application.Artists.Get;
using Application.DTO;
using Application.DTO.Release;
using Application.Releases.Create;
using Domain.Artists;
using Domain.Exceptions;
using Domain.Releases;
using FluentValidation.Results;

namespace Application.Common;

public interface IMapper
{
    public IEnumerable<PropertyError> MapToErrors(IEnumerable<ValidationFailure> failures);

    public ArtistResponse MapToResponse(Artist artist);

    public ReleaseResponse MapToResponse(Release release);

    public Release MapToEntity(CreateReleaseCommand request);
}