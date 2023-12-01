using System.Text.Json.Serialization;
using Domain.Primitives;
using Domain.Songs;

namespace Domain.Users;

public class User: Entity
{
    public string Username { get; private set; }
    
    public string Email { get; private set; }
    
    [JsonIgnore]
    public string Password { get; private set; }

    public Role Role { get; private set; }
    public int RoleId { get; private set; }
    
    
    public List<Song> FavouriteSongs { get; protected set; }

    private User()
    {
        
    }

    private User(string username, string email, string password, int roleId)
        :base(Guid.NewGuid())
    {
        Username = username;
        Email = email;
        Password = password;
        RoleId = roleId;
    }

    public void Update(string username, string email, string password, int roleId)
    {
        Username = username;
        Email = email;
        Password = password;
        RoleId = roleId;
    }

    public static User Create(string username, string email, string password, int roleId)
    {
        return new User(username, email, password, roleId);
    }
}