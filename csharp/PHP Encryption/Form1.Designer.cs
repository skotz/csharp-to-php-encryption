namespace PHP_Encryption
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSend = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.txtResponse = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtAddress2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSendTest2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtEncrypted2 = new System.Windows.Forms.TextBox();
            this.txtMessage2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtResponse2 = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtAddress3 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnEstablishConnection = new System.Windows.Forms.Button();
            this.btnSendTest3 = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtMessage3 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtResponse3 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(371, 45);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(141, 48);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send Message";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(122, 19);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(390, 20);
            this.txtAddress.TabIndex = 1;
            this.txtAddress.Text = "http://localhost/test.php";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(122, 47);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(243, 20);
            this.txtMessage.TabIndex = 2;
            this.txtMessage.Text = "Scott Clayton";
            // 
            // txtResponse
            // 
            this.txtResponse.Location = new System.Drawing.Point(122, 73);
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.Size = new System.Drawing.Size(243, 20);
            this.txtResponse.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Your Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 76);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "The Response:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Address to POST to:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnSend);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtMessage);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtResponse);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(518, 100);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Unencrypted asynchronous POST test";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtAddress2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnSendTest2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtEncrypted2);
            this.groupBox2.Controls.Add(this.txtMessage2);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtResponse2);
            this.groupBox2.Location = new System.Drawing.Point(12, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(518, 140);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "RSA encrypted POST test";
            // 
            // txtAddress2
            // 
            this.txtAddress2.Location = new System.Drawing.Point(122, 19);
            this.txtAddress2.Name = "txtAddress2";
            this.txtAddress2.Size = new System.Drawing.Size(390, 20);
            this.txtAddress2.TabIndex = 1;
            this.txtAddress2.Text = "http://localhost/rsa.php";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Address to POST to:";
            // 
            // btnSendTest2
            // 
            this.btnSendTest2.Location = new System.Drawing.Point(371, 45);
            this.btnSendTest2.Name = "btnSendTest2";
            this.btnSendTest2.Size = new System.Drawing.Size(141, 48);
            this.btnSendTest2.TabIndex = 0;
            this.btnSendTest2.Text = "Send Message";
            this.btnSendTest2.UseVisualStyleBackColor = true;
            this.btnSendTest2.Click += new System.EventHandler(this.btnSendTest2_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 102);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "The Response:";
            // 
            // txtEncrypted2
            // 
            this.txtEncrypted2.Location = new System.Drawing.Point(122, 73);
            this.txtEncrypted2.Name = "txtEncrypted2";
            this.txtEncrypted2.Size = new System.Drawing.Size(243, 20);
            this.txtEncrypted2.TabIndex = 2;
            // 
            // txtMessage2
            // 
            this.txtMessage2.Location = new System.Drawing.Point(122, 47);
            this.txtMessage2.Name = "txtMessage2";
            this.txtMessage2.Size = new System.Drawing.Size(243, 20);
            this.txtMessage2.TabIndex = 2;
            this.txtMessage2.Text = "Scott Clayton";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Encrypted Message:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 50);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(75, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "The Message:";
            // 
            // txtResponse2
            // 
            this.txtResponse2.Location = new System.Drawing.Point(122, 99);
            this.txtResponse2.Name = "txtResponse2";
            this.txtResponse2.Size = new System.Drawing.Size(390, 20);
            this.txtResponse2.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtAddress3);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.btnEstablishConnection);
            this.groupBox3.Controls.Add(this.btnSendTest3);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.txtMessage3);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtResponse3);
            this.groupBox3.Location = new System.Drawing.Point(12, 264);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(518, 100);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Full Test - Get certificate, send AES key, send secure message, get secure respon" +
    "se";
            // 
            // txtAddress3
            // 
            this.txtAddress3.Location = new System.Drawing.Point(122, 19);
            this.txtAddress3.Name = "txtAddress3";
            this.txtAddress3.Size = new System.Drawing.Size(390, 20);
            this.txtAddress3.TabIndex = 1;
            this.txtAddress3.Text = "http://localhost/example.php";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(104, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Address to POST to:";
            // 
            // btnEstablishConnection
            // 
            this.btnEstablishConnection.Location = new System.Drawing.Point(371, 45);
            this.btnEstablishConnection.Name = "btnEstablishConnection";
            this.btnEstablishConnection.Size = new System.Drawing.Size(141, 23);
            this.btnEstablishConnection.TabIndex = 0;
            this.btnEstablishConnection.Text = "Establish Connection";
            this.btnEstablishConnection.UseVisualStyleBackColor = true;
            this.btnEstablishConnection.Click += new System.EventHandler(this.btnEstablish_Click);
            // 
            // btnSendTest3
            // 
            this.btnSendTest3.Enabled = false;
            this.btnSendTest3.Location = new System.Drawing.Point(371, 71);
            this.btnSendTest3.Name = "btnSendTest3";
            this.btnSendTest3.Size = new System.Drawing.Size(141, 23);
            this.btnSendTest3.TabIndex = 0;
            this.btnSendTest3.Text = "Send Message";
            this.btnSendTest3.UseVisualStyleBackColor = true;
            this.btnSendTest3.Click += new System.EventHandler(this.btnSendTest3_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 76);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "The Response:";
            // 
            // txtMessage3
            // 
            this.txtMessage3.Location = new System.Drawing.Point(122, 47);
            this.txtMessage3.Name = "txtMessage3";
            this.txtMessage3.Size = new System.Drawing.Size(243, 20);
            this.txtMessage3.TabIndex = 2;
            this.txtMessage3.Text = "Scott Clayton";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 50);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 13);
            this.label11.TabIndex = 4;
            this.label11.Text = "The Message:";
            // 
            // txtResponse3
            // 
            this.txtResponse3.Location = new System.Drawing.Point(122, 73);
            this.txtResponse3.Name = "txtResponse3";
            this.txtResponse3.Size = new System.Drawing.Size(243, 20);
            this.txtResponse3.TabIndex = 3;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 374);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMain";
            this.Text = "Encryption between C# and PHP";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.TextBox txtResponse;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtAddress2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSendTest2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtEncrypted2;
        private System.Windows.Forms.TextBox txtMessage2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtResponse2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtAddress3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnSendTest3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtMessage3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtResponse3;
        private System.Windows.Forms.Button btnEstablishConnection;
    }
}

