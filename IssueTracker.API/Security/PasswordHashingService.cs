using System;
using System.Linq;
using System.Security.Cryptography;
using ServiceStack.Configuration;

namespace IssueTracker.API.Security
{
    public class PasswordHashingService
    {
        private static readonly int PasswordSaltSize = new AppSettings().Get("PasswordSaltSize", 16);
        private static readonly int PasswordKeySize = new AppSettings().Get("PasswordKeySize", 32);
        private static readonly int PasswordIterations = new AppSettings().Get("PasswordIterations", 1000);

        public static string HashPassword(string password)
        {
            var deriveBytes = new Rfc2898DeriveBytes(password, PasswordSaltSize, PasswordIterations);
            byte[] salt = deriveBytes.Salt;

            byte[] subkeyBytes = deriveBytes.GetBytes(PasswordKeySize);
            var outputBytes = new byte[1 + PasswordSaltSize + PasswordKeySize];

            Buffer.BlockCopy(salt, 0, outputBytes, 1, PasswordSaltSize);
            Buffer.BlockCopy(subkeyBytes, 0, outputBytes, 1 + PasswordSaltSize, PasswordKeySize);

            return Convert.ToBase64String(outputBytes);
        }

        public static bool CheckPassword(string hashedPassword, string password)
        {
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

            // Wrong length or version header.
            if (hashedPasswordBytes.Length != (1 + PasswordSaltSize + PasswordKeySize) || hashedPasswordBytes[0] != 0x00)
                return false;

            var salt = new byte[PasswordSaltSize];
            var storedSubkey = new byte[PasswordKeySize];

            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, PasswordSaltSize);
            Buffer.BlockCopy(hashedPasswordBytes, 1 + PasswordSaltSize, storedSubkey, 0, PasswordKeySize);

            byte[] generatedSubkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, PasswordIterations))
            {
                generatedSubkey = deriveBytes.GetBytes(PasswordKeySize);
            }
            return storedSubkey.SequenceEqual(generatedSubkey);
        }

    }
}