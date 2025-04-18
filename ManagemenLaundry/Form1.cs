using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagemenLaundry
{
    public partial class Form1 : Form
    {
        string connectionString = "Data Source=localhost;Initial Catalog=SistemManajemenLaundry;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        // =================== PEGAWAI CRUD ===================

        private void AddPelanggan(string id, string nama, string noTelp, string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Pelanggan VALUES (@ID, @Nama, @NoTelp, @Email)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@Nama", nama);
                cmd.Parameters.AddWithValue("@NoTelp", noTelp);
                cmd.Parameters.AddWithValue("@Email", email);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Pelanggan berhasil ditambahkan.");
            }
        }

        private void UpdatePelanggan(string id, string nama, string noTelp, string email)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Pelanggan SET Nama=@Nama, NoTelp=@NoTelp, Email=@Email WHERE ID_Pelanggan=@ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id);
                cmd.Parameters.AddWithValue("@Nama", nama);
                cmd.Parameters.AddWithValue("@NoTelp", noTelp);
                cmd.Parameters.AddWithValue("@Email", email);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Pelanggan berhasil diperbarui.");
            }
        }

        private void DeletePelanggan(string id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Pelanggan WHERE ID_Pelanggan=@ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@ID", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                MessageBox.Show("Pelanggan berhasil dihapus.");
            }
        }

        private DataTable GetPelanggan()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Pelanggan";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }
            return dt;
        }

        // TODO: Tambahkan method CRUD untuk Pesanan, Layanan, Barang, dan Pembayaran
        // dengan struktur serupa (Add, Update, Delete, Get)
    }
}