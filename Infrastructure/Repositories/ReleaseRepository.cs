using Domain.Releases;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReleaseRepository: IReleaseRepository
{
    private readonly ApplicationDbContext _context;

    public ReleaseRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<Release?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Releases
            .SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public void Add(Release release)
    {
        _context.Releases.Add(release);
    }

    public void Remove(Release release)
    {
        _context.Releases.Remove(release);
    }
}