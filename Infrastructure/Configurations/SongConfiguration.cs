﻿using Domain.Junction;
using Domain.Songs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class SongConfiguration: IEntityTypeConfiguration<Song>
{
    public void Configure(EntityTypeBuilder<Song> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasIndex(s => s.Name);

        builder.Property(s => s.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(s => s.AudioLink)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(s => s.ReleaseId).IsRequired();

        builder.Property(s => s.GenreId).IsRequired();
        
        builder
            .HasOne(s => s.Release)
            .WithMany(r => r.Songs)
            .HasForeignKey(s => s.ReleaseId);
        
        builder
            .HasOne(s => s.Genre)
            .WithMany(g => g.Songs)
            .HasForeignKey(s => s.GenreId);

        builder
            .HasMany(s => s.ArtistsOnFeat)
            .WithMany(a => a.SongsOnFeat)
            .UsingEntity<SongArtist>(
                sa => sa
                    .HasOne(sa => sa.Artist)
                    .WithMany(a => a.SongArtists)
                    .HasForeignKey(sa => sa.ArtistId),
                sa => sa
                    .HasOne(sa => sa.Song)
                    .WithMany(s => s.SongArtists)
                    .HasForeignKey(sa => sa.SongId)
                );

        builder
            .HasMany(s => s.UsersWhoAdded)
            .WithMany(u => u.FavouriteSongs).UsingEntity<SongUser>(
                sa => sa
                    .HasOne(sa => sa.User)
                    .WithMany(u => u.SongUsers)
                    .HasForeignKey(sa => sa.UserId),
                sa => sa
                    .HasOne(sa => sa.Song)
                    .WithMany(s => s.SongUsers)
                    .HasForeignKey(sa => sa.SongId)
                );
    }
}