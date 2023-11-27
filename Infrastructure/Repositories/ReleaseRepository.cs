using Domain.Releases;

namespace Infrastructure.Repositories;

public class ReleaseRepository: IReleaseRepository
{
    private readonly ApplicationDbContext _context;

    public ReleaseRepository(ApplicationDbContext context)
    {
        _context = context;
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