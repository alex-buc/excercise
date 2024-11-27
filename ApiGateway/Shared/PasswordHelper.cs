using System.Security.Cryptography;
using System.Text;

namespace ApiGateway.Shared;

public static class PasswordHelper {
    private static readonly byte[] _staticSalt = Encoding.UTF8.GetBytes("SomeFixedSaltValue123");
    public static string GeneratePasswordWithStaticSalt(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            // Combine the password and the static salt
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] combinedBytes = new byte[passwordBytes.Length + _staticSalt.Length];

            Buffer.BlockCopy(passwordBytes, 0, combinedBytes, 0, passwordBytes.Length);
            Buffer.BlockCopy(_staticSalt, 0, combinedBytes, passwordBytes.Length, _staticSalt.Length);

            // Hash the combined password and static salt
            byte[] hashBytes = sha256.ComputeHash(combinedBytes);

            // Return the hash as a Base64 string
            return Convert.ToBase64String(hashBytes);
        }
    }
}