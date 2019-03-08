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
    public partial class MusteriSiparisDurumu : Form
    {
        public MusteriSiparisDurumu()
        {
            InitializeComponent();
        }
        oteltakipDataContext ctx = new oteltakipDataContext();
        void griddoldur()
        {
            try
            {
                dataGridView2.DataSource = ctx.siparistablosus.Where(musstc => musstc.mustc.Contains(label1.Text.Trim()));
                this.dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                
                
                
            }
            catch (Exception)
            {
                MessageBox.Show("Birden fazla siparişiniz bulunmaktadır.");
            }
            
        }
        private void MusteriSiparisDurumu_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            griddoldur();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
