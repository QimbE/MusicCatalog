using Domain.Artists;
using Domain.Junction;
using Domain.Releases;
using Domain.Songs;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Application.Data;

public interface IApplicationDbContext
{
    DbSet<Artist> Artists { get; set; }
    
    DbSet<Release> Releases { get; set; }
    
    DbSet<Song> Songs { get; set; }
    
    DbSet<Genre> Genres { get; set; }
    
    DbSet<User> Users { get; set; }
    
    DbSet<SongUser> SongUsers { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken =default);
}