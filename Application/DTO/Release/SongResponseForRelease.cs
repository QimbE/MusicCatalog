namespace Application.DTO.Release;

public record SongResponseForRelease(Guid Id, string Name, string AudioLink, GenreResponse Genre, List<ArtistResponseForRelease> ArtistsOnFeat);