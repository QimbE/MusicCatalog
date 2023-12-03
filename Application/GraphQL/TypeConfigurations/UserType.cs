using Domain.Users;

namespace Application.GraphQL.TypeConfigurations;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        base.Configure(descriptor);

        descriptor.Authorize(Role.RoleNames.Admin);

        descriptor.Ignore(u => u.Password);
    }
}