using System.Linq.Expressions;

namespace Domain.Users;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email, string? includeProperties = null, CancellationToken cancellationToken = default);

    void Add(User user);
    Task<bool> Any(Expression<Func<User, bool>> expression, CancellationToken cancellationToken = default);
}