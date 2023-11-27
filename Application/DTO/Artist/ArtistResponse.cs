namespace Application.DTO.Artist;

public record ArtistResponse(Guid Id, string Name, string? Description, List<ReleaseResponseForArtist> Releases);