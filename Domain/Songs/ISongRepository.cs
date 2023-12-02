namespace Domain.Songs;

public interface ISongRepository
{
    Task<Song?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    void Add(Song song);
    
    void Remove(Song song);
}