using System.Text.Json.Serialization;
using Domain.Primitives;

namespace Domain.Users;

public class User: Entity
{
    public string Username { get; private set; }
    
    public string Email { get; private set; }
    
    [JsonIgnore]
    public string Password { get; private set; }

    public UserRole Role { get; private set; }

    private User(string username, string email, string password, UserRole role = UserRole.DefaultUser)
        :base(Guid.NewGuid())
    {
        Username = username;
        Email = email;
        Password = password;
        Role = role;
    }

    public void Update(string username, string email, string password, UserRole role)
    {
        Username = username;
        Email = email;
        Password = password;
        Role = role;
    }

    public static User Create(string username, string email, string password, UserRole role = UserRole.DefaultUser)
    {
        return new User(username, email, password, role);
    }
}