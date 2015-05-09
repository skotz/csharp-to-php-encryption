using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace PHP_Encryption
{
    class SecurePHPConnection
    {
        private string address;
        private bool connected;
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
        public delegate void ConnectionEstablishedHandler(object sender, OnConnectionEstablishedArgs e);

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
        /// Create a secure connection with a PHP script.
        /// </summary>
        public SecurePHPConnection()
        {
            connected = false;

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
        }

        void background_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (connected)
            {
                if (OnConnectionEstablished != null)
                {
                    OnConnectionEstablished(this, new OnConnectionEstablishedArgs());
                }
            }
            else
            {
                throw new CryptographicException("There was an error communicating with the PHP script while establishing a secure connection.");
            }
        }

        void background_DoWork(object sender, DoWorkEventArgs e)
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
            connected = (result == "AES OK");
        }

        /// <summary>
        /// Start a secure connection in the background. The OnConnectionEstablished event will be raised upon sucessful connection.
        /// </summary>
        public void EstablishSecureConnectionAsync()
        {
            if (!background.IsBusy)
                background.RunWorkerAsync();
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
        /// Send an encrypted message to the remote PHP script and wait for a secure response by way of OnResponseReceived.
        /// </summary>
        /// <param name="message">The message to send.</param>
        public void SendMessageSecureAsync(string message)
        {
            if (connected)
            {
                sender.RunWorkerAsync(message);
            }
        }

        void sender_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (OnResponseReceived != null)
            {
                OnResponseReceived(this, new ResponseReceivedEventArgs(asyncResponse));
            }
        }

        void sender_DoWork(object sender, DoWorkEventArgs e)
        {
            asyncResponse = SendMessageSecure((string)e.Argument);
        }
    }

    class OnConnectionEstablishedArgs
    {
        public OnConnectionEstablishedArgs()
        {
        }
    }

    class ResponseReceivedEventArgs
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
