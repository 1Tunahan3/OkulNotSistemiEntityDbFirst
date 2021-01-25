using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace YoutubeEntityDbFirst
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbSınavOgrenciEntities entities = new DbSınavOgrenciEntities();

        private void btnDersListesi_Click(object sender, EventArgs e)
        {
            DbSınavOgrenciEntities entities=new DbSınavOgrenciEntities();
            dataGridView1.DataSource = entities.TBLDERSLER.ToList();
        }

        private void btnOgrenciListele_Click(object sender, EventArgs e)
        {
           
            dataGridView1.DataSource = entities.TBLOGRENCI.ToList();
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }

        private void btnNotListeleme_Click(object sender, EventArgs e)
        {
            var query = from item in entities.TBLNOTLAR
                select new
                {
                    item.NOTID, item.OGR, item.DERS, item.SINAV1, item.SINAV2, item.SINAV3, item.ORTALAMA, item.DURUM
                };
            dataGridView1.DataSource = query.ToList();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            TBLOGRENCI t=new TBLOGRENCI();
            t.AD = txtOgrenciAd.Text;
            t.SOYAD = txtOgrenciSoyad.Text;
            entities.TBLOGRENCI.Add(t);
            entities.SaveChanges();
            MessageBox.Show("Eklendi");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);
            var x = entities.TBLOGRENCI.Find(id);
            entities.TBLOGRENCI.Remove(x);
            entities.SaveChanges();
            MessageBox.Show("Silindi");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtId.Text);
            var x = entities.TBLOGRENCI.Find(id);
            x.AD = txtOgrenciAd.Text;
            x.SOYAD = txtOgrenciSoyad.Text;
            x.FOTOGRAF = txtFotograf.Text;
            entities.SaveChanges();
            MessageBox.Show("Güncellendi");
        }

        private void btnProcedure_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = entities.NOTLISTESI();
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource=entities.TBLOGRENCI.Where(x=>x.AD==txtOgrenciAd.Text | x.SOYAD==txtOgrenciSoyad.Text).ToList();
        }

        private void txtOgrenciAd_TextChanged(object sender, EventArgs e)
        {
            string aranan = txtOgrenciAd.Text;
            var degerler = from item in entities.TBLOGRENCI
                where item.AD.Contains(aranan)
                select item;
            dataGridView1.DataSource = degerler.ToList();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                List<TBLOGRENCI> list = entities.TBLOGRENCI.OrderBy(p => p.AD).ToList();
                dataGridView1.DataSource = list;
            }

            if (radioButton2.Checked == true)
            {
                List<TBLOGRENCI> list2 = entities.TBLOGRENCI.OrderByDescending(p => p.AD).ToList();
                dataGridView1.DataSource = list2;
            }

            if (radioButton3.Checked == true)
            {
                List<TBLOGRENCI> list3 = entities.TBLOGRENCI.OrderBy(p => p.AD).Take(3).ToList();
                dataGridView1.DataSource = list3;
            }

            if (radioButton4.Checked==true)
            {
                List<TBLOGRENCI> list = entities.TBLOGRENCI.Where(p => p.AD.StartsWith("a")).ToList();
                dataGridView1.DataSource = list;
            }

            if (radioButton5.Checked==true)
            {
                List<TBLOGRENCI> list = entities.TBLOGRENCI.Where(p => p.AD.EndsWith("e")).ToList();
                dataGridView1.DataSource = list;
            }

            if (radioButton6.Checked==true)
            {
                var ortalama = entities.TBLNOTLAR.Average(p => p.SINAV1);
                List<TBLNOTLAR> list12 = entities.TBLNOTLAR.Where(p=>p.SINAV1 > ortalama).ToList();
                dataGridView1.DataSource = list12;
            }
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            //var top = entities.TBLNOTLAR.Max(p => p.SINAV1);
            //List<TBLNOTLAR> list13 = entities.TBLNOTLAR.Where(p => p.SINAV1 == top).ToList();
            //var list14 = entities.TBLOGRENCI.Where(p => p.ID = list13).ToList();
            //dataGridView1.DataSource = list13;
        }
    }
}
