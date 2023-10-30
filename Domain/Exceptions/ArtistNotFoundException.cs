namespace Domain.Exceptions;

public sealed class ArtistNotFoundException : Exception
{
    public ArtistNotFoundException(Guid id)
        :base($"The product with the Id = {id} was not found.")
    {
        
    }
}