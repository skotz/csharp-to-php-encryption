using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace PHP_Encryption
{
    static class Utility
    {
        public static string ToUrlSafeBase64(byte[] input)
        {
            return Convert.ToBase64String(input).Replace("+", "-").Replace("/", "_");
        }

        public static byte[] FromUrlSafeBase64(string input)
        {
            return Convert.FromBase64String(input.Replace("-", "+").Replace("_", "/"));
        }

        public static byte[] GetRandomSafeString(int length)
        {
            byte[] bytes = new byte[length];
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            random.GetBytes(bytes);

            char[] validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray(); // -=[]\\;',./`~!@#$%^&*()_+{}|:\"<>?

            for (int i = 0; i < length; i++)
            {
                bytes[i] = (byte)((int)validChars[(int)bytes[i] % validChars.Length]);
            }

            return bytes;
        }
    }
}
