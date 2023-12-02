using Domain.Exceptions;

namespace Domain.Songs.Exceptions;

public class GenreNotFoundException: NotFoundException
{
    private const string EntityName = "Genre";
    public GenreNotFoundException(string propertyName)
        :base(EntityName, propertyName)
    {
        
    }
    
    public GenreNotFoundException(string propertyName, Exception inner)
        :base(EntityName, propertyName, inner)
    {
        
    }
}