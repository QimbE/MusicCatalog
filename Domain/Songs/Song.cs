using Domain.Artists;
using Domain.Junction;
using Domain.Primitives;
using Domain.Releases;
using Domain.Users;

namespace Domain.Songs;

public class Song: Entity
{
    public Guid ReleaseId { get; protected set; }
    
    public Release Release { get; protected set; }
    
    
    public Guid GenreId { get; protected set; }
    
    public Genre Genre { get; protected set; }
    
    
    public string Name { get; protected set; }
    
    public string AudioLink { get; protected set; }
    
    
    public List<Artist> ArtistsOnFeat { get; protected set; }
    
    public List<SongArtist> SongArtists { get; set; }
    
    public List<User> UsersWhoAdded { get; protected set; }
    
    protected Song()
        :base(Guid.NewGuid())
    {
        
    }

    protected Song(Guid releaseId, Guid genreId, string name, string audioLink, List<Guid> artistsOnFeatIds)
        :base(Guid.NewGuid())
    {
        ReleaseId = releaseId;
        GenreId = genreId;
        Name = name;
        AudioLink = audioLink;
        SongArtists = IdsToEntity(artistsOnFeatIds);
    }

    public void Update(Guid releaseId, Guid genreId, string name, string audioLink, List<Guid> artistsOnFeatIds)
    {
        ReleaseId = releaseId;
        GenreId = genreId;
        Name = name;
        AudioLink = audioLink;
        SongArtists = IdsToEntity(artistsOnFeatIds);
    }

    public static Song Create(Guid releaseId, Guid genreId, string name, string audioLink, List<Guid> artistsOnFeatIds)
    {
        return new Song(releaseId, genreId, name, audioLink, artistsOnFeatIds);
    }

    private List<SongArtist> IdsToEntity(List<Guid> artistsOnFeatIds)
    {
        if (artistsOnFeatIds is null)
        {
            throw new ArgumentNullException();
        }

        var songArtists = new List<SongArtist>();
        
        foreach (var artistId in artistsOnFeatIds)
        {
            songArtists.Add(new SongArtist(this.Id, artistId));
        }

        return songArtists;
    }
}