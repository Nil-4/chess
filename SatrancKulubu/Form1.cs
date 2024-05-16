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


namespace SatrancKulubu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-4KSUEJI\\SQLEXPRESS;Initial Catalog=satranc;Integrated Security=True");


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();

                string ad = textBox1.Text;
                string soyad = textBox2.Text;
                string sifre = textBox3.Text;

                string query = "INSERT INTO satranc (Ad, Soyad, Sifre) VALUES (@Ad, @Soyad, @Sifre)";

                SqlCommand komut = new SqlCommand(query, baglanti);
                komut.Parameters.AddWithValue("@Ad", ad);
                komut.Parameters.AddWithValue("@Soyad", soyad);
                komut.Parameters.AddWithValue("@Sifre", sifre);

                komut.ExecuteNonQuery();

                MessageBox.Show("Kayıt eklendi.");
                Form2 frm2 = new Form2();
                frm2.Show();
                this.Hide();

                baglanti.Close();

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string ad = textBox4.Text;
                string soyad = textBox5.Text;
                string sifre = textBox6.Text;

                if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(soyad) || string.IsNullOrEmpty(sifre))
                {
                    MessageBox.Show("Ad, soyad ve şifre boş bırakılamaz.");
                    return;
                }

                SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-4KSUEJI\\SQLEXPRESS;Initial Catalog=satranc;Integrated Security=True");
                string query = "SELECT COUNT(*) FROM satranc WHERE ad = @ad AND soyad = @soyad AND sifre = @sifre";

                SqlCommand komut = new SqlCommand(query, baglanti);
                komut.Parameters.AddWithValue("@ad", ad);
                komut.Parameters.AddWithValue("@soyad", soyad);
                komut.Parameters.AddWithValue("@sifre", sifre);

                baglanti.Open();
                int kayitSayisi = (int)komut.ExecuteScalar();
                baglanti.Close();

                if (kayitSayisi > 0)
                {
                    MessageBox.Show("Giriş başarılı!");
                    

                    Form2 frm2 = new Form2();
                    frm2.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Geçersiz ad, soyad veya şifre!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}
    
//"Data Source=DESKTOP-4KSUEJI\\SQLEXPRESS;Initial Catalog=satranc;Integrated Security=True"