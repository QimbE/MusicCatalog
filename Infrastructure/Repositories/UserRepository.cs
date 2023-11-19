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

    public Task<User?> GetByEmailAsync(string email)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
    }

    public Task<bool> Any(Expression<Func<User, bool>> expression)
    {
        return _context.Users.AnyAsync(expression);
    }
}