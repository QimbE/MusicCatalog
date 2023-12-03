using Domain.Exceptions;

namespace Domain.Users.Exceptions;

public class UserNotFoundException: NotFoundException
{
    private const string EntityName = "User";
    public UserNotFoundException(string propertyName)
        :base(EntityName, propertyName)
    {
        
    }
    
    public UserNotFoundException(string propertyName, Exception inner)
        :base(EntityName, propertyName, inner)
    {
        
    }
}