using System.Linq.Expressions;

namespace Domain.Releases;

public interface IReleaseRepository
{
    Task<Release?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<bool> Any(Expression<Func<Release, bool>> expression, CancellationToken cancellationToken = default);
    
    void Add(Release release);
    
    void Remove(Release release);
}