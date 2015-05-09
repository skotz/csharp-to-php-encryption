//
// Copyright (c) 2011 Scott Clayton
//
// This file is part of the C# to PHP Encryption Library.
//
// The C# to PHP Encryption Library is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// The C# to PHP Encryption Library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with the C# to PHP Encryption Library.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.IO;
using System.Security.Cryptography;

namespace CS2PHPCryptography
{
    public class AEStoPHPCryptography
    {
        private byte[] Key;
        private byte[] IV;

        /// <summary>
        /// Gets the encryption key as a base64 encoded string.
        /// </summary>
        public string EncryptionKeyString
        {
            get { return Convert.ToBase64String(Key); }
        }

        /// <summary>
        /// Gets the initialization key as a base64 encoded string.
        /// </summary>
        public string EncryptionIVString
        {
            get { return Convert.ToBase64String(IV); }
        }

        /// <summary>
        /// Gets the encryption key.
        /// </summary>
        public byte[] EncryptionKey
        {
            get { return Key; }
        }

        /// <summary>
        /// Gets the initialization key.
        /// </summary>
        public byte[] EncryptionIV
        {
            get { return IV; }
        }

        public AEStoPHPCryptography()
        {
            Key = new byte[256 / 8];
            IV = new byte[128 / 8];

            GenerateRandomKeys();
        }

        public AEStoPHPCryptography(string key, string iv)
        {
            Key = Convert.FromBase64String(key);
            IV = Convert.FromBase64String(iv);

            if (Key.Length * 8 != 256)
            {
                throw new Exception("The Key must be exactally 256 bits long!");
            }
            if (IV.Length * 8 != 128)
            {
                throw new Exception("The IV must be exactally 128 bits long!");
            }
        }

        /// <summary>
        /// Generate the cryptographically secure random 256 bit Key and 128 bit IV for the AES algorithm.
        /// </summary>
        public void GenerateRandomKeys()
        {
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            random.GetBytes(Key);
            random.GetBytes(IV);
        }

        /// <summary>
        /// Encrypt a message and get the encrypted message in a URL safe form of base64.
        /// </summary>
        /// <param name="plainText">The message to encrypt.</param>
        public string Encrypt(string plainText)
        {
            return Utility.ToUrlSafeBase64(Encrypt2(plainText));
        }

        /// <summary>
        /// Encrypt a message using AES.
        /// </summary>
        /// <param name="plainText">The message to encrypt.</param>
        private byte[] Encrypt2(string plainText)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                aes.KeySize = 256;
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                MemoryStream msEncrypt = new MemoryStream();
                CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                StreamWriter swEncrypt = new StreamWriter(csEncrypt);

                swEncrypt.Write(plainText);

                swEncrypt.Close();
                csEncrypt.Close();
                aes.Clear();

                return msEncrypt.ToArray();
            }
            catch (Exception ex)
            {
                throw new CryptographicException("Problem trying to encrypt.", ex);
            }
        }

        /// <summary>
        /// Decrypt a message that is in a url safe base64 encoded string.
        /// </summary>
        /// <param name="cipherText">The string to decrypt.</param>
        public string Decrypt(string cipherText)
        {
            return Decrypt2(Utility.FromUrlSafeBase64(cipherText));
        }

        /// <summary>
        /// Decrypt a message that was AES encrypted.
        /// </summary>
        /// <param name="cipherText">The string to decrypt.</param>
        private string Decrypt2(byte[] cipherText)
        {
            try
            {
                RijndaelManaged aes = new RijndaelManaged();
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;
                aes.KeySize = 256;
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                MemoryStream msDecrypt = new MemoryStream(cipherText);
                CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                StreamReader srDecrypt = new StreamReader(csDecrypt);

                string plaintext = srDecrypt.ReadToEnd();

                srDecrypt.Close();
                csDecrypt.Close();
                msDecrypt.Close();
                aes.Clear();

                return plaintext;
            }
            catch (Exception ex)
            {
                throw new CryptographicException("Problem trying to decrypt.", ex);
            }
        }
    }
}