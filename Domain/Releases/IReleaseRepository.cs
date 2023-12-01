using System.Linq.Expressions;

namespace Domain.Releases;

public interface IReleaseRepository
{
    Task<Release?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    void Add(Release release);
    
    void Remove(Release release);
}