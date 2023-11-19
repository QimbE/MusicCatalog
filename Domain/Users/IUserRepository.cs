namespace Domain.Users;

//todo: cancelletion tokens
public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    
}