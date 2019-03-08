using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace oteltakip
{
    public partial class Gazete : Form
    {
        public Gazete()
        {
            InitializeComponent();
        }

        private void Gazete_Load(object sender, EventArgs e)
        {
        }

        private void milliyetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate("http://www.milliyet.com.tr");
        }

        private void hürriyetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void sabahToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.webBrowser1.Navigate("https://www.sabah.com.tr");
        }

        private void haberTürkToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
