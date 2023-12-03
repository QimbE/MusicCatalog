using System.Data;

namespace Domain.Songs.Exceptions;

public class SongIsNotInFavouritesException: DuplicateNameException
{
    public SongIsNotInFavouritesException(Guid songId)
        :base($"Song with Id {songId} is not in favourites.")
    {
        
    }
}