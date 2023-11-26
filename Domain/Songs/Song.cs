using Domain.Artists;
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
    
    public List<User> UsersWhoAdded { get; protected set; }
    
    protected Song()
        :base(Guid.NewGuid())
    {
        
    }

    protected Song(Guid releaseId, Guid genreId, string name, string audioLink)
        :base(Guid.NewGuid())
    {
        ReleaseId = releaseId;
        GenreId = genreId;
        Name = name;
        AudioLink = audioLink;
    }

    public void Update(Guid releaseId, Guid genreId, string name, string audioLink)
    {
        ReleaseId = releaseId;
        GenreId = genreId;
        Name = name;
        AudioLink = audioLink;
    }

    public static Song Create(Guid releaseId, Guid genreId, string name, string audioLink)
    {
        return new Song(releaseId, genreId, name, audioLink);
    }
}