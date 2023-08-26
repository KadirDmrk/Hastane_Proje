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
using System.Net.Http.Headers;

namespace Hastane_Proje
{
    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;
            // Ad Soyad Çekme
            SqlCommand komut = new SqlCommand("Select HastaAd,HastaSoyad From Tbl_Hastalar where HastaTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr= komut.ExecuteReader();
            while(dr.Read())
            {
                lblAdSoyad.Text = dr[0] + " "+ dr[1]; // Bak burada string bır ıfadeye cevırmemız gerekıyor fakat aralarına bosluk koydugum ııcn ototmatık strıng ıfadeye donusuyor.
            }
            bgl.baglanti().Close();

            // Randevu Geçmişi 
            DataTable dt = new DataTable(); //sanal tablo oluşturma 
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular where HastaTC= " + tc, bgl.baglanti());
            da.Fill(dt); // Dataadapterın içini doldur
            dataGridView1.DataSource = dt; // kaynak da dt den gelen tablo burada datagiridde bağlantyı open close demeye gerek yok 

            // Branşalrı Çekme 
            SqlCommand komut2=new SqlCommand("Select BransAd From Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while( dr2.Read())
            {
                cbxbrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void cbxbrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxdoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("Select DoktorAd,DoktorSoyad From Tbl_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", cbxbrans.Text);
            SqlDataReader dr3=komut3.ExecuteReader();
            while(dr3.Read())
            {
                cbxdoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void cbxdoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Randevu İşlemi 
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Tbl_Randevular where RandevuBrans='" + cbxbrans.Text + "'" + "and RandevuDoktor='"+cbxdoktor.Text + "' and RandevuDurum=0", bgl.baglanti());// tek tırnak amacı kelıme bazlı aramlarda yazıcagınız kelımeyı  tek tırnak ıcıne yazmamız gerekıyor.
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void lnkbilgileriniguncelle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiduzenle fr=new FrmBilgiduzenle();
            fr.TCno = lblTC.Text;
           
            fr.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Update Tbl_Randevular set RandevuDurum=1,HastaTC=@p1,HastaSikayet=@p2 where Randevuid=@p3", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            komut.Parameters.AddWithValue("@p2", richsikayet.Text);
            komut.Parameters.AddWithValue("@p3", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);

        }
    }
}
