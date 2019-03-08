namespace oteltakip
{
    partial class Gazete
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.milliyetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sabahToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.milliyetToolStripMenuItem,
            this.sabahToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1902, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // milliyetToolStripMenuItem
            // 
            this.milliyetToolStripMenuItem.Name = "milliyetToolStripMenuItem";
            this.milliyetToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.milliyetToolStripMenuItem.Text = "Milliyet";
            this.milliyetToolStripMenuItem.Click += new System.EventHandler(this.milliyetToolStripMenuItem_Click);
            // 
            // sabahToolStripMenuItem
            // 
            this.sabahToolStripMenuItem.Name = "sabahToolStripMenuItem";
            this.sabahToolStripMenuItem.Size = new System.Drawing.Size(62, 24);
            this.sabahToolStripMenuItem.Text = "Sabah";
            this.sabahToolStripMenuItem.Click += new System.EventHandler(this.sabahToolStripMenuItem_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 28);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(1902, 1005);
            this.webBrowser1.TabIndex = 1;
            this.webBrowser1.Url = new System.Uri("http://www.milliyet.com.tr", System.UriKind.Absolute);
            // 
            // Gazete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Gazete";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "X Otel | Gazeteler";
            this.Load += new System.EventHandler(this.Gazete_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem milliyetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sabahToolStripMenuItem;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}