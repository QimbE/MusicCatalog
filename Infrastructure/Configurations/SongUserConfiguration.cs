using Domain.Junction;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SongUserConfiguration: IEntityTypeConfiguration<SongUser>
{
    public void Configure(EntityTypeBuilder<SongUser> builder)
    {
        builder.HasKey(s => new { s.UserId, s.SongId });
    }
}