﻿using Domain.Songs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

public class GenreConfiguration: IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(g => g.Id);

        builder.HasIndex(g => g.Name)
            .IsUnique();

        builder.Property(g => g.Name)
            .HasMaxLength(100).IsRequired();
    }
}