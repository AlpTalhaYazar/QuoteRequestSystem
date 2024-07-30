using System.Security.Cryptography;
using System.Text;

namespace QuoteRequestSystem.Application.Helper;

public class PasswordHasher
{
    public static async Task<(string hash, string salt)> CreatePasswordHash(string password)
    {
        using var hmac = new HMACSHA512();
        var salt = hmac.Key;
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
    }

    public static async Task<bool> VerifyPasswordHash(string password, string storedHash, string storedSalt)
    {
        using var hmac = new HMACSHA512(Convert.FromBase64String(storedSalt));
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return computedHash.SequenceEqual(Convert.FromBase64String(storedHash));
    }
}