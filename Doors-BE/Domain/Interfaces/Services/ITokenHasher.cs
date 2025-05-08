namespace Domain.Interfaces.Services;

public interface ITokenHasher
{
    string HashToken(string token);
}