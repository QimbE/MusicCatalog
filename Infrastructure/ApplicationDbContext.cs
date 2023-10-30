using Application.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    public DbSet<Artist> Artists { get; set; }

    public ApplicationDbContext(DbContextOptions options)
        :base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}