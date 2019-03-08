using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace oteltakip
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        oteltakipDataContext ctx = new oteltakipDataContext();
        void griddoldur()
        {
            dataGridView1.DataSource = ctx.kullanicihesaplaris;
        }
        public void griddoldur2()
        {
            dataGridView2.DataSource = ctx.musterihesaplaris;
        }
        
        
        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        int timersayac = 2;
        
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView3.DataSource = ctx.rezervasyon1s;
            try
            {
                int sayac = 0;
                while (true)
                {
                   
                    if (Convert.ToDateTime(dataGridView3.Rows[sayac].Cells[4].Value.ToString()).Day < DateTime.Now.Day)
                    {
                        
                        string tc; string odano;
                        rezervasyon1 rz = ctx.rezervasyon1s.SingleOrDefault(cd => cd.oda__no== dataGridView3.Rows[sayac].Cells[7].Value.ToString());
                        tc = dataGridView3.Rows[sayac].Cells[1].Value.ToString();
                        odano = dataGridView3.Rows[sayac].Cells[7].Value.ToString();
                        ctx.rezervasyon1s.DeleteOnSubmit(rz);
                        ctx.SubmitChanges();
                        dataGridView3.DataSource= ctx.odadurumus.Where(xd => xd.odanoo.Contains( odano));
                        
                        dataGridView3.Rows[0].Cells[1].Value = "temiz boş";
                        ctx.SubmitChanges();
                        MessageBox.Show(tc +" Tc Kimlik numaralı rezervasyon giriş tarihini aksattığından dolayı silinmiştir.");
                        
                       

                        
                    }
                    sayac++;
                }

            }
            catch (Exception)
            {
            }
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            label16.Visible = false;
            dataGridView1.Visible = false;
            griddoldur();
            griddoldur2();
            label1.Visible = false; label3.Visible = false; label4.Visible = false; label5.Visible = false; label6.Visible = false;
            label2.Visible = false; label7.Visible = false; label8.Visible = false; label9.Visible = false; label10.Visible = false;
            label11.Visible = false; label12.Visible = false; label13.Visible = false; label14.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            button2.Visible = false;
           
            timer1.Start();
            timer1.Interval = 200;
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
        bool hangiradiobutton;
        int sayi = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            if (hangiradiobutton == true)
            {
                sayi = 0;
                if (textBox1.Text != string.Empty && textBox2.Text != string.Empty)
                {
                    while (true)
                    {
                        try
                        {
                            if (textBox1.Text == dataGridView1.Rows[sayi].Cells[0].Value.ToString() && textBox2.Text == dataGridView1.Rows[sayi].Cells[1].Value.ToString())
                            {
                                this.Visible = false;
                                
                                AnaMenu AnaMenu = new AnaMenu();
                                AnaMenu.label3.Text = dataGridView1.Rows[sayi].Cells[0].Value.ToString();
                                AnaMenu.Show();
                                break;
                            }
                            else
                            {
                                sayi++;
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Kullanıcı adı ve/veya şifre yanlıştır!");
                            break;
                        }
                    }
                }
                else
                    MessageBox.Show("Kullanıcı adı ve/veya şifre alanı boş bırakılamaz!");

            }
            else
            {
                sayi = 0;
                if (textBox1.Text != string.Empty && textBox2.Text != string.Empty)
                {
                    while (true)
                    {
                        try
                        {
                            if (textBox1.Text == dataGridView2.Rows[sayi].Cells[0].Value.ToString() && textBox2.Text == dataGridView2.Rows[sayi].Cells[1].Value.ToString())
                            {
                                this.Visible = false;
                                MusteriMenu AnaMenu = new MusteriMenu();
                                AnaMenu.label1.Text = dataGridView2.Rows[sayi].Cells[0].Value.ToString();
                                AnaMenu.Show();
                                break;
                            }
                            else
                            {
                                sayi++;
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Kullanıcı adı ve/veya şifre yanlıştır!");
                            break;
                        }
                    }
                }
                else
                    MessageBox.Show("Kullanıcı adı ve/veya şifre alanı boş bırakılamaz!");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if ( timersayac==2)
            {
                label4.Visible = !label4.Visible;
            }
            else if (timersayac == 3)
            {
                label5.Visible = !label5.Visible;
            }
            else if (timersayac == 4)
            {
                label6.Visible = !label6.Visible;
            }
            else if (timersayac == 5)
            {
                label7.Visible = !label7.Visible;
            }
            else if (timersayac == 6)
            {
                label8.Visible = !label8.Visible;
            }
            else if (timersayac == 7)
            {
                label9.Visible = !label9.Visible;
            }
            else if (timersayac == 8)
            {
                label10.Visible = !label10.Visible;
            }
            else if (timersayac == 9)
            {
                label11.Visible = !label11.Visible;
            }
            else if (timersayac == 10)
            {
                label12.Visible = !label12.Visible;
            }
            else if (timersayac == 11)
            {
                label13.Visible = !label13.Visible;
            }
            else if (timersayac == 12)
            {
                label14.Visible = !label14.Visible;
            }
            else
            {
                label4.Visible = false; label5.Visible = false; label6.Visible = false;
                label2.Visible = false; label7.Visible = false; label8.Visible = false; label9.Visible = false; label10.Visible = false;
                label11.Visible = false; label12.Visible = false; label13.Visible = false; label14.Visible = false;
                radioButton1.Visible = true;
                radioButton2.Visible = true;
                label16.Visible = true;
                timer1.Stop();
                timer2.Start();
                timer2.Interval = 50;
            }
            
            timersayac += 1;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
            label15.Text = DateTime.Now.ToString();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (button2.Visible == false)
            {
                textBox1.Clear();
                textBox2.Clear();
                label16.Visible = false;
                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox1.Focus();
                label1.Visible = true;
                label2.Visible = true;
                button2.Visible = true;
                hangiradiobutton = true;
                
            }
            else
            {
                hangiradiobutton = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
            if (button2.Visible == false)
            {
                MessageBox.Show(" Kullanıcı adınızı TC Kimlik no , şifrenizi telefon numaranız olarak giriniz.", "Bilgilendirme");
                textBox1.Clear();
                textBox2.Clear();

                textBox1.Visible = true;
                textBox2.Visible = true;
                textBox1.Focus();
                label1.Visible = true;
                label2.Visible = true;
                button2.Visible = true;
                label16.Visible = false;
                hangiradiobutton = false;
            }

            else
            {

                hangiradiobutton = false;
            }
        }

        private void radioButton2_EnabledChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            rezervasyon rzv = new rezervasyon();
            rzv.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
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
