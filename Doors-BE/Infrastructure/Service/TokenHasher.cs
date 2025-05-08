using System.Security.Cryptography;
using System.Text;
using Domain.Interfaces.Services;

namespace Infrastructure.Service;

public class TokenHasher : ITokenHasher
{
    public string HashToken(string token)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(token);
            var hashBytes = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }

    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}