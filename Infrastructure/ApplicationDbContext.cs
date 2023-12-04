using Application.Data;
using Domain.Artists;
using Domain.Junction;
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
    
    public DbSet<SongUser> SongUsers { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        :base(options)
    {
        
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        var artists = new List<Artist>
        {
            Artist.Create(
                "Elton John",
                "Sir Elton John is a legendary British singer, songwriter, and pianist, known for his flamboyant style and timeless hits. With a career spanning decades, he's a global icon and one of the best-selling music artists in the world."
            ),
            Artist.Create(
                "Dua Lipa",
                "Dua Lipa, a British pop sensation, has taken the music world by storm with her sultry vocals and chart-topping hits. Known for her genre-blending sound and captivating performances, she has become a prominent figure in contemporary pop music."
            ),
            Artist.Create(
                "Death Grips",
                "Known for their experimental and abrasive sound, Death Grips is an influential American experimental hip-hop trio. Combining elements of punk, industrial, and electronic music, they push the boundaries of conventional genres with their intense and raw sonic expressions."
            )
        };

        var releases = new List<Release>
        {
            Release.Create("Cold Heart", "Some release description", new DateTime(2021, 8, 13), "https://avatars.yandex.net/get-music-content/5457712/6fcf6795.a.18635265-1/m1000x1000?webp=false", artists[1].Id, ReleaseType.Single),
            Release.Create("Exmilitary", "Some release description", new DateTime(2011, 4, 25), "https://sun9-78.userapi.com/impg/c857232/v857232147/176469/EkCl5P81Q8E.jpg?size=2048x2048&quality=96&sign=615509ee8cff2be10a48b45d2e0a507a&type=album", artists[2].Id, ReleaseType.Mixtape)
        };

        var genres = new List<Genre>
        {
            Genre.Create("Pop music"),
            Genre.Create("Punk-rap")
        };

        var songs = new List<Song>
        {
            Song.Create(releases[0].Id, genres[0].Id, "Cold Heart", "about:blank", new List<Guid>{}),
            Song.Create(releases[1].Id, genres[1].Id, "Beware", "about:blank", new List<Guid>{}),
            Song.Create(releases[1].Id, genres[1].Id, "Guillotine", "about:blank", new List<Guid>{}),
            Song.Create(releases[1].Id, genres[1].Id, "Spread Eagle Cross The Block", "about:blank", new List<Guid>{}),
            Song.Create(releases[1].Id, genres[1].Id, "Lord of the Game", "about:blank", new List<Guid>{}),
        };

        modelBuilder.Entity<Genre>().HasData(genres);

        modelBuilder.Entity<Artist>().HasData(artists);

        modelBuilder.Entity<Release>().HasData(releases);

        modelBuilder.Entity<Song>().HasData(songs);

        modelBuilder.Entity<SongArtist>().HasData(new List<SongArtist> {new SongArtist(songs[0].Id, artists[0].Id) });
    }
}