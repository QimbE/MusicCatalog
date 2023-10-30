using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal class ArtistConfiguration : IEntityTypeConfiguration<Artist>
{
    public void Configure(EntityTypeBuilder<Artist> builder)
    {
        // PK
        builder.HasKey(a => a.Id);

        // Indexes
        builder.HasIndex(a => a.Name).IsUnique();
        
        #region Properties
        builder.Property(x => x.Name)
            .HasMaxLength(100) //nvarchar(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(2000);
        #endregion
    }
}