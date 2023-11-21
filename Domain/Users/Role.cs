using System.Collections.Immutable;
using System.Reflection;
using Ardalis.SmartEnum;
using Domain.Primitives;

namespace Domain.Users;

public class Role: SmartEnum<Role>
{
    public static readonly Role Default = new Role(1, nameof(Default));
    public static readonly Role DatabaseAdmin = new Role(2, nameof(DatabaseAdmin));
    public static readonly Role Admin = new Role(3, nameof(Admin));
    
    public List<User> Users { get; private set; }

    private Role(int value, string name)
        : base(name, value)
    {
        
    }

}