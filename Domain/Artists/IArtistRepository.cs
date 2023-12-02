using System.Linq.Expressions;

namespace Domain.Artists;

public interface IArtistRepository
{
    Task<Artist?> GetArtistByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    Task<bool> Any(Expression<Func<Artist, bool>> expression, CancellationToken cancellationToken = default);

    Task<int> Count(Expression<Func<Artist, bool>> expression, CancellationToken cancellationToken = default);
    
    void Add(Artist artist);
    
    void Remove(Artist artist);
}