using Org.BouncyCastle.Asn1.Cmp;
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
    public partial class PembayaranForm: Form
    {
        private string connectionString = "Data Source= LAPTOP-RFI0KF85\\HARITSZHAFRAN ;Initial Catalog=SistemManajemenLaundry;Integrated Security=True";
        private int selectedPesananId = -1;


        public PembayaranForm()
        {
            InitializeComponent();
        }

        private void PembayaranForm_Load(object sender, EventArgs e)
        {
            LoadComboBoxPesanan();
            LoadData();
            plhMtd.Items.AddRange(new string[] { "Transfer", "Tunai", "Qris" });
            plhSts.Items.AddRange(new string[] { "Belum Bayar", "Lunas" });
        }

        private void LoadComboBoxPesanan()
        {
            cmbPsn.Items.Clear();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT ID_Pesanan FROM Pesanan", conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cmbPsn.Items.Add(reader["ID_Pesanan"].ToString());
                }
            }
        }

        private void ClearForm()
        {
            cmbPsn.SelectedIndex = -1;
            plhMtd.SelectedIndex = -1;
            plhSts.SelectedIndex = -1;
        }


        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT ID_Pesanan, Total_Harga, Billing_Dibuat, Batas_Lunas FROM Pembayaran";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPembayaran.DataSource = dt;
            }
        }



        private decimal HitungTotalHarga(int idPesanan)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT p.Berat, l.L_Harga,
                           ISNULL(SUM(pb.Jumlah * b.Harga_Ekstra), 0) AS TotalEkstra
                    FROM Pesanan p
                    JOIN Layanan l ON p.ID_Layanan = l.ID_Layanan
                    LEFT JOIN PesananBarang pb ON p.ID_Pesanan = pb.ID_Pesanan
                    LEFT JOIN Barang b ON pb.ID_Barang = b.ID_Barang
                    WHERE p.ID_Pesanan = @ID_Pesanan
                    GROUP BY p.Berat, l.L_Harga";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Pesanan", idPesanan);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            decimal berat = Convert.ToDecimal(reader["Berat"]);
                            decimal hargaLayanan = Convert.ToDecimal(reader["L_Harga"]);
                            decimal totalEkstra = Convert.ToDecimal(reader["TotalEkstra"]);
                            return (berat * hargaLayanan) + totalEkstra;
                        }
                    }
                }
            }
            return 0;
        }

        private DateTime HitungBatasLunas(int idPesanan)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                    SELECT l.Panjang_Hari
                    FROM Pesanan p
                    JOIN Layanan l ON p.ID_Layanan = l.ID_Layanan
                    WHERE p.ID_Pesanan = @ID_Pesanan";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Pesanan", idPesanan);
                    int panjangHari = (int)cmd.ExecuteScalar();
                    return DateTime.Today.AddDays(panjangHari);
                }
            }
        }


        private void btnTambahPe_Click(object sender, EventArgs e)
        {
            if (cmbPsn.SelectedItem == null || plhMtd.SelectedItem == null || plhSts.SelectedItem == null)
            {
                MessageBox.Show("Harap lengkapi semua input!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idPesanan = int.Parse(cmbPsn.SelectedItem.ToString());
            decimal totalHarga = HitungTotalHarga(idPesanan);
            DateTime batasLunas = HitungBatasLunas(idPesanan);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"INSERT INTO Pembayaran (ID_Pesanan, Total_Harga, Metode_Pembayaran, Status_Pembayaran, Batas_Lunas)
                                 VALUES (@ID_Pesanan, @Total_Harga, @Metode_Pembayaran, @Status_Pembayaran, @Batas_Lunas)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Pesanan", idPesanan);
                    cmd.Parameters.AddWithValue("@Total_Harga", totalHarga);
                    cmd.Parameters.AddWithValue("@Metode_Pembayaran", plhMtd.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Status_Pembayaran", plhSts.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Batas_Lunas", batasLunas);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Data berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Gagal menambahkan data!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        

        private void btnHapusPe_Click(object sender, EventArgs e)
        {
            if (dgvPembayaran.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih baris untuk dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            int idPesanan = int.Parse(dgvPembayaran.SelectedRows[0].Cells["ID_Pesanan"].Value.ToString());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Pembayaran WHERE ID_Pesanan = @ID_Pesanan";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Pesanan", idPesanan);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Gagal menghapus data!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnUbahPe_Click(object sender, EventArgs e)
        {
            if (dgvPembayaran.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih baris untuk diubah!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idPesanan = int.Parse(dgvPembayaran.SelectedRows[0].Cells["ID_Pesanan"].Value.ToString());
            decimal totalHarga = HitungTotalHarga(idPesanan);
            DateTime batasLunas = HitungBatasLunas(idPesanan);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"UPDATE Pembayaran SET Total_Harga = @Total_Harga,
                                 Metode_Pembayaran = @Metode_Pembayaran,
                                 Status_Pembayaran = @Status_Pembayaran,
                                 Batas_Lunas = @Batas_Lunas
                                 WHERE ID_Pesanan = @ID_Pesanan";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_Pesanan", idPesanan);
                    cmd.Parameters.AddWithValue("@Total_Harga", totalHarga);
                    cmd.Parameters.AddWithValue("@Metode_Pembayaran", plhMtd.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Status_Pembayaran", plhSts.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@Batas_Lunas", batasLunas);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Data berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Gagal mengubah data!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnRefreshPe_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah anda ingin merefresh data?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            LoadData();
            LoadComboBoxPesanan();
            MessageBox.Show($"Jumlah Kolom: {dgvPembayaran.ColumnCount}\nJumlah Baris: {dgvPembayaran.RowCount}", "Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void dgvPembayaran_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvPembayaran.Rows[e.RowIndex];
            int id = (int)row.Cells["ID_Pesanan"].Value;

            int idPesanan = Convert.ToInt32(row.Cells["ID_Pesanan"].Value);
            cmbPsn.SelectedValue = idPesanan;
            plhMtd.Text = row.Cells["Metode_Pembayaran"].Value.ToString();
            plhSts.SelectedItem = row.Cells["Status_Pembayaran"].Value.ToString();
        }

        private void cmbPsn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
