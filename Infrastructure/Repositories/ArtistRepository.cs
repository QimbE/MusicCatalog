using System.Linq.Expressions;
using Domain.Artists;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal sealed class ArtistRepository : IArtistRepository
{
    private readonly ApplicationDbContext _context;

    public ArtistRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Artist?> GetArtistByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.Artists
            .SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public Task<bool> Any(Expression<Func<Artist, bool>> expression, CancellationToken cancellationToken = default)
    {
        return _context.Artists.AnyAsync(expression, cancellationToken);
    }

    public Task<int> Count(Expression<Func<Artist, bool>> expression, CancellationToken cancellationToken = default)
    {
        return _context.Artists.CountAsync(expression, cancellationToken);
    }

    public void Add(Artist artist)
    {
        _context.Artists.Add(artist);
    }

    public void Remove(Artist artist)
    {
        _context.Artists.Remove(artist);
    }
}