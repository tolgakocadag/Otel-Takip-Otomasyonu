using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
namespace oteltakip
{
    public partial class rapor : Form
    {
        public rapor()
        {
            InitializeComponent();
        }
        private void temizle()
        {
            TextBox1.Clear();
            TextBox2.Clear();
            TextBox3.Clear();
            TextBox4.Clear();
        }
        
        private void rapor_Load(object sender, EventArgs e)
        {groupBox1.Visible = false;
            pansiyonListesiToolStripMenuItem_Click(sender, e);
            
            dataGridView1.ReadOnly = true;
            this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            

        }
        oteltakipDataContext ctx = new oteltakipDataContext();
        private void boşOdaListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            try
            {
                temizle();
                label7.Text = "BOŞ ODA LİSTESİ";
                tiklananmenu = "bosodaliste";
                groupBox1.Visible = true;
                TextBox1.Visible = true;
                TextBox2.Visible = true;
                TextBox3.Visible = false;
                TextBox4.Visible = false;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = false;
                label4.Visible = false;
                label1.Text = "Oda No : ";
                label2.Text = "Oda Durumu : ";
                var data = from d in ctx.odadurumus.Where(xxd => (xxd.odadurumu1.Contains("temiz boş")))
                           select new
                           {
                               Oda_No = d.odanoo,
                               Oda_Durumu=d.odadurumu1,
                               Oda_Türü=d.turu

                           };
                dataGridView1.DataSource = data;
                
                
                
            }
            catch (Exception)
            { }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Excel.Application excel = new Excel.Application();
                excel.Visible = true;
                object Missing = Type.Missing;
                Workbook workbook = excel.Workbooks.Add(Missing);
                Worksheet sheet1 = (Worksheet)workbook.Sheets[1];
                int StartCol = 1;
                int StartRow = 1;
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    Range myRange = (Range)sheet1.Cells[StartRow, StartCol + j];
                    myRange.Value2 = dataGridView1.Columns[j].HeaderText;
                    myRange.EntireColumn.AutoFit();
                    myRange.EntireRow.AutoFit();

                }
                StartRow++;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {

                        Range myRange = (Range)sheet1.Cells[StartRow + i, StartCol + j];
                        myRange.Value2 = dataGridView1[j, i].Value == null ? "" : dataGridView1[j, i].Value;
                        myRange.Select();
                        myRange.EntireColumn.AutoFit();
                    myRange.EntireRow.AutoFit();

                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Excel lisansınızın bitmediğinden emin olunuz! Aksi takdirde Excel kaynaklı bir sorun yaşayabilirsiniz!");
            }
        }
        public string aranan="";
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (tiklananmenu == "gmusteriler")
            {
                
                var data = from d in ctx.gmusterilers.Where(xxd => xxd.tckimlikno.Contains(TextBox1.Text))
                           select new
                           {
                               TcKimlikNo = d.tckimlikno,
                               Adı_Soyadı = d.adi + " " + d.soyadi,
                               Cinsiyeti = d.cinsiyeti,
                               Kaldığı_Oda = d.kaldigioda,
                               Cep_Telefonu = d.ceptelefonu,
                               Giriş_Tarihi = d.giristarihi,
                               Çıkış_Tarihi = d.cikistarihi
                           };
                dataGridView1.DataSource = data;
                if (TextBox1.TextLength < 1)
                {
                    data = from d in ctx.gmusterilers
                               select new
                               {
                                   TcKimlikNo = d.tckimlikno,
                                   Adı_Soyadı = d.adi + " " + d.soyadi,
                                   Cinsiyeti = d.cinsiyeti,
                                   Kaldığı_Oda = d.kaldigioda,
                                   Cep_Telefonu = d.ceptelefonu,
                                   Giriş_Tarihi = d.giristarihi,
                                   Çıkış_Tarihi = d.cikistarihi
                               };
                    dataGridView1.DataSource = data;
                }
            }
            else if (tiklananmenu == "bosodaliste")
            {
                
                var data = from d in ctx.odadurumus.Where(xxd => xxd.odanoo.Contains(TextBox1.Text.ToUpper()))
                           select new
                           {
                               Oda_No = d.odanoo,
                               Oda_Durumu = d.odadurumu1,
                               Oda_Türü = d.turu

                           };
                dataGridView1.DataSource = data;
                if (TextBox1.TextLength < 1)
                {
                    data = from d in ctx.odadurumus.Where(xxd => (xxd.odadurumu1.Contains("temiz boş")))
                               select new
                               {
                                   Oda_No = d.odanoo,
                                   Oda_Durumu = d.odadurumu1,
                                   Oda_Türü = d.turu

                               };
                    dataGridView1.DataSource = data;
                }
            }
            else if (tiklananmenu == "musteriler")
            {
                var data = from d in ctx.musterilers.Where(xxd => xxd.tckimlikno.Contains(TextBox1.Text))
                           select new
                           {
                               TcKimlikNo = d.tckimlikno,
                               Adı_Soyadı = d.adi + " " + d.soyadi,
                               Cinsiyeti = d.cinsiyet,
                               Kaldığı_Oda = d.odano,
                               Cep_Telefonu = d.ceptel,
                               Giriş_Tarihi = d.giristarihi,
                               Çıkış_Tarihi = d.cikistarihi
                           };
                dataGridView1.DataSource = data;
                if (TextBox1.TextLength < 1)
                {
                     data = from d in ctx.musterilers
                               select new
                               {
                                   TcKimlikNo = d.tckimlikno,
                                   Adı_Soyadı = d.adi + " " + d.soyadi,
                                   Cinsiyeti = d.cinsiyet,
                                   Kaldığı_Oda = d.odano,
                                   Cep_Telefonu = d.ceptel,
                                   Giriş_Tarihi = d.giristarihi,
                                   Çıkış_Tarihi = d.cikistarihi
                               };
                    dataGridView1.DataSource = data;
                }
            }
            else if (tiklananmenu == "misafir")
            {
                
                var data = from d in ctx.misafir1s.Where(xxd => xxd.geldigioda.Contains(TextBox1.Text))
                           select new
                           {
                               Adı_Soyadı = d.isim + " " + d.soyisim,
                               Kaldığı_Oda = d.geldigioda,
                               Cep_Telefonu = d.ceptel,
                           };
                dataGridView1.DataSource = data;
                if (TextBox1.TextLength < 1)
                {
                     data = from d in ctx.misafir1s
                               select new
                               {
                                   Adı_Soyadı = d.isim + " " + d.soyisim,
                                   Kaldığı_Oda = d.geldigioda,
                                   Cep_Telefonu = d.ceptel,
                               };
                    dataGridView1.DataSource = data;
                }
            }
            else if (tiklananmenu == "odaliste")
            {
                
                var data = from d in ctx.odadurumus.Where(xxd => xxd.odanoo.Contains(TextBox1.Text))
                           select new
                           {
                               Oda_No = d.odanoo,
                               Oda_Durumu = d.odadurumu1,
                               Oda_Türü = d.turu
                           };
                dataGridView1.DataSource = data;
                if (TextBox1.TextLength < 1)
                {
                    data = from d in ctx.odadurumus
                               select new
                               {
                                   Oda_No = d.odanoo,
                                   Oda_Durumu = d.odadurumu1,
                                   Oda_Türü = d.turu
                               };
                    dataGridView1.DataSource = data;
                }
            }
            else if (tiklananmenu == "temizlikliste")
            {
                
                var data = from d in ctx.temizliks.Where(xxd => xxd.odano.Contains(TextBox1.Text))
                           select new
                           {
                               Oda_No = d.odano,
                               Temizlik_Tarihi = d.t_tarihi

                           };
                dataGridView1.DataSource = data;
                if (TextBox1.TextLength < 1)
                {
                     data = from d in ctx.temizliks
                               select new
                               {
                                   Oda_No = d.odano,
                                   Temizlik_Tarihi = d.t_tarihi

                               };
                    dataGridView1.DataSource = data;
                }
            }
        }

        private void misafirListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            try
            {
                label7.Text = "MİSAFİR LİSTESİ";
                groupBox1.Visible = true;
                tiklananmenu = "misafir";
                temizle();
                TextBox1.Visible = true;
                TextBox2.Visible = true;
                TextBox3.Visible = true;
                TextBox4.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label1.Text = "Geldiği Oda : ";
                label2.Text = "Adı : ";
                label3.Text = "Soyadı : ";
                label4.Text = "Cep Telefonu : ";
                var data = from d in ctx.misafir1s
                           select new
                           {
                               Adı_Soyadı = d.isim + " " + d.soyisim,
                               Kaldığı_Oda = d.geldigioda,
                               Cep_Telefonu = d.ceptel,
                           };
                dataGridView1.DataSource = data;
                
            }
            catch (Exception)
            {
            }
        }

        private void odanotext_TextChanged(object sender, EventArgs e)
        {
            if (tiklananmenu == "gmusteriler")
            {
                
                var data = from d in ctx.gmusterilers.Where(xxd => xxd.kaldigioda.Contains(TextBox4.Text))
                           select new
                           {
                               TcKimlikNo = d.tckimlikno,
                               Adı_Soyadı = d.adi + " " + d.soyadi,
                               Cinsiyeti = d.cinsiyeti,
                               Kaldığı_Oda = d.kaldigioda,
                               Cep_Telefonu = d.ceptelefonu,
                               Giriş_Tarihi = d.giristarihi,
                               Çıkış_Tarihi = d.cikistarihi
                           };
                dataGridView1.DataSource = data;
                if (TextBox4.TextLength < 1)
                {
                     data = from d in ctx.gmusterilers
                               select new
                               {
                                   TcKimlikNo = d.tckimlikno,
                                   Adı_Soyadı = d.adi + " " + d.soyadi,
                                   Cinsiyeti = d.cinsiyeti,
                                   Kaldığı_Oda = d.kaldigioda,
                                   Cep_Telefonu = d.ceptelefonu,
                                   Giriş_Tarihi = d.giristarihi,
                                   Çıkış_Tarihi = d.cikistarihi
                               };
                    dataGridView1.DataSource = data;
                }
            }
            else if (tiklananmenu == "musteriler")
            {
                
                var data = from d in ctx.musterilers.Where(xxd => xxd.odano.Contains(TextBox4.Text))
                           select new
                           {
                               TcKimlikNo = d.tckimlikno,
                               Adı_Soyadı = d.adi + " " + d.soyadi,
                               Cinsiyeti = d.cinsiyet,
                               Kaldığı_Oda = d.odano,
                               Cep_Telefonu = d.ceptel,
                               Giriş_Tarihi = d.giristarihi,
                               Çıkış_Tarihi = d.cikistarihi
                           };
                dataGridView1.DataSource = data;
                if (TextBox4.TextLength < 1)
                {
                    data = from d in ctx.musterilers
                               select new
                               {
                                   TcKimlikNo = d.tckimlikno,
                                   Adı_Soyadı = d.adi + " " + d.soyadi,
                                   Cinsiyeti = d.cinsiyet,
                                   Kaldığı_Oda = d.odano,
                                   Cep_Telefonu = d.ceptel,
                                   Giriş_Tarihi = d.giristarihi,
                                   Çıkış_Tarihi = d.cikistarihi
                               };
                    dataGridView1.DataSource = data;
                }
            }
            else if (tiklananmenu == "misafir")
            {
                dataGridView1.DataSource = ctx.misafir1s.Where(xxd => xxd.geldigioda.Contains(TextBox4.Text));
                var data = from d in ctx.misafir1s.Where(xxd => xxd.geldigioda.Contains(TextBox4.Text))
                           select new
                           {
                               Adı_Soyadı = d.isim + " " + d.soyisim,
                               Kaldığı_Oda = d.geldigioda,
                               Cep_Telefonu = d.ceptel,
                           };
                dataGridView1.DataSource = data;
                if (TextBox4.TextLength < 1)
                {
                    data = from d in ctx.misafir1s
                               select new
                               {
                                   Adı_Soyadı = d.isim + " " + d.soyisim,
                                   Kaldığı_Oda = d.geldigioda,
                                   Cep_Telefonu = d.ceptel,
                               };
                    dataGridView1.DataSource = data;
                }
            }
        }

        private void odanotext_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); 
        }

        private void tckimliktext_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); 
        }

        private void aditext_TextChanged(object sender, EventArgs e)
        {
            if (tiklananmenu == "gmusteriler")
            {
                
                var data = from d in ctx.gmusterilers.Where(xxd => xxd.adi.Contains(TextBox2.Text))
                           select new
                           {
                               TcKimlikNo = d.tckimlikno,
                               Adı_Soyadı = d.adi + " " + d.soyadi,
                               Cinsiyeti = d.cinsiyeti,
                               Kaldığı_Oda = d.kaldigioda,
                               Cep_Telefonu = d.ceptelefonu,
                               Giriş_Tarihi = d.giristarihi,
                               Çıkış_Tarihi = d.cikistarihi
                           };
                dataGridView1.DataSource = data;
                if (TextBox2.TextLength < 1)
                {
                     data = from d in ctx.gmusterilers
                               select new
                               {
                                   TcKimlikNo = d.tckimlikno,
                                   Adı_Soyadı = d.adi + " " + d.soyadi,
                                   Cinsiyeti = d.cinsiyeti,
                                   Kaldığı_Oda = d.kaldigioda,
                                   Cep_Telefonu = d.ceptelefonu,
                                   Giriş_Tarihi = d.giristarihi,
                                   Çıkış_Tarihi = d.cikistarihi
                               };
                    dataGridView1.DataSource = data;
                }
            }
            else if (tiklananmenu == "odaliste")
            {
               
                var data = from d in ctx.odadurumus.Where(xxd => xxd.odadurumu1.Contains(TextBox2.Text))
                           select new
                           {
                               Oda_No = d.odanoo,
                               Oda_Durumu = d.odadurumu1,
                               Oda_Türü = d.turu
                           };
                dataGridView1.DataSource = data;
                if (TextBox2.TextLength < 1)
                {
                    data = from d in ctx.odadurumus
                               select new
                               {
                                   Oda_No = d.odanoo,
                                   Oda_Durumu = d.odadurumu1,
                                   Oda_Türü = d.turu
                               };
                    dataGridView1.DataSource = data;
                }
            }
            else if (tiklananmenu == "bosodaliste")
            {
                
               var data = from d in ctx.odadurumus.Where(xxd => xxd.odadurumu1.Contains(TextBox2.Text))
                       select new
                       {
                           Oda_No = d.odanoo,
                           Oda_Durumu = d.odadurumu1,
                           Oda_Türü = d.turu

                       };
                dataGridView1.DataSource = data;
                if (TextBox2.TextLength < 1)
                {
                    data = from d in ctx.odadurumus.Where(xxd => (xxd.odadurumu1.Contains("temiz boş")))
                           select new
                           {
                               Oda_No = d.odanoo,
                               Oda_Durumu = d.odadurumu1,
                               Oda_Türü = d.turu

                           };
                    dataGridView1.DataSource = data;
                }
            }
            else if (tiklananmenu == "musteriler")
            {
                dataGridView1.DataSource = ctx.musterilers.Where(xxd => xxd.adi.Contains(TextBox2.Text));
                var data = from d in ctx.musterilers.Where(xxd => xxd.adi.Contains(TextBox2.Text))
                           select new
                           {
                               TcKimlikNo = d.tckimlikno,
                               Adı_Soyadı = d.adi + " " + d.soyadi,
                               Cinsiyeti = d.cinsiyet,
                               Kaldığı_Oda = d.odano,
                               Cep_Telefonu = d.ceptel,
                               Giriş_Tarihi = d.giristarihi,
                               Çıkış_Tarihi = d.cikistarihi
                           };
                dataGridView1.DataSource = data;
                if (TextBox2.TextLength < 1)
                {
                     data = from d in ctx.musterilers
                               select new
                               {
                                   TcKimlikNo = d.tckimlikno,
                                   Adı_Soyadı = d.adi + " " + d.soyadi,
                                   Cinsiyeti = d.cinsiyet,
                                   Kaldığı_Oda = d.odano,
                                   Cep_Telefonu = d.ceptel,
                                   Giriş_Tarihi = d.giristarihi,
                                   Çıkış_Tarihi = d.cikistarihi
                               };
                    dataGridView1.DataSource = data;
                }
            }
            else if (tiklananmenu == "misafir")
            {
                
                var data = from d in ctx.misafir1s.Where(xxd => xxd.isim.Contains(TextBox2.Text))
                           select new
                           {
                               Adı_Soyadı = d.isim + " " + d.soyisim,
                               Kaldığı_Oda = d.geldigioda,
                               Cep_Telefonu = d.ceptel,
                           };
                dataGridView1.DataSource = data;
                if (TextBox2.TextLength < 1)
                {
                    data = from d in ctx.misafir1s
                               select new
                               {
                                   Adı_Soyadı = d.isim + " " + d.soyisim,
                                   Kaldığı_Oda = d.geldigioda,
                                   Cep_Telefonu = d.ceptel,
                               };
                    dataGridView1.DataSource = data;
                }
            }
        }

        private void aditext_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void odadurumutxt_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void odaDurumlarıRaporuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            try
            {
                temizle();
                label7.Text = "ODA DURUMLARI RAPORU";
                tiklananmenu = "odaliste";
                groupBox1.Visible = true;
                TextBox1.Visible = true;
                TextBox2.Visible = true;
                TextBox3.Visible = false;
                TextBox4.Visible = false;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = false;
                label4.Visible = false;
                label1.Text = "Oda No : ";
                label2.Text = "Oda Durumu : ";
                var data = from d in ctx.odadurumus
                           select new
                           {
                               Oda_No = d.odanoo,
                               Oda_Durumu = d.odadurumu1,
                               Oda_Türü=d.turu
                           };
                dataGridView1.DataSource = data;
                
            }
            catch (Exception)
            { }
        }
        
        private void mToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void pansiyonListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                label7.Text = "MÜŞTERİ LİSTESİ";
                groupBox1.Visible = true;
                tiklananmenu = "musteriler";
                temizle();
                TextBox1.Visible = true;
                TextBox2.Visible = true;
                TextBox3.Visible = true;
                TextBox4.Visible = true;
                label1.Visible = true;
                label2.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label1.Text = "Tc Kimlik No : ";
                label2.Text = "Adı : ";
                label3.Text = "Soyadı : ";
                label4.Text = "Kaldığı Oda : ";
                var data = from d in ctx.musterilers
                           select new
                                {
                                    TcKimlikNo=d.tckimlikno,
                                    Adı_Soyadı = d.adi + " " + d.soyadi,
                                    Cinsiyeti=d.cinsiyet,
                                    Kaldığı_Oda=d.odano,
                                    Cep_Telefonu=d.ceptel,
                                    Giriş_Tarihi=d.giristarihi,
                                    Çıkış_Tarihi=d.cikistarihi
                                };
                dataGridView1.DataSource = data;


            }
            catch (Exception)
            {
            }
            
        }

        private void soyaditext_TextChanged(object sender, EventArgs e)
        {
            if (tiklananmenu == "gmusteriler")
            {
                
                var data = from d in ctx.gmusterilers.Where(xxd => xxd.soyadi.Contains(TextBox3.Text))
                           select new
                           {
                               TcKimlikNo = d.tckimlikno,
                               Adı_Soyadı = d.adi + " " + d.soyadi,
                               Cinsiyeti = d.cinsiyeti,
                               Kaldığı_Oda = d.kaldigioda,
                               Cep_Telefonu = d.ceptelefonu,
                               Giriş_Tarihi = d.giristarihi,
                               Çıkış_Tarihi = d.cikistarihi
                           };
                dataGridView1.DataSource = data;
                if (TextBox3.TextLength < 1)
                {
                     data = from d in ctx.gmusterilers
                               select new
                               {
                                   TcKimlikNo = d.tckimlikno,
                                   Adı_Soyadı = d.adi + " " + d.soyadi,
                                   Cinsiyeti = d.cinsiyeti,
                                   Kaldığı_Oda = d.kaldigioda,
                                   Cep_Telefonu = d.ceptelefonu,
                                   Giriş_Tarihi = d.giristarihi,
                                   Çıkış_Tarihi = d.cikistarihi
                               };
                    dataGridView1.DataSource = data;
                }
            }
            else if (tiklananmenu == "musteriler")
            {
                
                var data = from d in ctx.musterilers.Where(xxd => xxd.soyadi.Contains(TextBox3.Text))
                           select new
                           {
                               TcKimlikNo = d.tckimlikno,
                               Adı_Soyadı = d.adi + " " + d.soyadi,
                               Cinsiyeti = d.cinsiyet,
                               Kaldığı_Oda = d.odano,
                               Cep_Telefonu = d.ceptel,
                               Giriş_Tarihi = d.giristarihi,
                               Çıkış_Tarihi = d.cikistarihi
                           };
                dataGridView1.DataSource = data;
                if (TextBox3.TextLength < 1)
                {
                     data = from d in ctx.musterilers
                               select new
                               {
                                   TcKimlikNo = d.tckimlikno,
                                   Adı_Soyadı = d.adi + " " + d.soyadi,
                                   Cinsiyeti = d.cinsiyet,
                                   Kaldığı_Oda = d.odano,
                                   Cep_Telefonu = d.ceptel,
                                   Giriş_Tarihi = d.giristarihi,
                                   Çıkış_Tarihi = d.cikistarihi
                               };
                    dataGridView1.DataSource = data;
                }
            }
            else if (tiklananmenu == "misafir")
            {
                dataGridView1.DataSource = ctx.misafir1s.Where(xxd => xxd.soyisim.Contains(TextBox3.Text));
                var data = from d in ctx.misafir1s.Where(xxd => xxd.soyisim.Contains(TextBox3.Text))
                           select new
                           {
                               Adı_Soyadı = d.isim + " " + d.soyisim,
                               Kaldığı_Oda = d.geldigioda,
                               Cep_Telefonu = d.ceptel,
                           };
                dataGridView1.DataSource = data;
                if (TextBox3.TextLength < 1)
                {
                     data = from d in ctx.misafir1s
                               select new
                               {
                                   Adı_Soyadı = d.isim + " " + d.soyisim,
                                   Kaldığı_Oda = d.geldigioda,
                                   Cep_Telefonu = d.ceptel,
                               };
                    dataGridView1.DataSource = data;
                }
            }
        }
        public string tiklananmenu;
        private void çıkışYapanMüşteriListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            tiklananmenu = "gmusteriler";
            label7.Text = "ÇIKIŞ YAPAN MÜŞTERİ KAYITLARI";
            temizle();
            TextBox1.Visible = true;
            TextBox2.Visible = true;
            TextBox3.Visible = true;
            TextBox4.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label1.Text = "Tc Kimlik No : ";
            label2.Text = "Adı : ";
            label3.Text="Soyadı : ";
            label4.Text="Kaldığı Oda : ";
            var data = from d in ctx.gmusterilers
                       select new
                       {
                           TcKimlikNo = d.tckimlikno,
                           Adı_Soyadı = d.adi + " " + d.soyadi,
                           Cinsiyeti = d.cinsiyeti,
                           Kaldığı_Oda = d.kaldigioda,
                           Cep_Telefonu = d.ceptelefonu,
                           Giriş_Tarihi = d.giristarihi,
                           Çıkış_Tarihi = d.cikistarihi
                       };
            dataGridView1.DataSource = data;
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ctx.gmusterilers.Where(xxd => xxd.tckimlikno.Contains(TextBox1.Text));
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ctx.gmusterilers.Where(xxd => xxd.adi.Contains(TextBox2.Text));
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ctx.gmusterilers.Where(xxd => xxd.soyadi.Contains(TextBox3.Text));
        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ctx.gmusterilers.Where(xxd => xxd.kaldigioda.Contains(TextBox4.Text));
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void günlükÇıkışListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                label7.Text = "GÜNLÜK ÇIKIŞ LİSTESİ";
                string tarih = DateTime.Now.ToString("dd MMMM yyyy dddd").Substring(0, 1).Trim();

                if (Convert.ToInt16(tarih) == 0)
                {

                    var data = from d in ctx.gmusterilers.Where(d => d.cikistarihi.Trim().Contains(DateTime.Now.ToString("dd MMMM yyyy dddd").Substring(1).Trim()))
                               select new
                               {
                                   TcKimlikNo = d.tckimlikno,
                                   Adı_Soyadı = d.adi + " " + d.soyadi,
                                   Cinsiyeti = d.cinsiyeti,
                                   Kaldığı_Oda = d.kaldigioda,
                                   Cep_Telefonu = d.ceptelefonu,
                                   Giriş_Tarihi = d.giristarihi,
                                   Çıkış_Tarihi = d.cikistarihi
                               };
                    dataGridView1.DataSource = data;
                }
                else
                {
                    
                    var data = from d in ctx.gmusterilers.Where(d => d.cikistarihi.Trim().Contains(DateTime.Now.ToString("dd MMMM yyyy dddd").Trim()))
                               select new
                               {
                                   TcKimlikNo = d.tckimlikno,
                                   Adı_Soyadı = d.adi + " " + d.soyadi,
                                   Cinsiyeti = d.cinsiyeti,
                                   Kaldığı_Oda = d.kaldigioda,
                                   Cep_Telefonu = d.ceptelefonu,
                                   Giriş_Tarihi = d.giristarihi,
                                   Çıkış_Tarihi = d.cikistarihi
                               };
                    dataGridView1.DataSource = data;
                }
            }
            catch (Exception)
            {

            }
        }

        private void günlükGirişListesiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                label7.Text = "GÜNLÜK GİRİŞ LİSTESİ";
                string tarih = DateTime.Now.ToString("dd MMMM yyyy dddd").Substring(0, 1).Trim();

                if (Convert.ToInt16(tarih) == 0)
                {
                   
                    var data = from d in ctx.musterilers.Where(d => d.giristarihi.Trim().Contains(DateTime.Now.ToString("dd MMMM yyyy dddd").Substring(1).Trim()))
                               select new
                               {
                                   TcKimlikNo = d.tckimlikno,
                                   Adı_Soyadı = d.adi + " " + d.soyadi,
                                   Cinsiyeti = d.cinsiyet,
                                   Kaldığı_Oda = d.odano,
                                   Cep_Telefonu = d.ceptel,
                                   Giriş_Tarihi = d.giristarihi,
                                   Çıkış_Tarihi = d.cikistarihi
                               };
                    dataGridView1.DataSource = data;
                }
                else
                {
                   
                    var data = from d in ctx.gmusterilers.Where(d => d.giristarihi.Trim().Contains(DateTime.Now.ToString("dd MMMM yyyy dddd").Trim()))
                           select new
                           {
                               TcKimlikNo = d.tckimlikno,
                               Adı_Soyadı = d.adi + " " + d.soyadi,
                               Cinsiyeti = d.cinsiyeti,
                               Kaldığı_Oda = d.kaldigioda,
                               Cep_Telefonu = d.ceptelefonu,
                               Giriş_Tarihi = d.giristarihi,
                               Çıkış_Tarihi = d.cikistarihi
                           };
                    dataGridView1.DataSource = data;
                }
            }
            catch (Exception)
            {
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void temizlikRaporuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            temizle();
            label7.Text = "TEMİZLİK LİSTESİ";
            tiklananmenu = "temizlikliste";
            groupBox1.Visible = true;
            TextBox1.Visible = true;
            TextBox2.Visible = false;
            TextBox3.Visible = false;
            TextBox4.Visible = false;
            label1.Visible = true;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label1.Text = "Oda No : ";
            var data = from d in ctx.temizliks
                       select new
                       {
                            Oda_No= d.odano,
                           Temizlik_Tarihi = d.t_tarihi
                           
                       };
            dataGridView1.DataSource = data;
        }
    }
}
