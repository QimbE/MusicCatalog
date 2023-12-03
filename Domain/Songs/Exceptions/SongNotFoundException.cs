using Domain.Exceptions;

namespace Domain.Songs.Exceptions;

public class SongNotFoundException : NotFoundException
{
    private const string EntityName = "Song";
    public SongNotFoundException(string propertyName)
        :base(EntityName, propertyName)
    {
        
    }
    
    public SongNotFoundException(string propertyName, Exception inner)
        :base(EntityName, propertyName, inner)
    {
        
    }
}