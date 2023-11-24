using Domain.Releases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ReleaseTypeConfiguration: IEntityTypeConfiguration<ReleaseType>
{
    public void Configure(EntityTypeBuilder<ReleaseType> builder)
    {
        builder.HasKey(t => t.Value);

        
        builder.HasIndex(t => t.Name).IsUnique();


        builder.Property(t => t.Name)
            .HasMaxLength(100)
            .IsRequired();
        
        
        // One to many between ReleaseType and Release
        builder
            .HasMany(t => t.Releases)
            .WithOne(r => r.Type)
            .HasForeignKey(r => r.TypeId);

        builder.HasData(ReleaseType.List);
    }
}