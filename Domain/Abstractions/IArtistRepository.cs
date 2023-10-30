using Domain.Entities;

namespace Domain.Abstractions;

public interface IArtistRepository
{
    Task<Artist?> GetArtistByIdAsync(Guid id);

    Task<Artist?> GetArtistByNameAsync(string name);
    void Add(Artist artist);
    void Remove(Artist artist);
}