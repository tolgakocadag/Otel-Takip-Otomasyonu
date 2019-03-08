using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace oteltakip
{
    public partial class AnaMenu : Form
    {
        public AnaMenu()
        {
            InitializeComponent();
        }
        oteltakipDataContext ctx = new oteltakipDataContext();
        void griddoldur()
        {
            musteriler ms = ctx.musterilers.SingleOrDefault(musteri => musteri.tckimlikno == label3.Text);

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MusteriKayit frm = new MusteriKayit();
            frm.label12.Text = admin.ToString();
            frm.Show();
            
            
        }
        public string admin;
        private void AnaMenu_Load(object sender, EventArgs e)
        {
            griddoldur();
            string a = label3.Text;
            admin = a;
            label3.Text = "Sayın " + a.ToUpper() + " . Hoşgeldin!!";
            timer1.Interval = 1000;
            timer1.Start();
            
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Visible = !label3.Visible;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            

        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            yoneticisiparispanel sip = new yoneticisiparispanel();
            sip.label7.Text = admin;
            
            sip.Show();
        }

        private void girişToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void boşOdaListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }
        
        
        private void Bos_oda_document_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void pansiyonListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void Pansiyonlistedoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void odaDurumlarıRaporuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void odadurumudoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            misafir msf = new misafir();
            msf.Show();
        }

        private void mToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void oteldekimisdoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
        }

        private void günlükÇıkışListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void misafirListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            rapor rp = new rapor();
            rp.Show();
        }

        private void odadurumudia_Load(object sender, EventArgs e)
        {

        }

        private void panslistdialog_Load(object sender, EventArgs e)
        {

        }

        private void oteldekimisafirlerdia_Load(object sender, EventArgs e)
        {

        }

            
        private void AnaMenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult yesno = new DialogResult();
            yesno = MessageBox.Show("Programdan çıkmak istiyor musunuz ?", "Uyarı", MessageBoxButtons.YesNo);
            if (yesno == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
