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
    public partial class rezervasyon : Form
    {
        public rezervasyon()
        {
            InitializeComponent();
        }
        public bool donus=false;
        public string odano;
        private void tcdogrula(string tc)
        {
            
            string tcno = tc; int tekler_toplam = 0; int ciftler_toplam = 0; int sonuc = 0;

            try
            {
                if (Convert.ToInt32(tcno[0]) != 0)
                {

                    for (int i = 0; i < 9; i += 2)
                    {

                        tekler_toplam = tekler_toplam + Convert.ToInt32(tcno[i].ToString());

                    }
                    tekler_toplam = tekler_toplam * 7;
                    for (int i = 1; i < 8; i += 2)
                    {
                        ciftler_toplam = ciftler_toplam + Convert.ToInt32(tcno[i].ToString());

                    }
                    tekler_toplam = tekler_toplam - ciftler_toplam;

                    tekler_toplam = tekler_toplam % 10;

                    while (Convert.ToInt32(tcno[9].ToString()) == tekler_toplam)
                    {
                        for (int i = 0; i < 10; i++)
                        {

                            sonuc += tcno[i];
                            sonuc = sonuc % 10;
                        }
                        while (Convert.ToInt32(tcno[10].ToString()) == sonuc)
                        {

                            donus = true;

                            break;

                        }

                        break;
                    }
                }

            }
            catch (Exception)
            {
            }
            
        }
        
        oteltakipDataContext ctx = new oteltakipDataContext();

        private void button1_Click(object sender, EventArgs e)
        {
            donus = false;
            tcdogrula(textBox1.Text);
            try
            {
                if (donus == true && textBox1.Text.Length == 11 && textBox2.Text.Length > 3 && textBox3.Text.Length > 3&&textBox4.Text.Length==11)
                {
                    
                    dataGridView1.DataSource = ctx.rezervasyon1s;
                    rezervasyon1 rez = new rezervasyon1();
                    rez.tckimlik = textBox1.Text;
                    rez.adi = textBox2.Text;
                    rez.soyadi = textBox3.Text;
                    rez.giristarihi = dateTimePicker1.Value.ToString();
                    rez.cikistarihi = dateTimePicker2.Value.ToString();
                    rez.ceptel = textBox4.Text;
                    rez.oda__no = label6.Text.Substring(17).Trim();
                    ctx.rezervasyon1s.InsertOnSubmit(rez);
                    ctx.SubmitChanges();
                    
                    dataGridView1.DataSource = ctx.odadurumus.Where(xd => xd.odanoo==odano);
                    dataGridView1.Rows[0].Cells[1].Value = "rezerve".ToString();
                    ctx.SubmitChanges();
                    renk.Visible = false;
                    MessageBox.Show("Rezervasyonunuz Kaydedildi, Lütfen belirttiğiniz günde  kaydınızı otelimizden yaptırınız . Aksi takdirde rezervasyonunuz iptal edilecektir ve hakkınızda işlem yapılacaktır.");
                    this.Close();
                    

                }
                else
                {
                    if (textBox1.Text.Length != 11)
                        MessageBox.Show("Tc Kimlik no 11 haneli olmalıdır");
                    else if (textBox2.Text.Length < 3)
                        MessageBox.Show("Adınız en az 3 haneli olmalıdır");
                    else if (textBox3.Text.Length < 3)
                        MessageBox.Show("Soyadınız en az 3 haneli olmalıdır");
                    else if (textBox4.Text.Length < 11)
                        MessageBox.Show("Cep telefonunuz 11 haneli olmalıdır");
                    else if (donus == false)
                        MessageBox.Show("Geçerli Bir Tc Kimlik no giriniz.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) ; 
        }
        private void goster()
        {
            try
            {
                while (true)
                {
                    int sayac = 0;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button2.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button2.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button3.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button3.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button4.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button4.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button5.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button5.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button6.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button6.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button11.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button11.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button10.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button10.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button9.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button9.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button8.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button8.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button7.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button7.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button16.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button16.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button15.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button15.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button14.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button14.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button13.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button13.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button12.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button12.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button21.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button21.Visible = false;
                    }
                    sayac++; if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button20.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button20.Visible = false;
                    }
                    sayac++; if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button19.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button19.Visible = false;
                    }
                    sayac++; if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button18.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button18.Visible = false;
                    }
                    sayac++; if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button17.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button17.Visible = false;
                    }
                    sayac++; if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button26.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button26.Visible = false;
                    }
                    sayac++; if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button25.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button25.Visible = false;
                    }
                    sayac++; if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button24.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button24.Visible = false;
                    }
                    sayac++; if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button23.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button23.Visible = false;
                    }
                    sayac++; if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button22.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button22.Visible = false;
                    }
                    sayac++; if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button31.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button31.Visible = false;
                    }
                    sayac++; if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button30.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button30.Visible = false;
                    }
                    sayac++; if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button29.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button29.Visible = false;
                    }
                    sayac++; if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button28.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button28.Visible = false;
                    }
                    sayac++; if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button27.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button27.Visible = false;
                    }
                    sayac++; if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button36.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button36.Visible = false;
                    }
                    sayac++; if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button35.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button35.Visible = false;
                    }
                    sayac++; if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button34.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button34.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button33.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button33.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button32.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button32.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button41.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button41.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button40.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button40.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button39.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button39.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button38.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button38.Visible = false;
                    }
                    sayac++;
                    if (dataGridView1.Rows[sayac].Cells[1].Value.ToString() == "temiz boş")
                    {
                        button37.BackColor = Color.SpringGreen;
                    }
                    else
                    {
                        button37.Visible = false;
                    }
                    break;
                    
                }
            }
            catch (Exception)
            {
            }
        }
        private void butonvisible()
        {
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = true;
            button6.Visible = true;
            button7.Visible = true;
            button8.Visible = true;
            button9.Visible = true;
            button10.Visible = true;
            button11.Visible = true;
            button12.Visible = true;
            button13.Visible = true;
            button14.Visible = true;
            button15.Visible = true;
            button16.Visible = true;
            button17.Visible = true;
            button18.Visible = true;
            button19.Visible = true;
            button20.Visible = true;
            button21.Visible = true;
            button22.Visible = true;
            button23.Visible = true;
            button24.Visible = true;
            button25.Visible = true;
            button26.Visible = true;
            button27.Visible = true;
            button28.Visible = true;
            button29.Visible = true;
            button30.Visible = true;
            button31.Visible = true;
            button32.Visible = true;
            button33.Visible = true;
            button34.Visible = true;
            button35.Visible = true;
            button36.Visible = true;
            button37.Visible = true;
            button38.Visible = true;
            button39.Visible = true;
            button40.Visible = true;
            button41.Visible = true;
        }
        private void rezervasyon_Load(object sender, EventArgs e)
        {
            butonvisible(); 
            textBox4.MaxLength = 11;
            dataGridView1.DataSource = ctx.odadurumus;
           
            textBox1.MaxLength = 11;
            DateTimePicker dateTimePicker1 = new DateTimePicker();
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(+1);
            string yil = DateTime.Now.Year.ToString();
            string ay = DateTime.Now.Month.ToString();
            string gun = DateTime.Now.Day.ToString();
            goster();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar); 
        }
        private void tiklama(Control a)
        {
            label6.Text = "Seçilen Oda : " + a.Text;
            if (Convert.ToInt32(a.Text.Substring(3).Trim()) < 21)
            {
                label7.Text = "Oda ücreti : 80 TL";
            }
            else if (Convert.ToInt32(a.Text.Substring(3).Trim()) < 31)
            {
                label7.Text = "Oda ücreti : 200 TL ";
            }
            else
                label7.Text = "Oda ücreti : 150 TL";
            odano = a.Text.Substring(3).Trim();

            
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            tiklama(this.button2);
            renk = this.button2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tiklama(this.button3);
            renk = this.button3;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tiklama(this.button7);
            renk = this.button7;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value < DateTime.Now.AddDays(-1))
            {
                MessageBox.Show("Geçmiş tarihi başlangıç olarak giremezsiniz.");
                dateTimePicker1.Value = DateTime.Now;
            }
            dateTimePicker2.Value = dateTimePicker1.Value.AddDays(+1);
           
        }
        Control renk;

        private void button4_Click(object sender, EventArgs e)
        {
            tiklama(this.button4);
            renk = this.button4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            renk = this.button5;
            tiklama(this.button5);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tiklama(this.button6); renk = this.button6;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            tiklama(this.button11); renk = this.button11;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            tiklama(this.button10); renk = this.button10;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tiklama(this.button9); renk = this.button9;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tiklama(this.button8); renk = this.button8;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            tiklama(this.button16); renk = this.button16;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            tiklama(this.button15); renk = this.button15;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            tiklama(this.button14); renk = this.button14;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            tiklama(this.button13); renk = this.button13;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            tiklama(this.button12); renk = this.button12;
        }

        private void button21_Click(object sender, EventArgs e)
        {
            tiklama(this.button21); renk = this.button21;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            tiklama(this.button20); renk = this.button20;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            tiklama(this.button19); renk = this.button19;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            tiklama(this.button18); renk = this.button18;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            tiklama(this.button17); renk = this.button17;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            tiklama(this.button26); renk = this.button26;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            tiklama(this.button25); renk = this.button25;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            tiklama(this.button24); renk = this.button24;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            tiklama(this.button23); renk = this.button23;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            tiklama(this.button22); renk = this.button22;
        }

        private void button31_Click(object sender, EventArgs e)
        {
            tiklama(this.button31); renk = this.button31;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            tiklama(this.button30); renk = this.button30;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            tiklama(this.button29); renk = this.button29;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            tiklama(this.button28); renk = this.button28;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            tiklama(this.button27); renk = this.button27;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            tiklama(this.button36); renk = this.button36;
        }

        private void button35_Click(object sender, EventArgs e)
        {
            tiklama(this.button35); renk = this.button35;
        }

        private void button34_Click(object sender, EventArgs e)
        {
            tiklama(this.button34); renk = this.button34;
        }

        private void button33_Click(object sender, EventArgs e)
        {
            tiklama(this.button33); renk = this.button33;
        }

        private void button32_Click(object sender, EventArgs e)
        {
            tiklama(this.button32); renk = this.button32;
        }

        private void button41_Click(object sender, EventArgs e)
        {
            tiklama(this.button41); renk = this.button41;
        }

        private void button40_Click(object sender, EventArgs e)
        {
            tiklama(this.button40); renk = this.button40;
        }

        private void button39_Click(object sender, EventArgs e)
        {
            tiklama(this.button39); renk = this.button39;
        }

        private void button38_Click(object sender, EventArgs e)
        {
            tiklama(this.button38); renk = this.button38;
        }

        private void button37_Click(object sender, EventArgs e)
        {
            tiklama(this.button37); renk = this.button37;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                MessageBox.Show("Çıkış tarihi giriş tarihinden daha önce giremezsiniz.");
                dateTimePicker2.Value = dateTimePicker1.Value.AddDays(+1);
            }
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            
        }
    }
}
