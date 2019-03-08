using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using HtmlAgilityPack;
using System.Net;
namespace oteltakip
{
    public partial class MusteriMenu : Form
    {
        public MusteriMenu()
        {
            InitializeComponent();
        }
        string hamtc;
        string hamtc2;
        oteltakipDataContext ctx = new oteltakipDataContext();
        void griddoldur()
        {
            hamtc = label1.Text;
            dataGridView1.DataSource = ctx.musterilers.Where(musteri => musteri.tckimlikno.Contains(label1.Text));
            
        }
        void sorucevap()
        {
            dataGridView1.DataSource = ctx.sorusors;
        }
        

        
           
        
        private void MusteriMenu_Load(object sender, EventArgs e)
        {
            griddoldur();
            hamtc2 = hamtc;
            
            
            
            try
            {
                string ayir1 = dataGridView1.Rows[0].Cells[6].Value.ToString();
                ayir1 = ayir1.Substring(0, 2);
                string ayir2 = dataGridView1.Rows[0].Cells[5].Value.ToString();
                ayir2 = ayir2.Substring(0, 2);
                kalansure = Convert.ToInt16(ayir1) - Convert.ToInt16(ayir2);
            }
            catch (Exception)
            {
            }
            this.Text = "X Otel | " + dataGridView1.Rows[0].Cells[2].Value + " " + dataGridView1.Rows[0].Cells[3].Value;
            string kontrol = dataGridView1.Rows[0].Cells[1].Value.ToString();
            if (kontrol.Length == 11)
            {
                pictureBox1.ImageLocation = dataGridView1.Rows[0].Cells[12].Value.ToString();
            }
            string a = label1.Text;
            if (kalansure == 0)
            {
                label1.Text = "Sayın , " + dataGridView1.Rows[0].Cells[2].Value + " " + dataGridView1.Rows[0].Cells[3].Value + ". Hoşgeldin!!";
                MessageBox.Show("Bugün Çıkış gününüzdür.Aksi takdirde saat 00:00 da kaydınız silinecektir.");
            }
            else
            {
                label1.Text = "Sayın , " + dataGridView1.Rows[0].Cells[2].Value + " " + dataGridView1.Rows[0].Cells[3].Value + ". Hoşgeldin!!";
                MessageBox.Show("Çıkışınıza son " + kalansure + " gün kalmıştır.!");
            }
            tckimlikno = dataGridView1.Rows[0].Cells[2].Value.ToString();

            timer1.Interval = 1000;
            timer1.Start();
            
        }
        public string tckimlikno;
        public int kalansure;
        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Visible = !label1.Visible;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
          
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
    
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Musterisiparis sip = new Musterisiparis();
            sip.label6.Text= hamtc.ToString();
            sip.TopMost = true;
            sip.Show();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Gazete gzt = new Gazete();
            gzt.TopMost = true;
            gzt.Show();
        }

        private void MusteriMenu_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }
        
        private void MusteriMenu_FormClosing(object sender, FormClosingEventArgs e)
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
