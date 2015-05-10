using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PhpSafeKey
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            DialogResult d = openFileDialog1.ShowDialog();
            if (d == DialogResult.OK)
            {
                DialogResult s = saveFileDialog1.ShowDialog();
                if (s == DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog1.FileName, "<?php $PrivateRSAKey = \"" + File.ReadAllText(openFileDialog1.FileName) + "\"; ?>");
                    MessageBox.Show("The private key has been saved into a PHP file!");
                }
            }

            this.Close();
        }
    }
}
