using Application.Artists.Get;
using Application.DTO;
using Application.DTO.Artist;
using Application.DTO.Release;
using Application.Releases.Create;
using Domain.Artists;
using Domain.Exceptions;
using Domain.Releases;
using FluentValidation.Results;
using Riok.Mapperly.Abstractions;

namespace Application.Common;

[Mapper]
public partial class Mapper : IMapper
{
    public partial IEnumerable<PropertyError> MapToErrors(IEnumerable<ValidationFailure> failures);
    public partial IQueryable<ArtistResponse> MapToResponse(IQueryable<Artist> query);

    public partial ArtistResponse MapToResponse(Artist artist);
    
    public partial ReleaseResponse MapToResponse(Release release);

    public Release MapToEntity(CreateReleaseCommand request)
    {
        return Release.Create(request.Name, request.Description, request.ReleaseDate, request.LinkToCover,
            request.AuthorId, request.TypeId);
    }
}