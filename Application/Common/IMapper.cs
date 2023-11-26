using Application.Artists.Get;
using Application.DTO;
using Domain.Artists;
using Domain.Exceptions;
using FluentValidation.Results;

namespace Application.Common;

public interface IMapper
{
    public IEnumerable<PropertyError> MapToErrors(IEnumerable<ValidationFailure> failures);

    public ArtistResponse MapToResponse(Artist artist);
}