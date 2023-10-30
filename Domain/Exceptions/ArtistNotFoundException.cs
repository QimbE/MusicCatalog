using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class ArtistNotFoundException : NotFoundException
{
    private const string EntityName = "Artist";
    public ArtistNotFoundException(string propertyName)
        :base(EntityName, propertyName)
    {
        
    }
    
    public ArtistNotFoundException(string propertyName, Exception inner)
        :base(EntityName, propertyName, inner)
    {
        
    }
}