﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231204120738_addSampleData")]
    partial class addSampleData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Artists.Artist", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Artists");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a8bbf255-aafd-432d-9ad8-ca2957fd83c5"),
                            Description = "Sir Elton John is a legendary British singer, songwriter, and pianist, known for his flamboyant style and timeless hits. With a career spanning decades, he's a global icon and one of the best-selling music artists in the world.",
                            Name = "Elton John"
                        },
                        new
                        {
                            Id = new Guid("da779457-fdaf-4f58-aaba-e385fbbdc603"),
                            Description = "Dua Lipa, a British pop sensation, has taken the music world by storm with her sultry vocals and chart-topping hits. Known for her genre-blending sound and captivating performances, she has become a prominent figure in contemporary pop music.",
                            Name = "Dua Lipa"
                        },
                        new
                        {
                            Id = new Guid("048fdf80-2296-4cd9-9e98-5a4f07e591bc"),
                            Description = "Known for their experimental and abrasive sound, Death Grips is an influential American experimental hip-hop trio. Combining elements of punk, industrial, and electronic music, they push the boundaries of conventional genres with their intense and raw sonic expressions.",
                            Name = "Death Grips"
                        });
                });

            modelBuilder.Entity("Domain.Junction.SongArtist", b =>
                {
                    b.Property<Guid>("ArtistId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SongId")
                        .HasColumnType("uuid");

                    b.HasKey("ArtistId", "SongId");

                    b.HasIndex("SongId");

                    b.ToTable("SongArtist");

                    b.HasData(
                        new
                        {
                            ArtistId = new Guid("a8bbf255-aafd-432d-9ad8-ca2957fd83c5"),
                            SongId = new Guid("1f8f5423-b7f9-4804-ab66-5880c5476a0a")
                        });
                });

            modelBuilder.Entity("Domain.Junction.SongUser", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SongId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId", "SongId");

                    b.HasIndex("SongId");

                    b.ToTable("SongUsers");
                });

            modelBuilder.Entity("Domain.Releases.Release", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("LinkToCover")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateOnly>("ReleaseDate")
                        .HasColumnType("date");

                    b.Property<int>("TypeId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("Name");

                    b.HasIndex("TypeId");

                    b.ToTable("Releases");

                    b.HasData(
                        new
                        {
                            Id = new Guid("044efc07-7658-4c4a-96cd-119956142480"),
                            AuthorId = new Guid("da779457-fdaf-4f58-aaba-e385fbbdc603"),
                            Description = "Some release description",
                            LinkToCover = "https://avatars.yandex.net/get-music-content/5457712/6fcf6795.a.18635265-1/m1000x1000?webp=false",
                            Name = "Cold Heart",
                            ReleaseDate = new DateOnly(2021, 8, 13),
                            TypeId = 2
                        },
                        new
                        {
                            Id = new Guid("9ed69dde-e57d-42ef-86f3-e10b969ce5e3"),
                            AuthorId = new Guid("048fdf80-2296-4cd9-9e98-5a4f07e591bc"),
                            Description = "Some release description",
                            LinkToCover = "https://sun9-78.userapi.com/impg/c857232/v857232147/176469/EkCl5P81Q8E.jpg?size=2048x2048&quality=96&sign=615509ee8cff2be10a48b45d2e0a507a&type=album",
                            Name = "Exmilitary",
                            ReleaseDate = new DateOnly(2011, 4, 25),
                            TypeId = 3
                        });
                });

            modelBuilder.Entity("Domain.Releases.ReleaseType", b =>
                {
                    b.Property<int>("Value")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Value"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Value");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ReleaseTypes");

                    b.HasData(
                        new
                        {
                            Value = 1,
                            Name = "Album"
                        },
                        new
                        {
                            Value = 3,
                            Name = "Mixtape"
                        },
                        new
                        {
                            Value = 2,
                            Name = "Single"
                        });
                });

            modelBuilder.Entity("Domain.Songs.Genre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            Id = new Guid("aa8087bd-cccb-4013-b627-e6265a9611f9"),
                            Name = "Pop music"
                        },
                        new
                        {
                            Id = new Guid("aaa5fd8d-2a2d-4e8a-9acc-2f5778daed8c"),
                            Name = "Punk-rap"
                        });
                });

            modelBuilder.Entity("Domain.Songs.Song", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AudioLink")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("character varying(2000)");

                    b.Property<Guid>("GenreId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<Guid>("ReleaseId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("Name");

                    b.HasIndex("ReleaseId");

                    b.ToTable("Songs");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1f8f5423-b7f9-4804-ab66-5880c5476a0a"),
                            AudioLink = "about:blank",
                            GenreId = new Guid("aa8087bd-cccb-4013-b627-e6265a9611f9"),
                            Name = "Cold Heart",
                            ReleaseId = new Guid("044efc07-7658-4c4a-96cd-119956142480")
                        },
                        new
                        {
                            Id = new Guid("33f61113-cd1c-4218-9a1f-2e27ef0ed4f9"),
                            AudioLink = "about:blank",
                            GenreId = new Guid("aaa5fd8d-2a2d-4e8a-9acc-2f5778daed8c"),
                            Name = "Beware",
                            ReleaseId = new Guid("9ed69dde-e57d-42ef-86f3-e10b969ce5e3")
                        },
                        new
                        {
                            Id = new Guid("875d4ada-612b-43f8-b005-88fc57ced0be"),
                            AudioLink = "about:blank",
                            GenreId = new Guid("aaa5fd8d-2a2d-4e8a-9acc-2f5778daed8c"),
                            Name = "Guillotine",
                            ReleaseId = new Guid("9ed69dde-e57d-42ef-86f3-e10b969ce5e3")
                        },
                        new
                        {
                            Id = new Guid("de257ed2-5f8b-43c2-be0f-b291c263473a"),
                            AudioLink = "about:blank",
                            GenreId = new Guid("aaa5fd8d-2a2d-4e8a-9acc-2f5778daed8c"),
                            Name = "Spread Eagle Cross The Block",
                            ReleaseId = new Guid("9ed69dde-e57d-42ef-86f3-e10b969ce5e3")
                        },
                        new
                        {
                            Id = new Guid("a3b492ca-c29b-40af-86c9-4192acc369c1"),
                            AudioLink = "about:blank",
                            GenreId = new Guid("aaa5fd8d-2a2d-4e8a-9acc-2f5778daed8c"),
                            Name = "Lord of the Game",
                            ReleaseId = new Guid("9ed69dde-e57d-42ef-86f3-e10b969ce5e3")
                        });
                });

            modelBuilder.Entity("Domain.Users.Role", b =>
                {
                    b.Property<int>("Value")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Value"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Value");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Value = 3,
                            Name = "Admin"
                        },
                        new
                        {
                            Value = 2,
                            Name = "DatabaseAdmin"
                        },
                        new
                        {
                            Value = 1,
                            Name = "Default"
                        });
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("character varying(254)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("character varying(70)");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Domain.Junction.SongArtist", b =>
                {
                    b.HasOne("Domain.Artists.Artist", "Artist")
                        .WithMany("SongArtists")
                        .HasForeignKey("ArtistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Songs.Song", "Song")
                        .WithMany("SongArtists")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Artist");

                    b.Navigation("Song");
                });

            modelBuilder.Entity("Domain.Junction.SongUser", b =>
                {
                    b.HasOne("Domain.Songs.Song", "Song")
                        .WithMany("SongUsers")
                        .HasForeignKey("SongId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Users.User", "User")
                        .WithMany("SongUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Song");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Releases.Release", b =>
                {
                    b.HasOne("Domain.Artists.Artist", "Author")
                        .WithMany("Releases")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Releases.ReleaseType", "Type")
                        .WithMany("Releases")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Domain.Songs.Song", b =>
                {
                    b.HasOne("Domain.Songs.Genre", "Genre")
                        .WithMany("Songs")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Releases.Release", "Release")
                        .WithMany("Songs")
                        .HasForeignKey("ReleaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Genre");

                    b.Navigation("Release");
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.HasOne("Domain.Users.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Artists.Artist", b =>
                {
                    b.Navigation("Releases");

                    b.Navigation("SongArtists");
                });

            modelBuilder.Entity("Domain.Releases.Release", b =>
                {
                    b.Navigation("Songs");
                });

            modelBuilder.Entity("Domain.Releases.ReleaseType", b =>
                {
                    b.Navigation("Releases");
                });

            modelBuilder.Entity("Domain.Songs.Genre", b =>
                {
                    b.Navigation("Songs");
                });

            modelBuilder.Entity("Domain.Songs.Song", b =>
                {
                    b.Navigation("SongArtists");

                    b.Navigation("SongUsers");
                });

            modelBuilder.Entity("Domain.Users.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Domain.Users.User", b =>
                {
                    b.Navigation("SongUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
