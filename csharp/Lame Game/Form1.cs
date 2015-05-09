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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CS2PHPCryptography;

namespace Lame_Game
{
    public partial class Form1 : Form
    {
        SecurePHPConnection secure;

        public Form1()
        {
            InitializeComponent();

            secure = new SecurePHPConnection();
            secure.OnConnectionEstablished += new SecurePHPConnection.ConnectionEstablishedHandler(secure_OnConnectionEstablished);
            secure.OnResponseReceived += new SecurePHPConnection.ResponseReceivedHandler(secure_OnResponseReceived);

            secure.SetRemotePhpScriptLocation("http://skot2/enc/score.php");
            secure.EstablishSecureConnectionAsync();

            button1.Enabled = false;
        }

        void secure_OnResponseReceived(object sender, ResponseReceivedEventArgs e)
        {
            MessageBox.Show(e.Response);
        }

        void secure_OnConnectionEstablished(object sender, OnConnectionEstablishedEventArgs e)
        {
            button1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            decimal score = (int)numericUpDown1.Value;
            string message = name + "," + score.ToString();

            if (secure.OKToSendMessage)
            {
                secure.SendMessageSecureAsync(message);
            }
        }
    }
}
