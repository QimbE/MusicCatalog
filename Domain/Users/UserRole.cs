using Domain.Primitives;

namespace Domain.Users;

// Fuck Enum Mapping in ef core, todo: intermediate entity mapping
public enum UserRole
{
    DefaultUser = 0,
    DataBaseAdmin = 1,
    Admin = 2
}