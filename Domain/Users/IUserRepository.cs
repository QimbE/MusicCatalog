using System.Linq.Expressions;

namespace Domain.Users;

//todo: cancelletion tokens
public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    void Add(User user);
    Task<bool> Any(Expression<Func<User, bool>> expression);
}