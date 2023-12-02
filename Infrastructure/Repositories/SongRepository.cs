using Domain.Songs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SongRepository: ISongRepository
{
    private readonly ApplicationDbContext _context;

    public SongRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Song?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Songs
            .SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public void Add(Song song)
    {
        _context.Songs.Add(song);
    }

    public void Remove(Song song)
    {
        _context.Songs.Remove(song);
    }
}