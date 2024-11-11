using System.Security.Cryptography;
using System.Text;
namespace Jivar.Service.Util
{
    public class PasswordUtil
    {
        public static string HashPassword(string rawPassword)
        {
            // Generate a random salt
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Combine password and salt
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(rawPassword);
                byte[] passwordWithSaltBytes = new byte[passwordBytes.Length + salt.Length];
                Buffer.BlockCopy(passwordBytes, 0, passwordWithSaltBytes, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, passwordWithSaltBytes, passwordBytes.Length, salt.Length);

                // Compute hash
                byte[] hashBytes = sha256.ComputeHash(passwordWithSaltBytes);

                // Combine salt and hash for storage
                byte[] hashWithSaltBytes = new byte[salt.Length + hashBytes.Length];
                Buffer.BlockCopy(salt, 0, hashWithSaltBytes, 0, salt.Length);
                Buffer.BlockCopy(hashBytes, 0, hashWithSaltBytes, salt.Length, hashBytes.Length);

                // Convert to base64 for easier storage
                return Convert.ToBase64String(hashWithSaltBytes);
            }
        }

        // Method to verify a raw password against a hashed password with a salt
        public static bool VerifyPassword(string rawPassword, string storedHashedPassword)
        {
            byte[] hashWithSaltBytes = Convert.FromBase64String(storedHashedPassword);

            // Extract salt from the stored hash
            byte[] salt = new byte[16];
            Buffer.BlockCopy(hashWithSaltBytes, 0, salt, 0, salt.Length);

            // Hash the input password with the extracted salt
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(rawPassword);
                byte[] passwordWithSaltBytes = new byte[passwordBytes.Length + salt.Length];
                Buffer.BlockCopy(passwordBytes, 0, passwordWithSaltBytes, 0, passwordBytes.Length);
                Buffer.BlockCopy(salt, 0, passwordWithSaltBytes, passwordBytes.Length, salt.Length);

                byte[] hashBytes = sha256.ComputeHash(passwordWithSaltBytes);

                // Compare the computed hash with the stored hash
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    if (hashWithSaltBytes[i + salt.Length] != hashBytes[i])
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
