namespace PhpSafeKey
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ofdPrivateKey = new System.Windows.Forms.OpenFileDialog();
            this.sfdPrivateKey = new System.Windows.Forms.SaveFileDialog();
            this.timerPopup = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ofdPrivateKey
            // 
            this.ofdPrivateKey.FileName = "private.key";
            this.ofdPrivateKey.Filter = "Private Key Files|*.key";
            this.ofdPrivateKey.Title = "Select a private key to encode into a PHP file.";
            // 
            // sfdPrivateKey
            // 
            this.sfdPrivateKey.DefaultExt = "*.php";
            this.sfdPrivateKey.FileName = "private.php";
            this.sfdPrivateKey.Filter = "PHP Files|*.php";
            this.sfdPrivateKey.Title = "Save the PHP encoded private key.";
            // 
            // timerPopup
            // 
            this.timerPopup.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(10, 10);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "PHP Safe Key";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog ofdPrivateKey;
        private System.Windows.Forms.SaveFileDialog sfdPrivateKey;
        private System.Windows.Forms.Timer timerPopup;
    }
}

