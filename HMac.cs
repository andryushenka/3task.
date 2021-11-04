using System;
using System.Text;
using System.Security.Cryptography;

namespace 3task
{
    static class HMAC
    {
        public static byte[] GenerateRandomKey(int size)
        {
            using (var generator = RandomNumberGenerator.Create())
            {
                var salt = new byte[size];
                generator.GetBytes(salt);
                return salt;
            }
        }

        public static byte[] HashHMAC(byte[] key, byte[] message)
        {
            var hash = new HMACSHA256(key);
            return hash.ComputeHash(message);
        }

        public static byte[] StringEncode(string text)
        {
            var encoding = new ASCIIEncoding();
            return encoding.GetBytes(text);
        }

        public static string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
