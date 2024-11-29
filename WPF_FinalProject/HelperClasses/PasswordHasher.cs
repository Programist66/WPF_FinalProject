using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WPF_FinalProject.HelperClasses
{
    public class PasswordHasher
    {
        private static readonly int SaltSize = 16;
        private static readonly int HashSize = 20;

        public static string HashPassword(string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(password, SaltSize, 10000))
            {
                byte[] salt = algorithm.Salt;
                byte[] hash = algorithm.GetBytes(HashSize);

                return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            string[] parts = hashedPassword.Split(':');
            if (parts.Length != 2)
            {
                return false;
            }

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] hash = Convert.FromBase64String(parts[1]);

            using (var algorithm = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] computedHash = algorithm.GetBytes(HashSize);
                return ByteArraysEqual(hash, computedHash);
            }
        }

        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
