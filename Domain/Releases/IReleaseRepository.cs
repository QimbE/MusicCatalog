using System.Linq.Expressions;

namespace Domain.Releases;

public interface IReleaseRepository
{
    void Add(Release release);
    
    void Remove(Release release);
}