using System.Data;

namespace Domain.Songs.Exceptions;

public class GenreWithTheSameNameException: DuplicateNameException
{
    public GenreWithTheSameNameException(string name)
        :base($"Genre with the name {name} already exists.")
    {
        
    }
}