using System.Data;

namespace Domain.Songs.Exceptions;

public class SongIsAlreadyInFavouritesException : DuplicateNameException
{
    public SongIsAlreadyInFavouritesException(Guid songId)
        :base($"Song with Id {songId} is already in favourites.")
    {
        
    }
}