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
    public partial class Musterisiparis : Form
    {
        public Musterisiparis()
        {
            InitializeComponent();
        }
        int siparistutar=0; string siparistutarst;
        oteltakipDataContext ctx = new oteltakipDataContext();
        void griddoldur2()
        {
            dataGridView2.DataSource=ctx.siparistablosus.SingleOrDefault(mustcc => mustcc.mustc == label6.Text);
            this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
        }
        void griddoldur()
        {
            
            dataGridView1.DataSource = ctx.siparistablosus;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void Musterisiparis_Load(object sender, EventArgs e)
        {
            label6.Visible = false;
            timer1.Interval = 750;
            timer1.Start();
        }
        int icecekindex, yiyecekindex, tatliindex;
        private void tutarihesapla()
        {
            siparistutarst = siparistutarst.Substring(siparistutarst.Length - 5);
            siparistutarst = siparistutarst.Substring(0, 2);
            siparistutarst = siparistutarst.Trim();
            siparistutar += Convert.ToInt16(siparistutarst);
        }
        private void İcecekler_SelectedIndexChanged(object sender, EventArgs e)
        {
            icecekindex = İcecekler.SelectedIndex;
            pictureBox1.Image = icecekresimleri.Images[icecekindex];
            siparistutarst = İcecekler.Items[icecekindex].ToString();
            tutarihesapla();
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label4.Visible = !label4.Visible;
        }

        private void siparisliste_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tatlılar_SelectedIndexChanged(object sender, EventArgs e)
        {
            tatliindex = tatlılar.SelectedIndex;
            pictureBox1.Image = tatliresimleri.Images[tatliindex];
            siparistutarst = tatlılar.Items[tatliindex].ToString();
            tutarihesapla();
        }

        private void Yiyecekler_SelectedIndexChanged(object sender, EventArgs e)
        {
            yiyecekindex = Yiyecekler.SelectedIndex;
            pictureBox1.Image = yiyecekresimleri.Images[yiyecekindex];
            siparistutarst = Yiyecekler.Items[yiyecekindex].ToString();
            tutarihesapla();
        }

        private void İcecekler_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            siparisliste.Items.Add(İcecekler.Items[icecekindex]);
            textBox1.Text = siparistutar + " TL";
        }

        private void tatlılar_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            siparisliste.Items.Add(tatlılar.Items[tatliindex]);
            textBox1.Text = siparistutar + " TL";
        }

        private void Yiyecekler_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            siparisliste.Items.Add(Yiyecekler.Items[yiyecekindex]);
            textBox1.Text = siparistutar + " TL";
        }

        private void siparisliste_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                siparistutarst = siparisliste.Items[siparisliste.SelectedIndex].ToString();
                siparistutarst = siparistutarst.Substring(siparistutarst.Length - 5);
                siparistutarst = siparistutarst.Substring(0, 2);
                siparistutarst = siparistutarst.Trim();
                siparistutar -= Convert.ToInt16(siparistutarst);
                siparisliste.Items.RemoveAt(siparisliste.SelectedIndex);
                textBox1.Text = siparistutar + " TL";
            }
            catch (Exception)
            { 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                griddoldur();
               
                string liste = "";
                for (int i = 0; i < siparisliste.Items.Count; i++)
                {
                    if (i < siparisliste.Items.Count - 1)
                    {
                        liste += siparisliste.Items[i] + " , ";
                    }
                    else
                    {
                        liste += siparisliste.Items[i];
                    }
                }
                siparistablosu spt = new siparistablosu();
                spt.mustc = label6.Text;
                spt.siparisliste = liste;
                spt.siparis_tutar = siparistutar;
                spt.siparis_tarihi = System.DateTime.Now.ToString();
                ctx.siparistablosus.InsertOnSubmit(spt);
                ctx.SubmitChanges();
                MessageBox.Show("Sipariş onaylandı! En kısa sürede personellerimiz tarafından ulaştırılacaktır!");
                liste = "";
                siparisliste.Items.Clear();
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen sepeti boş bırakmayınız");
            }
        }

        private void Musterisiparis_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
    
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
             
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            MusteriSiparisDurumu msd = new MusteriSiparisDurumu();
            msd.label1.Text = label6.Text;
            
            msd.Show();
        }
    }
}
