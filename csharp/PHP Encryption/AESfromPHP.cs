using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

/*
 * Scott Clayton
 * 
 */

namespace PHP_Encryption
{
    class AEStoPHPCryptography
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
        public string EncryptionKeyIV
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
                throw new Exception("The Key must be exactally 256 bits long!");
            if (IV.Length * 8 != 128)
                throw new Exception("The IV must be exactally 128 bits long!");
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

                if (aes != null)
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

                if (aes != null)
                    aes.Clear();

                srDecrypt.Close();
                csDecrypt.Close();
                msDecrypt.Close();

                return plaintext;
            }
            catch (Exception ex)
            {
                throw new CryptographicException("Problem trying to decrypt.", ex);
            }
        }
    }
}
