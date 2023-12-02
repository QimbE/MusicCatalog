using Domain.Junction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SongArtistConfiguration: IEntityTypeConfiguration<SongArtist>
{
    public void Configure(EntityTypeBuilder<SongArtist> builder)
    {
        builder.HasKey(s => new { s.ArtistId, s.SongId });
    }
}