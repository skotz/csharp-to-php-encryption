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

using System.ComponentModel;
using System.Security.Cryptography;

namespace CS2PHPCryptography
{
    public class SecurePHPConnection
    {
        private string address;
        private bool connected;
        private bool inUse;
        private string asyncResponse;
        private BackgroundWorker background;
        private BackgroundWorker sender;
        private HttpControl http;
        private RSAtoPHPCryptography rsa;
        private AEStoPHPCryptography aes;

        /// <summary>
        /// Event raised when a secure connection has been established with the remote PHP script.
        /// </summary>
        public event ConnectionEstablishedHandler OnConnectionEstablished;

        public delegate void ConnectionEstablishedHandler(object sender, OnConnectionEstablishedEventArgs e);

        /// <summary>
        /// Event raised when an encrypted transmission is received as a response to something you sent.
        /// </summary>
        public event ResponseReceivedHandler OnResponseReceived;

        public delegate void ResponseReceivedHandler(object sender, ResponseReceivedEventArgs e);

        /// <summary>
        /// Gets the location of the PHP script that we are communicating with.
        /// </summary>
        public string PHPScriptLocation
        {
            get { return address; }
        }

        /// <summary>
        /// Gets whether a secure connection has been established.
        /// </summary>
        public bool SecureConnectionEstablished
        {
            get { return connected; }
        }

        /// <summary>
        /// Gets whether it is OK to send a message right now. A connection must be established, and there must be no other pending transmissions.
        /// </summary>
        public bool OKToSendMessage
        {
            get { return connected && !inUse; }
        }

        /// <summary>
        /// Create a secure connection with a PHP script.
        /// </summary>
        public SecurePHPConnection()
        {
            connected = false;
            inUse = false;

            http = new HttpControl();
            rsa = new RSAtoPHPCryptography();
            aes = new AEStoPHPCryptography();

            background = new BackgroundWorker();
            background.DoWork += new DoWorkEventHandler(background_DoWork);
            background.RunWorkerCompleted += new RunWorkerCompletedEventHandler(background_RunWorkerCompleted);

            sender = new BackgroundWorker();
            sender.DoWork += new DoWorkEventHandler(sender_DoWork);
            sender.RunWorkerCompleted += new RunWorkerCompletedEventHandler(sender_RunWorkerCompleted);
        }

        /// <summary>
        /// Set the location of the PHP script to use in this secure connection.
        /// </summary>
        /// <param name="phpScriptLocation">The URL of the php script to contact.</param>
        public void SetRemotePhpScriptLocation(string phpScriptLocation)
        {
            address = phpScriptLocation;
            connected = false;
            inUse = false;
        }

        private void background_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (connected)
            {
                if (OnConnectionEstablished != null)
                {
                    OnConnectionEstablished(this, new OnConnectionEstablishedEventArgs());
                }
            }
            else
            {
                throw new CryptographicException("There was an error communicating with the PHP script while establishing a secure connection.");
            }
        }

        private void background_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the RSA public key that we will use
            string cert = http.Post(address, "getkey=y");
            rsa.LoadCertificateFromString(cert);

            // Generate the AES keys, encrypt them with the RSA public key, then send them to the PHP script.
            aes.GenerateRandomKeys();
            string key = Utility.ToUrlSafeBase64(rsa.Encrypt(aes.EncryptionKey));
            string iv = Utility.ToUrlSafeBase64(rsa.Encrypt(aes.EncryptionIV));
            string result = http.Post(address, "key=" + key + "&iv=" + iv);

            // If the PHP script sends this message back, then the connection is now good.
            connected = (aes.Decrypt(result) == "AES OK");
        }

        /// <summary>
        /// Start a secure connection in the background. The OnConnectionEstablished event will be raised upon sucessful connection.
        /// </summary>
        public void EstablishSecureConnectionAsync()
        {
            if (!background.IsBusy)
            {
                background.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Send an encrypted message to the remote PHP script and wait for a secure response.
        /// </summary>
        /// <param name="message">The message to send.</param>
        public string SendMessageSecure(string message)
        {
            if (connected)
            {
                string encrypted = aes.Encrypt(message);
                string response = http.Post(address, "data=" + encrypted);
                return aes.Decrypt(response);
            }
            else
            {
                return "NOT CONNECTED";
            }
        }

        /// <summary>
        /// Securely close the connection. This will also tell the PHP server to destroy the session keys it has.
        /// </summary>
        public void CloseConnection()
        {
            SendMessageSecureAsync("CLOSE CONNECTION");
            connected = false;
        }

        /// <summary>
        /// Send an encrypted message to the remote PHP script and wait for a secure response by way of OnResponseReceived.
        /// </summary>
        /// <param name="message">The message to send.</param>
        public void SendMessageSecureAsync(string message)
        {
            if (connected && !inUse)
            {
                sender.RunWorkerAsync(message);
            }
        }

        private void sender_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            inUse = false;

            if (OnResponseReceived != null)
            {
                OnResponseReceived(this, new ResponseReceivedEventArgs(asyncResponse));
            }
        }

        private void sender_DoWork(object sender, DoWorkEventArgs e)
        {
            inUse = true;
            asyncResponse = SendMessageSecure((string)e.Argument);
        }
    }

    public class OnConnectionEstablishedEventArgs
    {
        public OnConnectionEstablishedEventArgs()
        {
        }
    }

    public class ResponseReceivedEventArgs
    {
        /// <summary>
        /// The decrypted response from the remote PHP script.
        /// </summary>
        public string Response { get; set; }

        public ResponseReceivedEventArgs(string response)
        {
            Response = response;
        }
    }
}