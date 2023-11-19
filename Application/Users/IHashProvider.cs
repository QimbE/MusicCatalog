namespace Application.Users;

public interface IHashProvider
{
    string Hash(string password);

    bool Verify(string passwordHash, string inputPassword);
}