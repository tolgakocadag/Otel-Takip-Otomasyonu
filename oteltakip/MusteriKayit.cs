using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Printing;
namespace oteltakip
{
    public partial class MusteriKayit : Form
    {
        public MusteriKayit()
        {
            InitializeComponent();
        }
        private void Sifirla()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            dateTimePicker1.ResetText();
            dateTimePicker2.ResetText();
            comboBox2.Text = "";
            comboBox1.Text = "";
            textBox9.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            pictureBox1.ImageLocation = "C:\\Users\\ÇakmaMadara\\Documents\\Visual Studio 2012\\Projects\\oteltakip\\oteltakip\\Resimler\\camera-icon-21.png";
        }
        private FilterInfoCollection webcam;
        private VideoCaptureDevice cam;
        oteltakipDataContext ctx = new oteltakipDataContext();
        void griddoldur()
        {
            dataGridView1.DataSource = ctx.musterilers;
        }
        public void griddoldurgecmis()
        {
            dataGridView1.DataSource = ctx.gmusterilers;
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                comboBox1.Items.Clear();
                for (int i = 0; i < 20; i++)
                {
                    comboBox1.Items.Add("ODA " + (i + 1));
                }
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                comboBox1.Items.Clear();
                for (int i = 20; i < 30; i++)
                {
                    comboBox1.Items.Add("ODA " + (i + 1));
                }
            }
            else
            {
                comboBox1.Items.Clear();
                for (int i = 30; i < 40; i++)
                {
                    comboBox1.Items.Add("ODA " + (i + 1));
                }
            }

        }

        private void button21_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button21;
            string d = button21.Text.ToString();
            tiklama(c, d);
            abc = this.button21;
            comboBox2.Text = comboBox2.Items[1].ToString();
            comboBox1.Text = this.button21.Text;
        }
        string Odano;

        private void tiklama(Control a, string b)
        {

            button34.Enabled = true;
            button33.Enabled = true;
            kaldigioda = a.Text;
            textBox1.Enabled = true;
            if (a.BackColor == Color.DimGray)
            {
                MessageBox.Show(b + " şu anda servis dışıdır.!");

                textBox1.Enabled = false;



            }

            if (a.BackColor == Color.DarkGreen)
            {
                DialogResult yesno = new DialogResult();
                yesno = MessageBox.Show("Bu oda temizlendi mi ?", "Uyarı", MessageBoxButtons.YesNo);
                if (yesno == DialogResult.Yes)
                {
                    dataGridView7.DataSource = ctx.odadurumus.Where(xxd => xxd.odanoo.Contains(b.Substring(3).Trim()));
                    dataGridView7.Rows[0].Cells[1].Value = "temiz boş";
                    ctx.SubmitChanges();
                    temizlik tmz = new temizlik();
                    tmz.odano = b;
                    tmz.t_tarihi = DateTime.Now.ToString();
                    ctx.temizliks.InsertOnSubmit(tmz);
                    ctx.SubmitChanges();
                    a.BackColor = Color.SpringGreen;
                    odanoogoster();
                    giris();
                    odanoogoster();
                    Sifirla();
                    sayibul();
                    odanoogoster();
                }
                else
                {
                    MessageBox.Show(b + " temizlenmediğinden dolayı satışa kapalıdır.!");

                    textBox1.Enabled = false;
                }



            }
            if (a.BackColor == Color.Red)
            {
                MessageBox.Show(b + " şu anda doludur.!");

                textBox1.Enabled = false;



            }
            if (a.BackColor == Color.DeepPink)
            {
                MessageBox.Show(b + " rezerve edilmiştir.!");
                button33.Enabled = false;
                button34.Enabled = false;
                textBox1.Enabled = false;



            }
            if (a.BackColor == Color.DarkRed)
            {
                DialogResult yesno = new DialogResult();
                yesno = MessageBox.Show("Bu oda temizlendi mi ?", "Uyarı", MessageBoxButtons.YesNo);
                if (yesno == DialogResult.Yes)
                {
                    dataGridView7.DataSource = ctx.odadurumus.Where(xxd => xxd.odanoo.Contains(b.Substring(3).Trim()));
                    dataGridView7.Rows[0].Cells[1].Value = "temiz dolu";
                    ctx.SubmitChanges();
                    temizlik tmz = new temizlik();
                    tmz.odano = b;
                    tmz.t_tarihi = DateTime.Now.ToString();
                    ctx.temizliks.InsertOnSubmit(tmz);
                    ctx.SubmitChanges();
                    a.BackColor = Color.Red;
                    odanoogoster();
                    giris();
                    odanoogoster();
                    Sifirla();
                    sayibul();
                    odanoogoster();
                }
                else
                {
                    MessageBox.Show(b + " şu anda doludur.!");

                    textBox1.Enabled = false;
                }



            }


            label13.Visible = true;
            comboBox3.Visible = true;
            button70.Visible = true;
            label13.Text = b;

            if (b.Length == 5)
                comboBox3.Text = b.Substring(3, 2).Trim();
            else
                comboBox3.Text = b.Substring(3, 3).Trim();
            dataGridView3.DataSource = ctx.odadurumus.Where(asdx => asdx.odanoo == comboBox3.Text);
            comboBox3.Text = dataGridView3.Rows[0].Cells[1].Value.ToString();


            Odano = a.Text;
            textBox1.Clear();
            dataGridView1.DataSource = ctx.musterilers.Where(musteri => musteri.odano.Contains(a.Text));
            if (dataGridView1.Rows.Count > 1)
            {
                Sifirla();


                textBox1.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.Rows[0].Cells[4].Value.ToString();
                dateTimePicker1.Text = dataGridView1.Rows[0].Cells[5].Value.ToString();
                dateTimePicker2.Text = dataGridView1.Rows[0].Cells[6].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[0].Cells[7].Value.ToString();
                comboBox1.Text = dataGridView1.Rows[0].Cells[9].Value.ToString();
                textBox9.Text = dataGridView1.Rows[0].Cells[8].Value.ToString();
                pictureBox1.ImageLocation = dataGridView1.Rows[0].Cells[12].Value.ToString();
                if (dataGridView1.Rows[0].Cells[11].Value.ToString() == "Erkek")
                    radioButton1.Checked = true;
                else
                    radioButton2.Checked = true;
            }

            dataGridView1.DataSource = ctx.rezervasyon1s.Where(musteri => musteri.oda__no.Contains(a.Text.Substring(3).Trim()));
            if (dataGridView1.Rows.Count > 1)
            {

                dataGridView1.DataSource = ctx.rezervasyon1s.Where(xxf => xxf.oda__no.Contains(a.Text.Substring(3).Trim()));
                textBox1.Text = dataGridView1.Rows[0].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[0].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[0].Cells[3].Value.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.Rows[0].Cells[4].Value.ToString());
                dateTimePicker2.Value = Convert.ToDateTime(dataGridView1.Rows[0].Cells[5].Value.ToString());
                textBox4.Text = dataGridView1.Rows[0].Cells[6].Value.ToString();
                string adana = dataGridView1.Rows[0].Cells[7].Value.ToString();
                if (Convert.ToInt16(adana) < 21)
                {
                    comboBox1.Text = "Tek Kişilik";
                }
                else if (Convert.ToInt16(adana) > 20 & Convert.ToInt16(adana) < 31)
                {
                    comboBox1.Text = "Evli-Çocuklu";
                }
                else
                {
                    comboBox1.Text = "Evli - Çocuksuz";
                }
                comboBox2.Text = a.Text;
            }








        }
        string durum;


        private void odanoogoster()
        {
            int sayi = 0; int odanogoster;
            dataGridView1.DataSource = ctx.odadurumus;
            for (int i = 0; i < 1; i++)
            {

                try
                {
                    while (true)
                    {


                        odanogoster = Convert.ToInt16(dataGridView1.Rows[sayi].Cells[2].Value);


                        dataGridView3.DataSource = ctx.odadurumus.Where(xdd => xdd.odanoo.Contains(odanogoster.ToString()));
                        durum = dataGridView3.Rows[0].Cells[1].Value.ToString();

                        if (odanogoster == 1)
                        {


                            if (durum == "kapali")
                                button1.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button1.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button1.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button1.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button1.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button1.BackColor = Color.DarkRed;
                            sayi++;
                        }

                        else if (odanogoster == 2)
                        {


                            if (durum == "kapali")
                                button2.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button2.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button2.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button2.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button2.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button2.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 3)
                        {


                            if (durum == "kapali")
                                button3.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button3.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button3.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button3.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button3.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button3.BackColor = Color.DarkRed; sayi++;
                            sayi++;


                        }
                        else if (odanogoster == 4)
                        {

                            if (durum == "kapali")
                                button71.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button71.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button71.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button71.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button71.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button71.BackColor = Color.DarkRed;
                            sayi++;


                        }
                        else if (odanogoster == 5)
                        {

                            if (durum == "kapali")
                                button5.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button5.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button5.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button5.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button5.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button5.BackColor = Color.DarkRed;
                            sayi++;
                        }
                        else if (odanogoster == 6)
                        {


                            if (durum == "kapali")
                                button6.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button6.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button6.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button6.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button6.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button6.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 7)
                        {


                            if (durum == "kapali")
                                button7.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button7.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button7.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button7.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button7.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button7.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 8)
                        {


                            if (durum == "kapali")
                                button8.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button8.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button8.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button8.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button8.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button8.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 9)
                        {


                            if (durum == "kapali")
                                button9.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button9.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button9.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button9.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button9.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button9.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 10)
                        {


                            if (durum == "kapali")
                                button10.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button10.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button10.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button10.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button10.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button10.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 11)
                        {


                            if (durum == "kapali")
                                button20.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button20.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button20.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button20.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button20.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button20.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 12)
                        {


                            if (durum == "kapali")
                                button18.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button18.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button18.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button18.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button18.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button18.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 13)
                        {


                            if (durum == "kapali")
                                button19.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button19.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button19.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button19.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button19.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button19.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 14)
                        {


                            if (durum == "kapali")
                                button17.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button17.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button17.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button17.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button17.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button17.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 15)
                        {


                            if (durum == "kapali")
                                button16.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button16.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button16.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button16.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button16.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button16.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 16)
                        {


                            if (durum == "kapali")
                                button15.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button15.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button15.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button15.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button15.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button15.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 17)
                        {


                            if (durum == "kapali")
                                button14.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button14.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button14.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button14.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button14.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button14.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 18)
                        {


                            if (durum == "kapali")
                                button13.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button13.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button13.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button13.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button13.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button13.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 19)
                        {


                            if (durum == "kapali")
                                button12.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button12.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button12.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button12.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button12.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button12.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 20)
                        {


                            if (durum == "kapali")
                                button11.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button11.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button11.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button11.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button11.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button11.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 21)
                        {


                            if (durum == "kapali")
                                button30.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button30.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button30.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button30.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button30.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button30.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 22)
                        {


                            if (durum == "kapali")
                                button28.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button28.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button28.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button28.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button28.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button28.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 23)
                        {


                            if (durum == "kapali")
                                button29.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button29.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button29.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button29.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button29.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button29.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 24)
                        {


                            if (durum == "kapali")
                                button27.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button27.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button27.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button27.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button27.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button27.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 25)
                        {


                            if (durum == "kapali")
                                button26.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button26.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button26.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button26.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button26.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button26.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 26)
                        {


                            if (durum == "kapali")
                                button25.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button25.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button25.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button25.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button25.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button25.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 27)
                        {


                            if (durum == "kapali")
                                button24.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button24.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button24.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button24.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button24.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button24.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 28)
                        {


                            if (durum == "kapali")
                                button23.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button23.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button23.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button23.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button23.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button23.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 29)
                        {


                            if (durum == "kapali")
                                button22.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button22.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button22.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button22.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button22.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button22.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 30)
                        {


                            if (durum == "kapali")
                                button21.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button21.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button21.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button21.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button21.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button21.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 31)
                        {


                            if (durum == "kapali")
                                button60.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button60.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button60.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button60.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button60.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button60.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 32)
                        {


                            if (durum == "kapali")
                                button58.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button58.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button58.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button58.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button58.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button58.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 33)
                        {


                            if (durum == "kapali")
                                button59.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button59.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button59.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button59.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button59.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button59.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 34)
                        {


                            if (durum == "kapali")
                                button57.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button57.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button57.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button57.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button57.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button57.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 35)
                        {


                            if (durum == "kapali")
                                button56.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button56.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button56.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button56.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button56.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button56.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 36)
                        {


                            if (durum == "kapali")
                                button55.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button55.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button55.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button55.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button55.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button55.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 37)
                        {


                            if (durum == "kapali")
                                button54.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button54.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button54.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button54.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button54.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button54.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 38)
                        {


                            if (durum == "kapali")
                                button53.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button53.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button53.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button53.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button53.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button53.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 39)
                        {


                            if (durum == "kapali")
                                button52.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button52.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button52.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button52.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button52.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button52.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else if (odanogoster == 40)
                        {


                            if (durum == "kapali")
                                button51.BackColor = Color.DimGray;
                            else if (durum == "rezerve")
                                button51.BackColor = Color.DeepPink;
                            else if (durum == "temiz boş")
                                button51.BackColor = Color.SpringGreen;
                            else if (durum == "temiz dolu")
                                button51.BackColor = Color.Red;
                            else if (durum == "kirli boş")
                                button51.BackColor = Color.DarkGreen;
                            else if (durum == "kirli dolu")
                                button51.BackColor = Color.DarkRed;
                            sayi++;

                        }
                        else
                            break;




                    }


                }
                catch (Exception)
                {
                }
            }

        }



        private void giris()
        {
            button1.BackColor = Color.Green;
            button2.BackColor = Color.Green;
            button3.BackColor = Color.Green;
            button71.BackColor = Color.SpringGreen;
            button5.BackColor = Color.Green;
            button6.BackColor = Color.Green;
            button7.BackColor = Color.Green;
            button8.BackColor = Color.Green;
            button9.BackColor = Color.Green;
            button10.BackColor = Color.Green;
            button11.BackColor = Color.Green;
            button12.BackColor = Color.Green;
            button13.BackColor = Color.Green;
            button14.BackColor = Color.Green;
            button15.BackColor = Color.Green;
            button16.BackColor = Color.Green;
            button17.BackColor = Color.Green;
            button18.BackColor = Color.Green;
            button19.BackColor = Color.Green;
            button20.BackColor = Color.Green;
            button21.BackColor = Color.Green;
            button22.BackColor = Color.Green;
            button23.BackColor = Color.Green;
            button24.BackColor = Color.Green;
            button25.BackColor = Color.Green;
            button26.BackColor = Color.Green;
            button27.BackColor = Color.Green;
            button28.BackColor = Color.Green;
            button29.BackColor = Color.Green;
            button30.BackColor = Color.Green;
            button51.BackColor = Color.Green;
            button52.BackColor = Color.Green;
            button53.BackColor = Color.Green;
            button54.BackColor = Color.Green;
            button55.BackColor = Color.Green;
            button56.BackColor = Color.Green;
            button57.BackColor = Color.Green;
            button58.BackColor = Color.Green;
            button59.BackColor = Color.Green;
            button60.BackColor = Color.Green;
        }
        void odalaridoldur()
        {
            dataGridView3.DataSource = ctx.odadurumus;
        }

        void sayibul()
        {
            try
            {
                odalaridoldur();
                int sayi = 0;
                int temizbos = 0;
                int kirlibos = 0;
                int temizdolu = 0;
                int kirlidolu = 0;
                int rezerv = 0;
                int kapali = 0;
                while (true)
                {
                    if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == "temiz dolu")
                    {
                        temizdolu++; button67.Text = temizdolu.ToString(); ;
                    }
                    else if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == "temiz boş")
                    {
                        temizbos++; button66.Text = temizbos.ToString();
                    }
                    else if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == "kirli dolu")
                    {
                        kirlidolu++; button65.Text = kirlidolu.ToString();
                    }
                    else if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == "kirli boş")
                    {
                        kirlibos++; button64.Text = kirlibos.ToString();
                    }
                    else if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == "rezerve")
                    {
                        rezerv++; button49.Text = rezerv.ToString();
                    }
                    else if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == "kapali")
                    {
                        kapali++;
                        button69.Text = kapali.ToString();
                    }
                    button67.Text = temizdolu.ToString();
                    button66.Text = temizbos.ToString();
                    button65.Text = kirlidolu.ToString();
                    button64.Text = kirlibos.ToString();
                    button49.Text = rezerv.ToString();
                    button45.Text = (temizdolu + kirlidolu).ToString();
                    button46.Text = (40 - (temizdolu + kirlidolu) - kapali - rezerv).ToString();

                    sayi++;
                }

            }
            catch (Exception)
            {

            }

        }

        public string kaldigioda;
        public bool donus = false;
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
        private void MusteriKayit_Load(object sender, EventArgs e)
        {

            comboBox2.Enabled = false;
            comboBox2.ForeColor = Color.Red;
            comboBox1.ForeColor = Color.Red;
            comboBox1.Enabled = false;
            label13.Visible = false;
            comboBox3.Visible = false;
            button70.Visible = false;
            button44.Text = "40";
            odanoogoster();
            giris();
            odanoogoster();
            Sifirla();
            sayibul();

            pictureBox1.ImageLocation = "C:\\Users\\ÇakmaMadara\\Documents\\Visual Studio 2012\\Projects\\oteltakip\\oteltakip\\Resimler\\camera-icon-21.png";
            label10.Text = null;
            label10.Visible = false;
            textBox1.MaxLength = 11;
            textBox4.MaxLength = 11;

            try
            {
                int sayi = 0;
                while (true)
                {
                    dataGridView1.DataSource = ctx.musterilers;
                    sirano = Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value.ToString());
                    string dt = dataGridView1.Rows[sayi].Cells[6].Value.ToString();

                    if (Convert.ToDateTime(dt).Day < DateTime.Now.Day)
                    {

                        kaldigioda = dataGridView1.Rows[sayi].Cells[9].Value.ToString();
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 1")
                        {
                            button1_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 2")
                        {
                            button2_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 3")
                        {
                            button3_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 4")
                        {
                            button71_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 5")
                        {
                            button5_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 6")
                        {
                            button6_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 7")
                        {
                            button7_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 8")
                        {
                            button8_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 9")
                        {
                            button9_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 10")
                        {
                            button10_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 11")
                        {
                            button20_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 12")
                        {
                            button18_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 13")
                        {
                            button19_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 14")
                        {
                            button17_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 15")
                        {
                            button16_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 16")
                        {
                            button15_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 17")
                        {
                            button14_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 18")
                        {
                            button13_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 19")
                        {
                            button12_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 20")
                        {
                            button11_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 21")
                        {
                            button30_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 22")
                        {
                            button28_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 23")
                        {
                            button29_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 24")
                        {
                            button27_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 25")
                        {
                            button26_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 26")
                        {
                            button25_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 27")
                        {
                            button24_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 28")
                        {
                            button23_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 29")
                        {
                            button22_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 30")
                        {
                            button21_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 31")
                        {
                            button60_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 32")
                        {
                            button58_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 33")
                        {
                            button59_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 34")
                        {
                            button57_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 35")
                        {
                            button56_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 36")
                        {
                            button55_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 37")
                        {
                            button54_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 38")
                        {
                            button53_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 39")
                        {
                            button52_Click(sender, e);
                        }
                        if (dataGridView1.Rows[sayi].Cells[9].Value.ToString() == "ODA 40")
                        {
                            button51_Click(sender, e);
                        }


                        button34_Click(sender, e);

                    } sayi++;
                }


            }
            catch (Exception)
            {

            }
            Sifirla();
            sayibul();

            odanoogoster();





            webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            cam = new VideoCaptureDevice(webcam[0].MonikerString);
            cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);

        }
        private void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bit = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = bit;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (cam.IsRunning)
            {
                cam.Stop();
            }
            else
                cam.Start();
        }
        private void MusteriKayit_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cam.IsRunning)
            {
                cam.Stop();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0 && comboBox1.SelectedIndex < 20)
            {
                textBox9.Text = 80 + " TL";
            }
            else if (comboBox2.SelectedIndex == 1 && comboBox1.SelectedIndex < 30)
            {
                textBox9.Text = 200 + " TL";
            }
            else
            {
                textBox9.Text = 150 + " TL";
            }
        }
        private void musteri()
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            abc = this.button1;
            Control c;
            string d = button1.Text;
            c = this.button1;
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button1.Text;
            textBox9.Text = "80 TL";
        }
        private void sontiklanan(Control a)
        {
            a.BackColor = Color.Green;
        }
        Control abc;
        private void button2_Click(object sender, EventArgs e)
        {
            abc = this.button2;
            Control c;
            string d = button2.Text;
            c = this.button2;

            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button2.Text;
            textBox9.Text = "80 TL";

        }

        private void button35_Click(object sender, EventArgs e)
        {
            cam.Start();
        }

        private void button36_Click(object sender, EventArgs e)
        {
            cam.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Control c;
            abc = this.button3;
            c = this.button3;
            string d = button3.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button3.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {



        }

        private void button5_Click(object sender, EventArgs e)
        {
            Control c;
            abc = this.button5;
            c = this.button5;
            string d = button5.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button5.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Control c;
            abc = this.button6;
            c = this.button6;
            string d = button6.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button6.Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Control c;
            abc = this.button7;
            c = this.button7;
            string d = button7.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button7.Text;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Control c;
            abc = this.button8;
            c = this.button8;
            string d = button8.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button8.Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Control c;
            abc = this.button9;
            c = this.button9;
            string d = button9.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button9.Text;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Control c;
            abc = this.button10;
            c = this.button10;
            string d = button10.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button10.Text;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button20;
            string d = button20.Text.ToString();
            tiklama(c, d); comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button20.Text;
            abc = this.button20;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Control c;
            abc = this.button18;
            c = this.button18;
            string d = button18.Text.ToString();
            tiklama(c, d); comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button18.Text;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Control c; abc = this.button19;
            c = this.button19;
            string d = button19.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button19.Text;

        }

        private void button17_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button17;
            abc = this.button17;
            string d = button17.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button17.Text;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            Control c;
            abc = this.button16;
            c = this.button16;
            string d = button16.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button16.Text;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button15;
            abc = this.button15;
            string d = button15.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button15.Text;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button14;
            string d = button14.Text.ToString();
            abc = this.button14;
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button14.Text;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button13;
            string d = button13.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button13.Text;
            abc = this.button13;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button12;
            string d = button12.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button12.Text;
            abc = this.button12;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button11;
            string d = button11.Text.ToString();
            tiklama(c, d);
            abc = this.button11;
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button11.Text;
        }

        private void button30_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button30;
            string d = button30.Text.ToString();
            tiklama(c, d);
            abc = this.button30;
            comboBox2.Text = comboBox2.Items[1].ToString();
            comboBox1.Text = this.button30.Text;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button28;
            string d = button28.Text.ToString();
            tiklama(c, d);
            abc = this.button28;
            comboBox2.Text = comboBox2.Items[1].ToString();
            comboBox1.Text = this.button28.Text;
        }

        private void button29_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button29;
            string d = button29.Text.ToString();
            tiklama(c, d);
            abc = this.button29;
            comboBox2.Text = comboBox2.Items[1].ToString();
            comboBox1.Text = this.button29.Text;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button27;
            string d = button27.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[1].ToString();
            comboBox1.Text = this.button27.Text;
            abc = this.button27;
        }

        private void button26_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button26;
            string d = button26.Text.ToString();
            tiklama(c, d);
            abc = this.button26;
            comboBox2.Text = comboBox2.Items[1].ToString();
            comboBox1.Text = this.button26.Text;
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button25;
            string d = button25.Text.ToString();
            tiklama(c, d);
            abc = this.button25;
            comboBox2.Text = comboBox2.Items[1].ToString();
            comboBox1.Text = this.button25.Text;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button24;
            string d = button24.Text.ToString();
            tiklama(c, d);
            abc = this.button24;
            comboBox2.Text = comboBox2.Items[1].ToString();
            comboBox1.Text = this.button24.Text;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button23;
            string d = button23.Text.ToString();
            tiklama(c, d);
            abc = this.button23;
            comboBox2.Text = comboBox2.Items[1].ToString();
            comboBox1.Text = this.button23.Text;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Control c;
            abc = this.button22;
            c = this.button22;
            string d = button22.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[1].ToString();
            comboBox1.Text = this.button22.Text;
        }

        private void button60_Click(object sender, EventArgs e)
        {
            Control c; abc = this.button60;
            c = this.button60;
            string d = button60.Text.ToString();
            tiklama(c, d);

            comboBox2.Text = comboBox2.Items[2].ToString();
            comboBox1.Text = this.button60.Text;
        }

        private void button58_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button58;
            string d = button58.Text.ToString();
            tiklama(c, d);
            abc = this.button58;
            comboBox2.Text = comboBox2.Items[2].ToString();
            comboBox1.Text = this.button58.Text;
        }

        private void button59_Click(object sender, EventArgs e)
        {
            Control c; abc = this.button59;
            c = this.button59;
            string d = button59.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[2].ToString();
            comboBox1.Text = this.button59.Text;
        }

        private void button57_Click(object sender, EventArgs e)
        {
            Control c; abc = this.button57;
            c = this.button57;
            string d = button57.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[2].ToString();
            comboBox1.Text = this.button57.Text;
        }

        private void button56_Click(object sender, EventArgs e)
        {
            Control c; abc = this.button56;
            c = this.button56;
            string d = button56.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[2].ToString();
            comboBox1.Text = this.button56.Text;
        }

        private void button55_Click(object sender, EventArgs e)
        {
            Control c; abc = this.button55;
            c = this.button55;
            string d = button55.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[2].ToString();
            comboBox1.Text = this.button55.Text;
        }
        private void btnGuid()
        {
            string GuidKey = Guid.NewGuid().ToString();
            anahtars = GuidKey;
        }
        private void button54_Click(object sender, EventArgs e)
        {
            Control c; abc = this.button54;
            c = this.button54;
            string d = button54.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[2].ToString();
            comboBox1.Text = this.button54.Text;
        }

        private void button53_Click(object sender, EventArgs e)
        {
            Control c; abc = this.button53;
            c = this.button53;
            string d = button53.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[2].ToString();
            comboBox1.Text = this.button53.Text;
        }

        private void button52_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button52;
            string d = button52.Text.ToString();
            tiklama(c, d); abc = this.button52;
            comboBox2.Text = comboBox2.Items[2].ToString();
            comboBox1.Text = this.button52.Text;
        }

        private void button51_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button51;
            string d = button51.Text.ToString();
            tiklama(c, d); abc = this.button51;
            comboBox2.Text = comboBox2.Items[2].ToString();
            comboBox1.Text = this.button51.Text;
        }
        bool kayıtlımı;
        private bool kayitlimi()
        {
            kayıtlımı = true;
            griddoldur();
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                if (dataGridView1.Rows[i].Cells[9].Value.ToString() == comboBox1.Text.ToString())
                {
                    if (dataGridView1.Rows[i].Cells[10].Value.ToString() == "False".ToString())
                    {
                        kayıtlımı = false;
                        break;
                    }
                }
            }
            return kayıtlımı;

        }
        public string anahtars;

        public string odanoo;
        private void button31_Click(object sender, EventArgs e)
        {
            btnGuid();
            bool a = kayitlimi();
            tcdogrula(textBox1.Text);
            while (a == true)
            {

                if (donus == true && textBox1.TextLength == 11 && (textBox2.TextLength > 2 && textBox2.TextLength < 30) && (textBox3.TextLength > 2 && textBox3.TextLength < 30) &&
                    textBox4.TextLength == 11 && (label10.Text == "Erkek" || label10.Text == "Kadın") && comboBox1.Text.Length > 0)
                {

                    try
                    {
                        musteriler ms = new musteriler();
                        ms.tckimlikno = textBox1.Text;
                        ms.adi = textBox2.Text;
                        ms.soyadi = textBox3.Text;
                        ms.ceptel = textBox4.Text;
                        ms.giristarihi = dateTimePicker1.Text.ToString();
                        ms.cikistarihi = dateTimePicker2.Text.ToString();
                        ms.odaturu = comboBox2.Text;
                        ms.tutar = textBox9.Text;
                        ms.odano = comboBox1.Text;
                        ms.bosmu = "False";
                        ms.cinsiyet = label10.Text;
                        ms.resim = label11.Text;
                        ms.anahtar = anahtars;

                        ctx.musterilers.InsertOnSubmit(ms);
                        ctx.SubmitChanges();
                        musterihesaplari mh = new musterihesaplari();
                        mh.kullaniciadi = textBox1.Text;
                        mh.sifre = textBox4.Text;
                        mh.oda_numarasi = comboBox1.Text;
                        ctx.musterihesaplaris.InsertOnSubmit(mh);
                        ctx.SubmitChanges();
                        griddoldur();
                        giris();
                        sayibul();
                        string abcc = comboBox1.Text.ToString();
                        if (comboBox1.Text.Length == 5)
                            abcc = abcc.Substring(3, 2);
                        else
                            abcc = abcc.Substring(3, 3);

                        abcc = abcc.Trim();
                        odanoo = abcc;
                        odadurumu oda = ctx.odadurumus.SingleOrDefault(asd => asd.odanoo == abcc);
                        oda.odadurumu1 = "temiz dolu";
                        ctx.SubmitChanges();

                        abc.BackColor = Color.Red;

                        Sifirla();
                        odanoogoster();
                        sayibul();
                        odanoogoster();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Oda seçiniz");
                    }

                    MessageBox.Show("Kayıt eklendi");
                    try
                    {
                        rezervasyon1 rzv = ctx.rezervasyon1s.SingleOrDefault(xxd => xxd.oda__no == odanoo.ToString());
                        ctx.rezervasyon1s.DeleteOnSubmit(rzv);
                        ctx.SubmitChanges();
                    }
                    catch (Exception)
                    {
                    }
                    donus = false;
                    sayibul();
                    break;

                }
                else
                {
                    if (donus == false)
                    {
                        MessageBox.Show("Geçerli bir tc kimlik no giriniz");
                    }
                    else
                        MessageBox.Show("Lütfen alanları doldurunuz"); break;
                }
            }
            if (kayıtlımı == false)
            {
                MessageBox.Show("Bu oda Doludur!");
            }


        }

        ////CRUD işlemleri class
        //private int CreateAccount(musteriler accounts)
        //{
        //    int id = 0;
        //    return id;
        //}


        private void groupBox6_Enter(object sender, EventArgs e)
        {
            //musteriler m = new musteriler();
            //m.adi = "";
            //this.CreateAccount(m);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label10.Text = radioButton1.Text;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label10.Text = radioButton2.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button33_Click(object sender, EventArgs e)
        {
            try
            {
                musteriler mus = ctx.musterilers.SingleOrDefault(must => must.tckimlikno == textBox1.Text);
                mus.tckimlikno = textBox1.Text;
                mus.adi = textBox2.Text;
                mus.soyadi = textBox3.Text;
                mus.ceptel = textBox4.Text;
                mus.giristarihi = dateTimePicker1.Value.ToString();
                mus.cikistarihi = dateTimePicker2.Value.ToString();
                mus.odaturu = comboBox2.Text;
                mus.tutar = textBox9.Text;
                mus.odano = comboBox1.Text;
                mus.cinsiyet = label10.Text;
                ctx.SubmitChanges();
                griddoldur();
                musterihesaplari mhesap = ctx.musterihesaplaris.SingleOrDefault(musth => musth.kullaniciadi == textBox1.Text);
                mhesap.kullaniciadi = textBox1.Text;
                mhesap.sifre = textBox4.Text;
                mhesap.oda_numarasi = comboBox1.Text;
                ctx.SubmitChanges();
                MessageBox.Show("Başarıyla Güncellendi");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        private void tiklanan(Control a)
        {
            a.BackColor = Color.Green;
        }
        public int sirano = 0;
        DialogResult sil = new DialogResult();
        private void button34_Click(object sender, EventArgs e)
        {
            try
            {

                sil = MessageBox.Show(kaldigioda + " bilgileri silinecektir! Nedeni : Konaklama süresinin bitmesi veya zorunlu çıkıştır! Emin Misiniz ?", "Uyarı", MessageBoxButtons.YesNo);
                if (sil == DialogResult.Yes)
                {

                    dataGridView1.DataSource = ctx.musterilers.Where(mus => mus.tckimlikno.Contains(textBox1.Text));

                    gmusteriler gmus = new gmusteriler();
                    gmus.tckimlikno = dataGridView1.Rows[0].Cells[1].Value.ToString();
                    gmus.adi = dataGridView1.Rows[0].Cells[2].Value.ToString();
                    gmus.soyadi = dataGridView1.Rows[0].Cells[3].Value.ToString();
                    gmus.ceptelefonu = dataGridView1.Rows[0].Cells[4].Value.ToString();
                    gmus.cinsiyeti = dataGridView1.Rows[0].Cells[11].Value.ToString();
                    gmus.giristarihi = dataGridView1.Rows[0].Cells[5].Value.ToString();
                    gmus.cikistarihi = dataGridView1.Rows[0].Cells[6].Value.ToString();
                    gmus.odaturu = dataGridView1.Rows[0].Cells[7].Value.ToString();
                    gmus.kaldigioda = dataGridView1.Rows[0].Cells[9].Value.ToString();
                    gmus.odedigiucret = dataGridView1.Rows[0].Cells[8].Value.ToString();
                    gmus.resimi = dataGridView1.Rows[0].Cells[12].Value.ToString();
                    ctx.gmusterilers.InsertOnSubmit(gmus);
                    ctx.SubmitChanges();

                    string abcc = dataGridView1.Rows[0].Cells[9].Value.ToString();
                    if (comboBox1.Text.Length == 5)
                        abcc = abcc.Substring(3, 2);
                    else
                        abcc = abcc.Substring(3, 3);

                    abcc = abcc.Trim();
                    odadurumu oda = ctx.odadurumus.SingleOrDefault(asd => asd.odanoo == abcc);
                    oda.odadurumu1 = "kirli boş";




                    abc.BackColor = Color.Red;
                    ctx.SubmitChanges();
                    griddoldurgecmis();
                    griddoldur();
                    musteriler musss = ctx.musterilers.SingleOrDefault(musssss => musssss.odano == comboBox1.Text);
                    ctx.musterilers.DeleteOnSubmit(musss);
                    ctx.SubmitChanges();
                    musterihesaplari mhs = ctx.musterihesaplaris.SingleOrDefault(mhsss => mhsss.oda_numarasi == comboBox1.Text);
                    ctx.musterihesaplaris.DeleteOnSubmit(mhs);
                    ctx.SubmitChanges();
                    tiklanan(abc);
                    rezervasyon1 rezer = ctx.rezervasyon1s.SingleOrDefault(xxxd => xxxd.oda__no == abcc);
                    ctx.rezervasyon1s.DeleteOnSubmit(rezer);
                    ctx.SubmitChanges();
                    griddoldur();
                    giris();
                    odanoogoster();
                    sayibul();
                    odanoogoster();

                    Sifirla();
                    abc.BackColor = Color.DarkGreen;

                    MessageBox.Show("Kayıt silindi!");

                    try
                    {
                        ctx.misafir1s.DeleteAllOnSubmit(ctx.misafir1s.Where(x => x.geldigioda == "ODA " + abcc));
                        ctx.SubmitChanges();
                    }
                    catch (Exception)
                    {
                    }


                }
            }
            catch (Exception)
            { }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            cam.Stop();
            openFileDialog1.InitialDirectory = "C:\\Users\\ÇakmaMadara\\Documents\\Visual Studio 2012\\Projects\\oteltakip\\oteltakip\\Resimler";

            openFileDialog1.Filter = "All Files|*.*|Bitmap|*.bmp|GIF|*.gif|JPEG|*.jpg";

            openFileDialog1.FilterIndex = 4;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                label11.Text = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(label11.Text);
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            cam.Stop();
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                saveFileDialog1.InitialDirectory = "C:\\Users\\ÇakmaMadara\\Documents\\Visual Studio 2012\\Projects\\oteltakip\\oteltakip\\Resimler";
                pictureBox1.Image.Save(saveFileDialog1.FileName);//Picturebox'taki görüntüyü kaydediyoruz.
            }
            label11.Text = saveFileDialog1.FileName;
            pictureBox1.Image = Image.FromFile(label11.Text);
        }

        private void MusteriKayit_FormClosed(object sender, FormClosedEventArgs e)
        {


        }

        private void button39_Click(object sender, EventArgs e)
        {

        }

        private void button39_Click_1(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void button40_Click(object sender, EventArgs e)
        {

        }

        private void button41_Click(object sender, EventArgs e)
        {

        }

        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void button39_Click_2(object sender, EventArgs e)
        {

        }

        private void printPreviewDialog1_Load_1(object sender, EventArgs e)
        {

        }

        private void button39_Click_3(object sender, EventArgs e)
        {

            printPreviewDialog1.ShowDialog();

        }
        Font baslik = new Font("Verdana", 15, FontStyle.Bold);
        Font altyazi = new Font("Verdana", 9);
        Font normal = new Font("Verdana", 10, FontStyle.Bold);
        SolidBrush sb = new SolidBrush(Color.Black);
        private void printDocument1_PrintPage_2(object sender, PrintPageEventArgs e)
        {
            try
            {
                StringFormat st = new StringFormat();
                st.Alignment = StringAlignment.Near;
                e.Graphics.DrawString("X Otel", baslik, sb, 100, 100, st);
                e.Graphics.DrawString("KOCADAĞ OTELCİLİK A.Ş. İstanbul Şubesi\n" +
                    "X Mah. X Cad. No:X 0xxxx\n" +
                    "X, Bahçelievler/İstanbul\n" +
                    "Tel: 0xxx xxx xx xx Faks: 0xxx xxx xx xx\n" +
                    "Maslak V.D. xxx xxx xx xx\n" +
                    "İstanbul Ticaret Odası Tic. Sic. No: xxxxxx\n" +
                    "hotel@xotel.com", altyazi, sb, 100, 135, st);
                e.Graphics.DrawImage(pictureBox2.Image, 400, 100, 100, 125);
                e.Graphics.DrawImage(pictureBox3.Image, 585, 100, 150, 175);
                string a = DateTime.Now.ToString();
                e.Graphics.DrawString("Date/Tarih        " + a.Substring(0, 10), normal, sb, 120, 300, st);
                e.Graphics.DrawString("Time/Saat         " + a.Substring(11), normal, sb, 120, 340, st);
                e.Graphics.DrawString("X Sigorta A.S.\n" +
                "Eski Buyukdere Caddesi, No 2\n" +
                "Maslak,\n" +
                "Istanbul, Turkey\n" +
                "VD Buyuk Mukellefler xxxxxxxxxx\n" +
                "Kocadag, Tolga", normal, sb, 120, 380, st);
                e.Graphics.DrawString("Room/Oda           " + comboBox1.Text.Substring(4) + "                                  " + comboBox2.Text, normal, sb, 120, 520, st);
                e.Graphics.DrawString("Arrival/Giriş      " + dateTimePicker1.Value.ToString().Substring(0, 10) + "        Departure/Çıkış  " + dateTimePicker2.Value.ToString().Substring(0, 10), normal, sb, 120, 560, st);
                e.Graphics.DrawString("Cashier/Kasiyer    " + label12.Text, normal, sb, 120, 600, st);
                e.Graphics.DrawString("PO No", normal, sb, 120, 640, st);
                e.Graphics.DrawString("______________________________________________________________", normal, sb, 130, 680, st);
                e.Graphics.DrawString("  Açıklama/Description                                                                               Tutar\n\n\n       " +
                comboBox2.Text + " (" + textBox2.Text + " " + textBox3.Text + ")                                                " + textBox9.Text + "\n\n\n\n\n\nToplam Tutar" +
                "                                                                                              " + textBox9.Text, normal, sb, 130, 710, st);
                e.Graphics.DrawString("\n|                                                                   |                                                                 |\n" +
                                        "|                                                                   |                                                                 |\n" +
                                        "|                                                                   |                                                                 |\n" +
                                                        "|                                                                                                                                      |\n" +
                                                                        "|                                                                                                                                      |\n" +
                                                                                        "|                                                                                                                                      |\n" +
                                                                                                        "|                                                                                                                                      |\n" +
                                                                                                                        "|                                                                                                                                      |\n" +
                                                                                                                                        "|                                                                                                                                      |\n" +
                                "|                                                                                                                                      |\n" +
                                "|                                                                                                                                      |\n" +
                                "|                                                                                                                                      |\n" +
                                "|                                                                                                                                      |\n", altyazi, sb, 125, 680, st);
                e.Graphics.DrawString("______________________________________________________________", normal, sb, 130, 724, st);
                e.Graphics.DrawString("______________________________________________________________", normal, sb, 130, 845, st);
                e.Graphics.DrawString("______________________________________________________________", normal, sb, 130, 875, st);
                e.Graphics.DrawString("İmza/Kaşe", normal, sb, 500, 1000, st);
            }
            catch (Exception)
            {
                MessageBox.Show("Odayı seçiniz", "Uyarı");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button40_Click_1(object sender, EventArgs e)
        {
            button1.Visible = true; button2.Visible = true; button3.Visible = true; button71.Visible = true; button5.Visible = true;
            button10.Visible = true; button9.Visible = true; button8.Visible = true; button7.Visible = true; button6.Visible = true;
            button11.Visible = true; button17.Visible = true; button28.Visible = true; button29.Visible = true; button55.Visible = true;
            button12.Visible = true; button18.Visible = true; button27.Visible = true; button30.Visible = true; button56.Visible = true;
            button13.Visible = true; button19.Visible = true; button26.Visible = true; button51.Visible = true; button57.Visible = true;
            button14.Visible = true; button20.Visible = true; button25.Visible = true; button52.Visible = true; button58.Visible = true;
            button15.Visible = true; button21.Visible = true; button24.Visible = true; button53.Visible = true; button59.Visible = true;
            button16.Visible = true; button22.Visible = true; button23.Visible = true; button54.Visible = true; button60.Visible = true;
        }
        private void filtre(string filt)
        {
            odalaridoldur();
            int sayi = 0;
            while (true)
            {
                button1.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button1.Visible = true;
                break;
            }
            sayi++;
            while (true)
            {
                button2.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button2.Visible = true;
                break;
            }
            sayi++;
            while (true)
            {
                button3.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button3.Visible = true;
                break;
            }
            sayi++;
            while (true)
            {
                button71.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button71.Visible = true;
                break;
            }
            sayi++;
            while (true)
            {
                button5.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button5.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button6.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button6.Visible = true;
                break;
            }
            sayi++;
            while (true)
            {
                button7.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button7.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button8.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button8.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button9.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button9.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button10.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button10.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button20.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button20.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button18.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button18.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button19.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button19.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button17.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button17.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button16.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button16.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button15.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button15.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button14.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button14.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button13.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button13.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button12.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button12.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button11.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button11.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button30.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button30.Visible = true; break;
            }
            sayi++;
            while (true)
            {

                button28.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button28.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button29.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button29.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button27.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button27.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button26.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button26.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button25.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button25.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button24.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button24.Visible = true;
                break;
            }
            sayi++;
            while (true)
            {
                button23.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button23.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button22.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button22.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button21.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button21.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button60.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button60.Visible = true;
                break;
            }
            sayi++;
            while (true)
            {
                button58.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button58.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button59.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button59.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button57.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button57.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button56.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button56.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button55.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button55.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button54.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button54.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button53.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button53.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button52.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button52.Visible = true; break;
            }
            sayi++;
            while (true)
            {
                button51.Visible = false;
                if (dataGridView3.Rows[sayi].Cells[1].Value.ToString() == filt)
                    button51.Visible = true; break;
            }
        }

        private void button41_Click_1(object sender, EventArgs e)
        {

            try
            {
                filtre("temiz dolu");

            }
            catch (Exception)
            { }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            try
            {
                filtre("temiz boş");
            }
            catch (Exception)
            {

            }
        }

        private void button68_Click(object sender, EventArgs e)
        {
            try
            {
                filtre("kapali");

            }
            catch (Exception)
            { }
        }

        private void button61_Click(object sender, EventArgs e)
        {
            try
            {
                filtre("kirli dolu");

            }
            catch (Exception)
            { }
        }

        private void button50_Click(object sender, EventArgs e)
        {
            try
            {
                filtre("kirli boş");

            }
            catch (Exception)
            { }
        }

        private void button48_Click(object sender, EventArgs e)
        {
            try
            {
                filtre("rezerve");

            }
            catch (Exception)
            { }
        }

        private void button63_Click(object sender, EventArgs e)
        {
            try
            {
                filtre("temiz dolu");

            }
            catch (Exception)
            { }
        }

        private void button62_Click(object sender, EventArgs e)
        {
            try
            {
                filtre("temiz boş");

            }
            catch (Exception)
            { }
        }

        private void MusteriKayit_Click(object sender, EventArgs e)
        {

        }

        private void button70_Click(object sender, EventArgs e)
        {
            try
            {

                dataGridView3.DataSource = ctx.odadurumus.Where(asdx => asdx.odanoo.Contains(label13.Text.Substring(3).Trim()));
                dataGridView3.Rows[0].Cells[1].Value = comboBox3.Text;
                ctx.SubmitChanges();
                giris();
                odanoogoster();
                odalaridoldur();
                sayibul();
                odanoogoster();

                MessageBox.Show("Oda başarı ile güncellendi.");
                label13.Visible = false;
                comboBox3.Visible = false;
                button70.Visible = false; giris();
                odanoogoster();
                sayibul();
                odanoogoster();
            }
            catch (Exception)
            {
                giris();
                odanoogoster();
                sayibul();
                odanoogoster();
            }
        }

        private void button71_Click(object sender, EventArgs e)
        {
            Control c;
            c = this.button71;
            abc = this.button71;
            string d = button71.Text.ToString();
            tiklama(c, d);
            comboBox2.Text = comboBox2.Items[0].ToString();
            comboBox1.Text = this.button71.Text;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }
        public int timersayac = 0;
        private void baloncuk2(Control al, string Oddano)
        {
            string odano = Oddano;
            try
            {
                if (dataGridView5.Rows[0].Cells[1].Value.ToString() == "temiz dolu" | dataGridView5.Rows[0].Cells[1].Value.ToString() == "kirli dolu")
                {
                    dataGridView5.DataSource = ctx.musterilers.Where(xd => xd.odano.Contains(odano));
                    dataGridView6.DataSource = ctx.misafir1s.Where(xxd => xxd.geldigioda.Contains(odano));

                    if (dataGridView6.Rows[0].Cells[0].Value == null)
                    {
                        if (timersayac == 0)
                        {
                            timersayac++;
                            timer1.Interval = 500;
                            timer1.Start();
                            toolTip2.SetToolTip(al, "Tc Kimlik No : " + dataGridView5.Rows[0].Cells[1].Value.ToString() +
                            "\nAdı : " + dataGridView5.Rows[0].Cells[2].Value.ToString() + "\nSoyadı: " + dataGridView5.Rows[0].Cells[3].Value.ToString() +
                            "\nCep Telefonu : " + dataGridView5.Rows[0].Cells[4].Value.ToString() + "\nGiriş Tarihi : " + dataGridView5.Rows[0].Cells[5].Value.ToString() +
                            "\nÇıkış Tarihi : " + dataGridView5.Rows[0].Cells[6].Value.ToString() + "\nAnahtarı : " + dataGridView5.Rows[0].Cells[13].Value.ToString());
                            toolTip2.ToolTipTitle = "Oda Bilgileri";
                            toolTip2.ToolTipIcon = ToolTipIcon.Info;

                            toolTip2.IsBalloon = true;
                        }
                    }
                    else
                    {

                        if (timersayac == 0)
                        {
                            timersayac++;
                            timer1.Interval = 1500;
                            timer1.Start();


                            toolTip2.SetToolTip(al, "Tc Kimlik No : " + dataGridView5.Rows[0].Cells[1].Value.ToString() +
                            "\nAdı : " + dataGridView5.Rows[0].Cells[2].Value.ToString() + "\nSoyadı: " + dataGridView5.Rows[0].Cells[3].Value.ToString() +
                            "\nCep Telefonu : " + dataGridView5.Rows[0].Cells[4].Value.ToString() + "\nGiriş Tarihi : " + dataGridView5.Rows[0].Cells[5].Value.ToString() +
                            "\nÇıkış Tarihi : " + dataGridView5.Rows[0].Cells[6].Value.ToString() + "\nAnahtarı : " + dataGridView5.Rows[0].Cells[13].Value.ToString() + "\nMisafirleri\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[0].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[0].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[0].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[0].Cells[4].Value.ToString());
                            if (dataGridView6.Rows[1].Cells[1].Value.ToString() != "")
                            {

                                toolTip2.SetToolTip(al, "Tc Kimlik No : " + dataGridView5.Rows[0].Cells[1].Value.ToString() +
                            "\nAdı : " + dataGridView5.Rows[0].Cells[2].Value.ToString() + "\nSoyadı: " + dataGridView5.Rows[0].Cells[3].Value.ToString() +
                            "\nCep Telefonu : " + dataGridView5.Rows[0].Cells[4].Value.ToString() + "\nGiriş Tarihi : " + dataGridView5.Rows[0].Cells[5].Value.ToString() +
                            "\nÇıkış Tarihi : " + dataGridView5.Rows[0].Cells[6].Value.ToString() + "\nAnahtarı : " + dataGridView5.Rows[0].Cells[13].Value.ToString() + "\nMisafirleri\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[0].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[0].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[0].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[0].Cells[4].Value.ToString() +
                            "\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[1].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[1].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[1].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[1].Cells[4].Value.ToString());

                            }
                            if (dataGridView6.Rows[2].Cells[1].Value.ToString() != "")
                            {
                                toolTip2.SetToolTip(al, "Tc Kimlik No : " + dataGridView5.Rows[0].Cells[1].Value.ToString() +
                            "\nAdı : " + dataGridView5.Rows[0].Cells[2].Value.ToString() + "\nSoyadı: " + dataGridView5.Rows[0].Cells[3].Value.ToString() +
                            "\nCep Telefonu : " + dataGridView5.Rows[0].Cells[4].Value.ToString() + "\nGiriş Tarihi : " + dataGridView5.Rows[0].Cells[5].Value.ToString() +
                            "\nÇıkış Tarihi : " + dataGridView5.Rows[0].Cells[6].Value.ToString() + "\nAnahtarı : " + dataGridView5.Rows[0].Cells[13].Value.ToString() + "\nMisafirleri\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[0].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[0].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[0].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[0].Cells[4].Value.ToString() +
                            "\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[1].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[1].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[1].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[1].Cells[4].Value.ToString() +
                                "\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[2].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[2].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[2].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[2].Cells[4].Value.ToString());
                            }
                            if (dataGridView6.Rows[3].Cells[1].Value.ToString() != "")
                            {
                                toolTip2.SetToolTip(al, "Tc Kimlik No : " + dataGridView5.Rows[0].Cells[1].Value.ToString() +
                            "\nAdı : " + dataGridView5.Rows[0].Cells[2].Value.ToString() + "\nSoyadı: " + dataGridView5.Rows[0].Cells[3].Value.ToString() +
                            "\nCep Telefonu : " + dataGridView5.Rows[0].Cells[4].Value.ToString() + "\nGiriş Tarihi : " + dataGridView5.Rows[0].Cells[5].Value.ToString() +
                            "\nÇıkış Tarihi : " + dataGridView5.Rows[0].Cells[6].Value.ToString() + "\nAnahtarı : " + dataGridView5.Rows[0].Cells[13].Value.ToString() + "\nMisafirleri\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[0].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[0].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[0].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[0].Cells[4].Value.ToString() +
                            "\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[1].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[1].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[1].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[1].Cells[4].Value.ToString() +
                                "\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[2].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[2].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[2].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[2].Cells[4].Value.ToString() +
                                "\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[3].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[3].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[3].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[3].Cells[4].Value.ToString());
                            }
                            if (dataGridView6.Rows[4].Cells[1].Value.ToString() != "")
                            {
                                toolTip2.SetToolTip(al, "Tc Kimlik No : " + dataGridView5.Rows[0].Cells[1].Value.ToString() +
                            "\nAdı : " + dataGridView5.Rows[0].Cells[2].Value.ToString() + "\nSoyadı: " + dataGridView5.Rows[0].Cells[3].Value.ToString() +
                            "\nCep Telefonu : " + dataGridView5.Rows[0].Cells[4].Value.ToString() + "\nGiriş Tarihi : " + dataGridView5.Rows[0].Cells[5].Value.ToString() +
                            "\nÇıkış Tarihi : " + dataGridView5.Rows[0].Cells[6].Value.ToString() + "\nAnahtarı : " + dataGridView5.Rows[0].Cells[13].Value.ToString() + "\nMisafirleri\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[0].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[0].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[0].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[0].Cells[4].Value.ToString() +
                            "\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[1].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[1].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[1].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[1].Cells[4].Value.ToString() +
                                "\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[2].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[2].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[2].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[2].Cells[4].Value.ToString() +
                                "\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[3].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[3].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[3].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[3].Cells[4].Value.ToString() +
                                "\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[4].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[4].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[4].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[4].Cells[4].Value.ToString());
                            }
                            if (dataGridView6.Rows[5].Cells[1].Value.ToString() != "")
                            {
                                toolTip2.SetToolTip(al, "Tc Kimlik No : " + dataGridView5.Rows[0].Cells[1].Value.ToString() +
                            "\nAdı : " + dataGridView5.Rows[0].Cells[2].Value.ToString() + "\nSoyadı: " + dataGridView5.Rows[0].Cells[3].Value.ToString() +
                            "\nCep Telefonu : " + dataGridView5.Rows[0].Cells[4].Value.ToString() + "\nGiriş Tarihi : " + dataGridView5.Rows[0].Cells[5].Value.ToString() +
                            "\nÇıkış Tarihi : " + dataGridView5.Rows[0].Cells[6].Value.ToString() + "\nAnahtarı : " + dataGridView5.Rows[0].Cells[13].Value.ToString() + "\nMisafirleri\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[0].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[0].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[0].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[0].Cells[4].Value.ToString() +
                            "\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[1].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[1].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[1].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[1].Cells[4].Value.ToString() +
                                "\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[2].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[2].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[2].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[2].Cells[4].Value.ToString() +
                                "\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[3].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[3].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[3].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[3].Cells[4].Value.ToString() +
                                "\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[4].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[4].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[4].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[4].Cells[4].Value.ToString() +
                                 "\n---------------------\nGeldiği Oda: " + dataGridView6.Rows[5].Cells[1].Value.ToString() + " İsim: " + dataGridView6.Rows[5].Cells[2].Value.ToString() + " Soyisim : " + dataGridView6.Rows[5].Cells[3].Value.ToString() + " Cep Telefonu: " + dataGridView6.Rows[5].Cells[4].Value.ToString());
                            }
                        }


                    }

                    toolTip2.ToolTipTitle = "Oda Bilgileri";
                    toolTip2.ToolTipIcon = ToolTipIcon.Info;

                    toolTip2.IsBalloon = true;
                }
                else
                    toolTip2.RemoveAll();

            }
            catch (Exception)
            {
            }

        }
        private void baloncuk(Control al, string Oddano)
        {
            string odano = Oddano;
            dataGridView5.DataSource = ctx.odadurumus.Where(xd => xd.odanoo.Contains(odano));
            if (dataGridView5.Rows[0].Cells[1].Value.ToString() == "rezerve")
            {
                try
                {
                    dataGridView5.DataSource = ctx.rezervasyon1s.Where(cd => cd.oda__no.Contains(odano));
                    toolTip1.SetToolTip(al, "Tc Kimlik no : " + dataGridView5.Rows[0].Cells[1].Value.ToString() + "\n" +
                        "Adı : " + dataGridView5.Rows[0].Cells[2].Value.ToString() + "\nSoyadı : " + dataGridView5.Rows[0].Cells[3].Value.ToString() + "\n" +
                        "Cep Telefonu : " + dataGridView5.Rows[0].Cells[6].Value.ToString() + "\nGiriş Tarihi : " + dataGridView5.Rows[0].Cells[4].Value.ToString() +
                        "\nÇıkış Tarihi : " + dataGridView5.Rows[0].Cells[5].Value.ToString());
                    toolTip1.ToolTipTitle = "Rezervasyon Bilgileri";
                    toolTip1.ToolTipIcon = ToolTipIcon.Info;
                    toolTip1.IsBalloon = true;
                }
                catch (Exception)
                {
                }
            }
            else
            {
                toolTip1.RemoveAll();
            }
        }
        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button1;
            string odano = this.button1.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);

        }
        Control mouse;
        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button2;
            string odano = this.button2.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button3_MouseMove(object sender, MouseEventArgs e)
        {

            mouse = this.button3;
            string odano = this.button3.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button71_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button71;
            string odano = this.button71.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button5_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button5;
            string odano = this.button5.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button6_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button6;
            string odano = this.button6.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button7_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button7;
            string odano = this.button7.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button8_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button8;
            string odano = this.button8.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button9_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button9;
            string odano = this.button9.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button10_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button10;
            string odano = this.button10.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button20_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button20;
            string odano = this.button20.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button18_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button18;
            string odano = this.button18.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button19_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button19;
            string odano = this.button19.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button17_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button17;
            string odano = this.button17.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button16_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button16;
            string odano = this.button16.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button15_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button15;
            string odano = this.button15.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button14_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button14;
            string odano = this.button14.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button13_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button13;
            string odano = this.button13.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button12_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button12;
            string odano = this.button12.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button11_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button11;
            string odano = this.button11.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button30_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button30;
            string odano = this.button30.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button28_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button28;
            string odano = this.button28.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button29_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button29;
            string odano = this.button29.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button27_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button27;
            string odano = this.button27.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button26_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button26;
            string odano = this.button26.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button25_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button25;
            string odano = this.button25.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button24_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button24;
            string odano = this.button24.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button23_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button23;
            string odano = this.button23.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button22_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button22;
            string odano = this.button22.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button21_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button21;
            string odano = this.button21.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button60_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button60;
            string odano = this.button60.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button58_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button58;
            string odano = this.button58.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button59_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button59;
            string odano = this.button59.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button57_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button57;
            string odano = this.button57.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button56_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button56;
            string odano = this.button56.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button55_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button55;
            string odano = this.button55.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button54_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button54;
            string odano = this.button54.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button53_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button53;
            string odano = this.button53.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button52_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button52;
            string odano = this.button52.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button51_MouseMove(object sender, MouseEventArgs e)
        {
            mouse = this.button51;
            string odano = this.button51.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
        }

        private void button18_Move(object sender, EventArgs e)
        {
            mouse = this.button18;
            string odano = this.button18.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button1_Move(object sender, EventArgs e)
        {
            mouse = this.button1;
            string odano = this.button1.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button2_Move(object sender, EventArgs e)
        {
            mouse = this.button2;
            string odano = this.button2.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button3_Move(object sender, EventArgs e)
        {
            mouse = this.button3;
            string odano = this.button3.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button71_Move(object sender, EventArgs e)
        {
            mouse = this.button71;
            string odano = this.button71.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button5_Move(object sender, EventArgs e)
        {
            mouse = this.button5;
            string odano = this.button5.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button6_Move(object sender, EventArgs e)
        {
            mouse = this.button6;
            string odano = this.button6.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button7_Move(object sender, EventArgs e)
        {
            mouse = this.button7;
            string odano = this.button7.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button8_Move(object sender, EventArgs e)
        {
            mouse = this.button8;
            string odano = this.button8.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button9_Move(object sender, EventArgs e)
        {
            mouse = this.button9;
            string odano = this.button9.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button10_Move(object sender, EventArgs e)
        {
            mouse = this.button10;
            string odano = this.button10.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button20_Move(object sender, EventArgs e)
        {
            mouse = this.button20;
            string odano = this.button20.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button19_Move(object sender, EventArgs e)
        {
            mouse = this.button19;
            string odano = this.button19.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button17_Move(object sender, EventArgs e)
        {
            mouse = this.button17;
            string odano = this.button17.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button16_Move(object sender, EventArgs e)
        {
            mouse = this.button16;
            string odano = this.button16.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button15_Move(object sender, EventArgs e)
        {
            mouse = this.button15;
            string odano = this.button15.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button14_Move(object sender, EventArgs e)
        {
            mouse = this.button14;
            string odano = this.button14.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button13_Move(object sender, EventArgs e)
        {
            mouse = this.button13;
            string odano = this.button13.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button12_Move(object sender, EventArgs e)
        {
            mouse = this.button12;
            string odano = this.button12.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button11_Move(object sender, EventArgs e)
        {
            mouse = this.button11;
            string odano = this.button11.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button30_Move(object sender, EventArgs e)
        {
            mouse = this.button30;
            string odano = this.button30.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button28_Move(object sender, EventArgs e)
        {
            mouse = this.button28;
            string odano = this.button28.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button29_Move(object sender, EventArgs e)
        {
            mouse = this.button29;
            string odano = this.button29.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button27_Move(object sender, EventArgs e)
        {
            mouse = this.button27;
            string odano = this.button27.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button26_Move(object sender, EventArgs e)
        {
            mouse = this.button26;
            string odano = this.button26.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button25_Move(object sender, EventArgs e)
        {
            mouse = this.button25;
            string odano = this.button25.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button24_Move(object sender, EventArgs e)
        {
            mouse = this.button24;
            string odano = this.button24.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button23_Move(object sender, EventArgs e)
        {
            mouse = this.button23;
            string odano = this.button23.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button22_Move(object sender, EventArgs e)
        {
            mouse = this.button22;
            string odano = this.button22.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button21_Move(object sender, EventArgs e)
        {
            mouse = this.button21;
            string odano = this.button21.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button60_Move(object sender, EventArgs e)
        {
            mouse = this.button60;
            string odano = this.button60.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button58_Move(object sender, EventArgs e)
        {
            mouse = this.button58;
            string odano = this.button58.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button59_Move(object sender, EventArgs e)
        {
            mouse = this.button59;
            string odano = this.button59.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button57_Move(object sender, EventArgs e)
        {
            mouse = this.button57;
            string odano = this.button57.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button56_Move(object sender, EventArgs e)
        {
            mouse = this.button56;
            string odano = this.button56.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button55_Move(object sender, EventArgs e)
        {
            mouse = this.button55;
            string odano = this.button55.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button54_Move(object sender, EventArgs e)
        {
            mouse = this.button54;
            string odano = this.button54.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button53_Move(object sender, EventArgs e)
        {
            mouse = this.button53;
            string odano = this.button53.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button52_Move(object sender, EventArgs e)
        {
            mouse = this.button52;
            string odano = this.button52.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button51_Move(object sender, EventArgs e)
        {
            mouse = this.button51;
            string odano = this.button51.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void button23_Move_1(object sender, EventArgs e)
        {
            mouse = this.button23;
            string odano = this.button23.Text.Substring(3).Trim();
            baloncuk(mouse, odano); baloncuk2(mouse, odano);
            baloncuk2(mouse, odano);
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value < dateTimePicker1.Value)
                dateTimePicker2.Value = DateTime.Now;
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button19_MouseEnter(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            timer1.Stop();
            timersayac = 0;

        }

        private void textBox9_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip3.SetToolTip(textBox9, "Lütfen tutarı kendiniz giriniz.!");
            toolTip3.ToolTipIcon = ToolTipIcon.Info;

            toolTip3.IsBalloon = true;
        }
    }
}
