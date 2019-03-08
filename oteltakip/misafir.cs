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
    public partial class misafir : Form
    {
        public misafir()
        {
            InitializeComponent();
        }
        private void sifirla()
        {
            comboBox1.Text = comboBox1.Items[0].ToString();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }
        oteltakipDataContext ctx = new oteltakipDataContext();
        private void getir()
        {
            var data = from d in ctx.misafir1s
                       select new
                       {
                           Geldiği_Oda = d.geldigioda,
                           İsim = d.isim,
                           Soyisim = d.soyisim,
                           Ceptel = d.ceptel

                       };
            dataGridView2.DataSource = data;
        }
        private void misafir_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 11;
            getir();
            this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView2.ReadOnly = true;
            dataGridView1.DataSource = ctx.odadurumus.Where(xxd => xxd.odadurumu1.Contains("kirli dolu") | xxd.odadurumu1.Contains("temiz dolu"));
            try
            {
                int sayi = 0;
                while (true)
                {
                    
                    comboBox1.Items.Add("ODA " + dataGridView1.Rows[sayi].Cells[2].Value.ToString());
                    sayi++;


                }


            }
            catch (Exception)
            {
            }
            if (comboBox1.Items.Count > 1)
                comboBox1.Text = comboBox1.Items[0].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text.Substring(0, 3) == "ODA" & textBox2.Text.Length >= 3 & textBox3.Text.Length >= 3&textBox1.Text.Length==11)
                {
                    misafir1 msf = new misafir1();
                    msf.geldigioda = comboBox1.Text;
                    msf.isim = textBox2.Text;
                    msf.soyisim = textBox3.Text;
                    msf.ceptel = textBox1.Text;
                    ctx.misafir1s.InsertOnSubmit(msf);
                    ctx.SubmitChanges();
                    getir();
                    MessageBox.Show("Misafir girişi başarı ile kaydedildi.");
                    getir();
                    sifirla();
                }
                else
                {
                    MessageBox.Show("İsim ve soyisim 3 haneden uzun olmalı , cep telefonu 11 haneli olmalıdır ve odayı seçtiğinizden emin olunuz!");
                }
            }
            catch (Exception)
            {
            }
            getir();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        
        {
            int a = comboBox1.SelectedIndex;
            dataGridView1.DataSource = ctx.musterilers.Where(xxd => xxd.odano.Contains(comboBox1.Items[a].ToString()));
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                misafir1 misss = ctx.misafir1s.SingleOrDefault(musssss => musssss.ceptel == textBox1.Text);
                ctx.misafir1s.DeleteOnSubmit(misss);
                ctx.SubmitChanges();
                MessageBox.Show("Müşteri Çıkışı Sağlandı!");
                sifirla();
                getir();
                
            }
            catch (Exception)
            {
                MessageBox.Show("Müşteri Kaydı zaten yok");
            }
            getir();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                comboBox1.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
                textBox2.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                textBox1.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            }
            catch (Exception)
            {
            }
        }
    }
}

