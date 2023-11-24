using Domain.Releases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class ReleaseConfiguration: IEntityTypeConfiguration<Release>
{
    public void Configure(EntityTypeBuilder<Release> builder)
    {
        builder.HasKey(r => r.Id);


        builder.HasIndex(r => r.Name);
        
        
        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.Description)
            .HasMaxLength(1000);

        builder.Property(r => r.ReleaseDate)
            .IsRequired();

        builder.Property(r => r.LinkToCover)
            .HasMaxLength(2000);

        builder
            .HasOne(r => r.Author)
            .WithMany(a => a.Releases)
            .HasForeignKey(r => r.AuthorId);
    }
}