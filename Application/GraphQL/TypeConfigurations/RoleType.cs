using Domain.Users;

namespace Application.GraphQL.TypeConfigurations;

public class RoleType : EnumType<Role>
{
    protected override void Configure(IEnumTypeDescriptor<Role> descriptor)
    {
        descriptor.Value(Role.Default).Name(Role.Default.Name.ToUpperInvariant());
        
        descriptor.Value(Role.DatabaseAdmin).Name(Role.DatabaseAdmin.Name.ToUpperInvariant());
        
        descriptor.Value(Role.Admin).Name(Role.Admin.Name.ToUpperInvariant());
    }
}