using System.Data;

namespace Domain.Exceptions;

public class ArtistWithTheSameNameException : DuplicateNameException
{
    public ArtistWithTheSameNameException(string name)
        :base($"Artist with the name {name} already exists.")
    {
        
    }
}