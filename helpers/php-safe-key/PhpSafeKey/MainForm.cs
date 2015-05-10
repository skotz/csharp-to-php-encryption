using System;
using System.IO;
using System.Windows.Forms;

namespace PhpSafeKey
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            timerPopup.Start();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            timerPopup.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timerPopup.Stop();

            DialogResult d = ofdPrivateKey.ShowDialog();
            if (d == DialogResult.OK)
            {
                DialogResult s = sfdPrivateKey.ShowDialog();
                if (s == DialogResult.OK)
                {
                    File.WriteAllText(sfdPrivateKey.FileName, "<?php $PrivateRSAKey = \"" + File.ReadAllText(ofdPrivateKey.FileName) + "\"; ?>");
                    MessageBox.Show("The private key has been saved into a PHP file.");
                }
            }

            this.Close();
        }
    }
}