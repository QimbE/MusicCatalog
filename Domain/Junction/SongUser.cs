using Domain.Songs;
using Domain.Users;

namespace Domain.Junction;

public class SongUser
{
    public Guid SongId { get; set; }
    
    public Song Song { get; private set; }
    
    public Guid UserId { get; set; }
    
    public User User { get; private set; }

    public SongUser(Guid songId, Guid userId)
    {
        SongId = songId;
        UserId = userId;
    }
}