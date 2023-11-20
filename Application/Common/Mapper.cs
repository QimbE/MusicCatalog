using Application.Artists.Get;
using Domain.Artists;
using Domain.Exceptions;
using FluentValidation.Results;
using Riok.Mapperly.Abstractions;

namespace Application.Common;

[Mapper]
public partial class Mapper : IMapper
{
    public partial IEnumerable<PropertyError> MapToErrors(IEnumerable<ValidationFailure> failures);

    public partial ArtistResponse MapToResponse(Artist artist);
}