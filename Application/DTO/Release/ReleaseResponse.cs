namespace Application.DTO.Release;

public record ReleaseResponse(Guid Id,
    ArtistResponseForRelease Author,
    string Name,
    string? Description,
    string Type,
    DateOnly ReleaseDate,
    string LinkToCover,
    List<SongResponseForRelease> Songs
    );