using Application.Data;
using Domain.Artists;
using Domain.Releases;
using Domain.Songs;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    public DbSet<Artist> Artists { get; set; }
    
    public DbSet<Role> Roles { get; set; }
    
    public DbSet<Release> Releases { get; set; }
    
    public DbSet<ReleaseType> ReleaseTypes { get; set; }
    
    
    public DbSet<Song> Songs { get; set; }
    
    public DbSet<Genre> Genres { get; set; }
    
    public DbSet<User> Users { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}