using System.Linq.Expressions;

namespace Domain.Songs;

public interface IGenreRepository
{
    Task<Genre?> GetById(Guid id, CancellationToken cancellationToken = default);
    
    Task<bool> Any(Expression<Func<Genre, bool>> expression, CancellationToken cancellationToken = default);

    void Add(Genre genre);
}