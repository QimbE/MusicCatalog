using System.Linq.Expressions;

namespace Domain.Artists;

public interface IArtistRepository
{
    Task<Artist?> GetArtistByIdAsync(Guid id);

    Task<Artist?> GetArtistByNameAsync(string name);
    
    Task<bool> Any(Expression<Func<Artist, bool>> expression);
    
    void Add(Artist artist);
    
    void Remove(Artist artist);
}