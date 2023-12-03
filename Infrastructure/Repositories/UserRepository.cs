using System.Linq.Expressions;
using Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository: IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<User?> GetByEmailAsync(string email, string? includeProperties = null, CancellationToken cancellationToken = default)
    {
        IQueryable<User> set = _context.Users;

        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var includeProp in
                     includeProperties.Split(
                         new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
            {
                set = set.Include(includeProp);
            }
        }
        
        return set.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public Task<User?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return _context.Users.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
    }

    public Task<bool> Any(Expression<Func<User, bool>> expression, CancellationToken cancellationToken = default)
    {
        return _context.Users.AnyAsync(expression, cancellationToken);
    }
}