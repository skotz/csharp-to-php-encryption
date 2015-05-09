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
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CS2PHPCryptography
{
    public class RSAtoPHPCryptography
    {
        private X509Certificate2 cert;
        private bool initialized;

        /// <summary>
        /// Create a new PHP compatible RSA encryptor from a certificate.
        /// </summary>
        /// <param name="certificateLocation">The file to load as a certificate.</param>
        public RSAtoPHPCryptography(string certificateLocation)
        {
            LoadCertificateFromFile(certificateLocation);
        }

        /// <summary>
        /// Create a new PHP compatible RSA encryptor. Make sure you load a certificate before trying to encrypt.
        /// </summary>
        public RSAtoPHPCryptography()
        {
            initialized = false;
        }

        /// <summary>
        /// Create a new PHP compatible RSA encryptor from a certificate file.
        /// </summary>
        /// <param name="certificateLocation">The file to load as a certificate.</param>
        public void LoadCertificateFromFile(string certificateLocation)
        {
            try
            {
                cert = GetCertificateFromFile(certificateLocation);
                initialized = true;
            }
            catch (Exception ex)
            {
                initialized = false;
                throw new CryptographicException("There was an error reading the certificate.", ex);
            }

            // You should keep the private key on the server and only have the public key on the client side.
            if (cert.HasPrivateKey)
                throw new CryptographicException("Use a certificate that does not contain a private key for security purposes.");
        }

        /// <summary>
        /// Create a new PHP compatible RSA encryptor from a certificate string.
        /// </summary>
        /// <param name="certificateText">The base64 encoded text to load as a certificate.</param>
        public void LoadCertificateFromString(string certificateText)
        {
            try
            {
                cert = GetCertificate(certificateText);
                initialized = true;
            }
            catch (Exception ex)
            {
                initialized = false;
                throw new CryptographicException("There was an error reading the certificate.", ex);
            }

            // You should keep the private key on the server and only have the public key on the client side.
            if (cert.HasPrivateKey)
            {
                throw new CryptographicException("Use a certificate that does not contain a private key for security purposes.");
            }
        }

        /// <summary>
        /// Load a public RSA key from a certificate string.
        /// </summary>
        /// <param name="key">The certificate text.</param>
        /// <exception cref="FormatException"></exception>
        private X509Certificate2 GetCertificate(string key)
        {
            try
            {
                if (key.Contains("-----"))
                {
                    // Get just the base64 encoded part of the file then trim off the beginning and ending -----BLAH----- tags
                    key = key.Split(new string[] { "-----" }, StringSplitOptions.RemoveEmptyEntries)[1];
                }

                // Remove "new line" characters
                key.Replace("\n", "");

                // Convert the key to a certificate for encryption
                return new X509Certificate2(Convert.FromBase64String(key));
            }
            catch (Exception ex)
            {
                throw new FormatException("The certificate key was not in the expected format.", ex);
            }
        }

        /// <summary>
        /// Load a public RSA key from a certificate file.
        /// </summary>
        /// <param name="file">The certificate file.</param>
        /// <returns></returns>
        private X509Certificate2 GetCertificateFromFile(string file)
        {
            return GetCertificate(File.ReadAllText(file));
        }

        /// <summary>
        /// Encrypt a messages using the supplied public certificate.
        /// </summary>
        /// <param name="message">The message to encrypt.</param>
        public byte[] Encrypt(byte[] message)
        {
            if (initialized)
            {
                RSACryptoServiceProvider publicprovider = (RSACryptoServiceProvider)cert.PublicKey.Key;
                return publicprovider.Encrypt(message, false);
            }
            else
            {
                throw new Exception("The RSA engine has not been initialized with a certificate yet.");
            }
        }

        /// <summary>
        /// Encrypt a messages using the supplied public certificate and returns the ciphertext as a base64 encoded string.
        /// </summary>
        /// <param name="message">The message to encrypt.</param>
        public string Encrypt(string message)
        {
            if (initialized)
            {
                // Encrypt and convert to a form of base64 with two url-unfriendly characters replaced.
                return Utility.ToUrlSafeBase64(Encrypt(ASCIIEncoding.ASCII.GetBytes(message)));
            }
            else
            {
                throw new Exception("The RSA engine has not been initialized with a certificate yet.");
            }
        }
    }
}