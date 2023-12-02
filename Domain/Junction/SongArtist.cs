using Domain.Artists;
using Domain.Songs;

namespace Domain.Junction;

public class SongArtist
{
    public Guid SongId { get; set; }
    
    public Song Song { get; private set; }
    
    public Guid ArtistId { get; set; }
    
    public Artist Artist { get; private set; }

    public SongArtist(Guid songId, Guid artistId)
    {
        SongId = songId;
        ArtistId = artistId;
    }
}