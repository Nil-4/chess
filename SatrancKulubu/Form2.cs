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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-4KSUEJI\\SQLEXPRESS;Initial Catalog=satranc;Integrated Security=True");


        private void Form2_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'satrancDataSet.satranc' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.satrancTableAdapter.Fill(this.satrancDataSet.satranc);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBox1.Text);

                // Veritabanı bağlantısını aç
                using (SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-4KSUEJI\\SQLEXPRESS;Initial Catalog=satranc;Integrated Security=True"))
                {
                    baglanti.Open();

                    // SQL DELETE sorgusu
                    string query = "DELETE FROM satranc WHERE id = @id";

                    // SqlCommand nesnesini oluştur
                    using (SqlCommand komut = new SqlCommand(query, baglanti))
                    {
                        // Parametre ekle
                        komut.Parameters.AddWithValue("@id", id);

                        // Sorguyu çalıştır ve veriyi sil
                        komut.ExecuteNonQuery();

                        MessageBox.Show("Veri silindi.");
                    }

                    baglanti.Close();
                    textBox1.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Lütfen güncellemek için bir ID girin.");
                return;
            }

            int id = Convert.ToInt32(textBox2.Text);
            string ad = textBox3.Text;
            string soyad = textBox4.Text;
            string sifre = textBox5.Text;

            try
            {
                using (SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-4KSUEJI\\SQLEXPRESS;Initial Catalog=satranc;Integrated Security=True"))
                {
                    baglanti.Open();

                    string query = "UPDATE satranc SET ad = @ad, soyad = @soyad, sifre = @sifre WHERE id = @id";

                    using (SqlCommand komut = new SqlCommand(query, baglanti))
                    {
                        komut.Parameters.AddWithValue("@ad", ad);
                        komut.Parameters.AddWithValue("@soyad", soyad);
                        komut.Parameters.AddWithValue("@sifre", sifre);
                        komut.Parameters.AddWithValue("@id", id);

                        int etkilenenSatirSayisi = komut.ExecuteNonQuery();

                        if (etkilenenSatirSayisi > 0)
                        {
                            MessageBox.Show("Güncelleme işlemi tamamlandı.");
                        }
                        else
                        {
                            MessageBox.Show("Güncelleme işlemi başarısız oldu. Belirtilen ID bulunamadı.");
                        }
                    }

                    baglanti.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                baglanti.Open();

                string ad = textBox8.Text;
                string soyad = textBox7.Text;
                string sifre = textBox6.Text;

                if (string.IsNullOrEmpty(ad) || string.IsNullOrEmpty(soyad) || string.IsNullOrEmpty(sifre))
                {
                    MessageBox.Show("Ad, soyad ve şifre boş bırakılamaz.");
                    return;
                }

                string query = "INSERT INTO satranc (Ad, Soyad, Sifre) VALUES (@Ad, @Soyad, @Sifre)";

                SqlCommand komut = new SqlCommand(query, baglanti);
                komut.Parameters.AddWithValue("@Ad", ad);
                komut.Parameters.AddWithValue("@Soyad", soyad);
                komut.Parameters.AddWithValue("@Sifre", sifre);

                komut.ExecuteNonQuery();

                MessageBox.Show("Kayıt eklendi.");
                

                baglanti.Close();

                textBox8.Text = "";
                textBox7.Text = "";
                textBox6.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-4KSUEJI\\SQLEXPRESS;Initial Catalog=satranc;Integrated Security=True"))
                {
                    string query = "SELECT * FROM satranc";

                    using (SqlCommand komut = new SqlCommand(query, baglanti))
                    {
                        baglanti.Open();

                        using (SqlDataReader reader = komut.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                DataTable dataTable = new DataTable();
                                dataTable.Load(reader);

                                dataGridView1.DataSource = dataTable;
                            }
                            else
                            {
                                MessageBox.Show("Kayıt bulunamadı.");
                            }
                        }

                        baglanti.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}
