using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace EmployeeManagementApp.Helpers
{
    public class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 10000;

        public static string HashPassword(string password)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var salt = new byte[SaltSize];
                rng.GetBytes(salt);

                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
                var hash = pbkdf2.GetBytes(HashSize);

                var hashBytes = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

                var base64Hash = Convert.ToBase64String(hashBytes);

                return $"{Iterations}.{base64Hash}";
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var hashedPasswordParts = hashedPassword.Split('.', 2);
            if (hashedPasswordParts.Length != 2)
            {
                throw new ArgumentException("Invalid hashed password format.");
            }

            var iterations = int.Parse(hashedPasswordParts[0]);
            var hashBytes = Convert.FromBase64String(hashedPasswordParts[1]);

            if (iterations < 1 || iterations > 100000)
            {
                throw new ArgumentException("Invalid iteration count.");
            }

            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(HashSize);

            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
    

}
