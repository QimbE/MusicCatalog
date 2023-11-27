namespace Application.DTO.Artist;

public record ReleaseResponseForArtist(
    Guid Id,
    string Name,
    string Type,
    DateOnly ReleaseDate,
    string LinkToCover
    );