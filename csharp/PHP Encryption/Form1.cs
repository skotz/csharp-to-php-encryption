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

using CS2PHPCryptography;
using System;
using System.Windows.Forms;

namespace PHP_Encryption
{
    public partial class frmMain : Form
    {
        private AsyncHttpControl http;
        private AsyncHttpControl http2;
        private RSAtoPHPCryptography rsa;

        private SecurePHPConnection secure;

        public frmMain()
        {
            InitializeComponent();

            secure = new SecurePHPConnection();
            secure.OnConnectionEstablished += new SecurePHPConnection.ConnectionEstablishedHandler(secure_OnConnectionEstablished);
            secure.OnResponseReceived += new SecurePHPConnection.ResponseReceivedHandler(secure_OnResponseReceived);

            // For the second test we are just going to test RSA by hard coding in the key on both the client and server sides
            rsa = new RSAtoPHPCryptography();
            rsa.LoadCertificateFromString("-----BEGIN CERTIFICATE-----MIID2jCCA0OgAwIBAgIJAPEru6Ch9es0MA0GCSqGSIb3DQEBBQUAMIGlMQswCQYDVQQGEwJVUzEQMA4GA1UECBMHRmxvcmlkYTESMBAGA1UEBxMJUGVuc2Fjb2xhMRswGQYDVQQKExJTY290dCBUZXN0IENvbXBhbnkxGTAXBgNVBAsTEFNlY3VyaXR5IFNlY3Rpb24xFjAUBgNVBAMTDVNjb3R0IENsYXl0b24xIDAeBgkqhkiG9w0BCQEWEXNzbEBzcGFya2hpdHouY29tMB4XDTExMDcwNDEzMDczM1oXDTIxMDcwMTEzMDczNFowgaUxCzAJBgNVBAYTAlVTMRAwDgYDVQQIEwdGbG9yaWRhMRIwEAYDVQQHEwlQZW5zYWNvbGExGzAZBgNVBAoTElNjb3R0IFRlc3QgQ29tcGFueTEZMBcGA1UECxMQU2VjdXJpdHkgU2VjdGlvbjEWMBQGA1UEAxMNU2NvdHQgQ2xheXRvbjEgMB4GCSqGSIb3DQEJARYRc3NsQHNwYXJraGl0ei5jb20wgZ8wDQYJKoZIhvcNAQEBBQADgY0AMIGJAoGBAKLEwtnhSD3sUMidycowAhupy59PMh8FYX6ebKy4NYqEiFONzrujkGtAZgmUaCAQBEmGcfBUDVd4ew72Xjikq0WhBUju+wmrIcgnQcIMAXMkZ2gBV12SkvCzRrJf5zqO0rC0x/tBli/46KGrzyYLl7K3QFx3MQPNvVO+w/b0coatAgMBAAGjggEOMIIBCjAdBgNVHQ4EFgQU+6E6OauoEUohJOAgC8OXU3xaHn4wgdoGA1UdIwSB0jCBz4AU+6E6OauoEUohJOAgC8OXU3xaHn6hgaukgagwgaUxCzAJBgNVBAYTAlVTMRAwDgYDVQQIEwdGbG9yaWRhMRIwEAYDVQQHEwlQZW5zYWNvbGExGzAZBgNVBAoTElNjb3R0IFRlc3QgQ29tcGFueTEZMBcGA1UECxMQU2VjdXJpdHkgU2VjdGlvbjEWMBQGA1UEAxMNU2NvdHQgQ2xheXRvbjEgMB4GCSqGSIb3DQEJARYRc3NsQHNwYXJraGl0ei5jb22CCQDxK7ugofXrNDAMBgNVHRMEBTADAQH/MA0GCSqGSIb3DQEBBQUAA4GBAJ8lRVFiLgfxiHsrPvhY+i05FYnDskit9QTnBv2ScM7rfK+EKfOswjxv9sGdGqKaTYE684XCmrwxCx42hNOSgMGDiZAlNoBJdJbF/bw2Qr5HUmZ8G3L3UlB1+qyM0+JkXMqkVcoIR7Ia5AGZHe9/QAwD3nA9rf3diH2LWATtgWNB-----END CERTIFICATE-----");

            http = new AsyncHttpControl();
            http.OnHttpResponse += new AsyncHttpControl.ResponseCallback(http_OnHttpResponse);

            http2 = new AsyncHttpControl();
            http2.OnHttpResponse += new AsyncHttpControl.ResponseCallback(http2_OnHttpResponse);

            // A simple AES test that decrypts a message with a base64 encoded 256 bit key and IV
            AEStoPHPCryptography aes = new AEStoPHPCryptography("YWJjZGVmZ2hpamtsbW5vcGFiY2RlZmdoaWprbG1ub3A=", "YWJjZGVmZ2hpamtsbW5vcA==");
            txtMessage2.Text = aes.Decrypt("34sFYu82zUy4Yrp22lmYN2rlLU1SmwTySx2QhjFGboc=");
        }

        private void http_OnHttpResponse(object sender, OnHttpResponseEventArgs e)
        {
            btnSend.Enabled = true;

            if (!e.Error)
            {
                txtResponse.Text = e.ResponseBody;
            }
            else
            {
                MessageBox.Show(e.ResponseBody);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            btnSend.Enabled = false;

            PostPackageBuilder postVars = new PostPackageBuilder();
            postVars.AddVariable("name", txtMessage.Text);

            if (!http.IsBusy)
            {
                http.Post(txtAddress.Text, postVars);
            }
        }

        private void btnSendTest2_Click(object sender, EventArgs e)
        {
            btnSendTest2.Enabled = false;

            PostPackageBuilder postVars = new PostPackageBuilder();
            string code = rsa.Encrypt(txtMessage2.Text);
            postVars.AddVariable("code", code);

            txtEncrypted2.Text = code;

            if (!http2.IsBusy)
            {
                http2.Post(txtAddress2.Text, postVars);
            }
        }

        private void http2_OnHttpResponse(object sender, OnHttpResponseEventArgs e)
        {
            btnSendTest2.Enabled = true;

            if (!e.Error)
            {
                txtResponse2.Text = e.ResponseBody;
            }
            else
            {
                MessageBox.Show(e.ResponseBody);
            }
        }

        private void btnSendTest3_Click(object sender, EventArgs e)
        {
            btnSendTest3.Enabled = false;

            if (secure.SecureConnectionEstablished)
            {
                secure.SendMessageSecureAsync(txtMessage3.Text);
            }
        }

        private void btnEstablish_Click(object sender, EventArgs e)
        {
            secure.SetRemotePhpScriptLocation(txtAddress3.Text);
            secure.EstablishSecureConnectionAsync();

            btnEstablishConnection.Enabled = false;
        }

        private void secure_OnResponseReceived(object sender, ResponseReceivedEventArgs e)
        {
            btnSendTest3.Enabled = true;
            txtResponse3.Text = e.Response;
        }

        private void secure_OnConnectionEstablished(object sender, OnConnectionEstablishedEventArgs e)
        {
            btnSendTest3.Enabled = true;
            txtResponse3.Text = "Sucessfully connected!";
        }
    }
}