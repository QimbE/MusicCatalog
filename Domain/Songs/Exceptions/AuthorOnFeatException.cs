using System.Data;

namespace Domain.Songs.Exceptions;

public class AuthorOnFeatException: InvalidConstraintException
{
    public AuthorOnFeatException():base("You are trying to add author of song as coauthor")
    {
        
    }


}