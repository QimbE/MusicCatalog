using System.Linq.Expressions;
using Domain.Songs;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenreRepository: IGenreRepository
{
    private readonly ApplicationDbContext _context;

    public GenreRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Genre?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.Genres.SingleOrDefaultAsync(g => g.Id == id, cancellationToken);
    }

    public Task<bool> Any(Expression<Func<Genre, bool>> expression, CancellationToken cancellationToken = default)
    {
        return _context.Genres.AnyAsync(expression, cancellationToken);
    }

    public void Add(Genre genre)
    {
        _context.Genres.Add(genre);
    }
}