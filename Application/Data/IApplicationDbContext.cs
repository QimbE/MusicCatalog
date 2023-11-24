using Domain.Artists;
using Domain.Releases;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<Artist> Artists { get; set; }
    
    DbSet<Release> Releases { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken =default);
}