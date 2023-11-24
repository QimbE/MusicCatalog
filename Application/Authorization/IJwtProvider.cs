using Domain.Users;

namespace Application.Authorization;

public interface IJwtProvider
{
    string Generate(User user);
}