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
    public partial class yoneticisiparispanel : Form
    {
        public yoneticisiparispanel()
        {
            InitializeComponent();
        }
        oteltakipDataContext ctx = new oteltakipDataContext();
        void griddoldur()
        {
            dataGridView1.DataSource = ctx.siparistablosus;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void yoneticisiparispanel_Load(object sender, EventArgs e)
        {
            griddoldur();
            label7.Visible = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        string index;
        int tutar;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string kelime = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string tamkelime = " ";
                char[] karakterler = kelime.ToCharArray();
                index = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                foreach (char karakter in karakterler)
                {
                    if (karakter == ',')
                    {
                        listBox1.Items.Add(tamkelime);
                        tamkelime = "";
                    }
                    else
                    {
                        tamkelime += karakter;
                    }
                }
                listBox1.Items.Add(tamkelime);
                textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                if (dataGridView1.CurrentRow.Cells[5].Value.ToString() == "")
                {
                    textBox4.Text = "";
                }
                else
                {
                    textBox4.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                }
                if (dataGridView1.CurrentRow.Cells[6].Value.ToString() == "")
                {
                    textBox5.Text = "";
                }
                else
                {
                    textBox5.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                }
                
                tutar = (int)dataGridView1.CurrentRow.Cells[3].Value;
            }
            catch (Exception)
            {
            }
        }
        DialogResult okey = new DialogResult();
        private void button1_Click(object sender, EventArgs e)
        {
            okey = MessageBox.Show("Siparişi gerçekleştirdiğinize emin misiniz ?", "Uyarı", MessageBoxButtons.YesNo);
            if (okey == DialogResult.Yes)
            {
                try
                {
                    siparistablosu st=ctx.siparistablosus.SingleOrDefault(stx => stx.sip_no==(int)dataGridView1.CurrentRow.Cells[0].Value);
                    st.onaylayanpersonel = label7.Text;
                    st.teslim_tarihi = DateTime.Now.ToString();
                    ctx.SubmitChanges();
                    griddoldur();
                    MessageBox.Show(index + " No'lu sipariş teslim edildi!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                printPreviewDialog1.Width = 1080;
                printPreviewDialog1.Height = 1920;
                printPreviewDialog1.Show();
            }
            catch (Exception)
            {
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                Ean14 Barcode = new Ean14();
                Barcode.CountryCode = "90";
                Barcode.ManufacturerCode = "1234";
                Barcode.ProductCode = "000001";
                Barcode.ChecksumDigit = "5";
                e.Graphics.DrawString("X Otel", new System.Drawing.Font(new FontFamily("Arial"), 15, FontStyle.Bold), Brushes.Red, new PointF(50, 5));
                e.Graphics.DrawString("Telefon : (xxx) xxx xx xx Fax : (xxx) xxx xx xx", new System.Drawing.Font(new FontFamily("Arial"), 8, FontStyle.Bold), Brushes.Red, new PointF(18, 45));
                e.Graphics.DrawString("Tarih - Saat : " + DateTime.Now.ToString(), new System.Drawing.Font(new FontFamily("Arial"), 8, FontStyle.Bold), Brushes.Red, new PointF(18, 65));
                e.Graphics.DrawString("Kasiyer : " + label7.Text, new System.Drawing.Font(new FontFamily("Arial"), 8, FontStyle.Bold), Brushes.Red, new PointF(18, 85));
                Barcode.DrawEan13Barcode(e.Graphics, (new PointF(25, 70)));
                string kelime = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string tamkelime = " ";
                char[] karakterler = kelime.ToCharArray();
                foreach (char karakter in karakterler)
                {
                    if (karakter == ',')
                    {

                        tamkelime += "\n";
                    }
                    else
                    {
                        tamkelime += karakter;
                    }
                }
                e.Graphics.DrawString("Ürünler \n-------------------------------------------------\n" + tamkelime + "\n-------------------------------------------------\nTutar:                      " + tutar + " TL", new System.Drawing.Font(new FontFamily("Arial"), 10, FontStyle.Bold), Brushes.Red, new PointF(20, 105));
            }
            catch (Exception)
            {
            }
        }
    }
}
