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
    public partial class MasukanPesananForm: Form
    {
        private string connectionString = "Data Source= LAPTOP-RFI0KF85\\HARITSZHAFRAN ;Initial Catalog=SistemManajemenLaundry;Integrated Security=True";

        public MasukanPesananForm()
        {
            InitializeComponent();
        }

        private void MasukanPesananForm_Load(object sender, EventArgs e)
        {
            LoadPelanggan();
            LoadLayanan();
            LoadData();
        }
        private void ClearForm()
        {
            plhPelanggan.SelectedIndex = -1;
            dtpMasuk.Value = DateTime.Now; 
            plhStatus.SelectedIndex = -1;
            plhLayanan.SelectedIndex = -1;
            txtBerat.Clear();
            txtBreks.Clear();
            txtTotal.Clear();

            plhPelanggan.Focus();
        }
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            p.ID_Pesanan,
                            p.ID_Pelanggan,
                            p.Tanggal_Masuk,
                            p.Status_Laundry,
                            p.Berat,
                            p.Total_Ekstra,
                            p.Total_Harga,
                            l.Nama_Layanan
                        FROM Pesanan p
                        INNER JOIN Pelanggan pel ON p.ID_Pelanggan = pel.ID_Pelanggan
                        LEFT JOIN Layanan l ON p.ID_Layanan = l.ID_Layanan
                    ";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvPesanan.AutoGenerateColumns = true;
                    dgvPesanan.DataSource = dt;

                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTambahP_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    if (plhPelanggan.SelectedIndex == -1 || plhStatus.SelectedIndex == -1 ||
                        plhLayanan.SelectedIndex == -1 || string.IsNullOrWhiteSpace(txtBerat.Text))
                    {
                        MessageBox.Show("Harap isi semua data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string idPelanggan = plhPelanggan.SelectedValue.ToString();
                    string idLayanan = plhLayanan.SelectedValue.ToString();
                    string status = plhStatus.SelectedItem.ToString();
                    DateTime tanggalMasuk = dtpMasuk.Value;
                    float berat = float.Parse(txtBerat.Text);
                    decimal totalEkstra = string.IsNullOrWhiteSpace(txtBreks.Text) ? 0 : decimal.Parse(txtBreks.Text);

                    conn.Open();

                    // Ambil harga layanan
                    string hargaQuery = "SELECT L_Harga FROM Layanan WHERE ID_Layanan = @ID_Layanan";
                    SqlCommand hargaCmd = new SqlCommand(hargaQuery, conn);
                    hargaCmd.Parameters.AddWithValue("@ID_Layanan", idLayanan);
                    decimal hargaLayanan = (decimal)hargaCmd.ExecuteScalar();

                    decimal totalHarga = (decimal)berat * hargaLayanan + totalEkstra;

                    string insertQuery = @"
                INSERT INTO Pesanan (ID_Pelanggan, ID_Layanan, Tanggal_Masuk, Status_Laundry, Berat, Total_Ekstra, Total_Harga)
                VALUES (@ID_Pelanggan, @ID_Layanan, @Tanggal_Masuk, @Status_Laundry, @Berat, @Total_Ekstra, @Total_Harga)";

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_Pelanggan", idPelanggan);
                        cmd.Parameters.AddWithValue("@ID_Layanan", idLayanan);
                        cmd.Parameters.AddWithValue("@Tanggal_Masuk", tanggalMasuk);
                        cmd.Parameters.AddWithValue("@Status_Laundry", status);
                        cmd.Parameters.AddWithValue("@Berat", berat);
                        cmd.Parameters.AddWithValue("@Total_Ekstra", totalEkstra);
                        cmd.Parameters.AddWithValue("@Total_Harga", totalHarga);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Pesanan berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Pesanan gagal ditambahkan!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHapusP_Click(object sender, EventArgs e)
        {
            if (dgvPesanan.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Yakin ingin menghapus pesanan ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            int idPesanan = Convert.ToInt32(dgvPesanan.SelectedRows[0].Cells["ID_Pesanan"].Value);
                            conn.Open();
                            string query = "DELETE FROM Pesanan WHERE ID_Pesanan = @ID_Pesanan";

                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@ID_Pesanan", idPesanan);
                                int rows = cmd.ExecuteNonQuery();
                                if (rows > 0)
                                {
                                    MessageBox.Show("Pesanan berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadData();
                                    ClearForm();
                                }
                                else
                                {
                                    MessageBox.Show("Pesanan gagal dihapus!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih pesanan yang ingin dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUbahP_Click(object sender, EventArgs e)
        {
            if (dgvPesanan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih pesanan yang ingin diperbarui!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    int idPesanan = Convert.ToInt32(dgvPesanan.SelectedRows[0].Cells["ID_Pesanan"].Value);
                    string idPelanggan = plhPelanggan.SelectedValue.ToString();
                    string idLayanan = plhLayanan.SelectedValue.ToString();
                    string status = plhStatus.SelectedItem.ToString();
                    DateTime tanggalMasuk = dtpMasuk.Value;
                    float berat = float.Parse(txtBerat.Text);
                    decimal totalEkstra = string.IsNullOrWhiteSpace(txtBreks.Text) ? 0 : decimal.Parse(txtBreks.Text);

                    conn.Open();
                    string hargaQuery = "SELECT L_Harga FROM Layanan WHERE ID_Layanan = @ID_Layanan";
                    SqlCommand hargaCmd = new SqlCommand(hargaQuery, conn);
                    hargaCmd.Parameters.AddWithValue("@ID_Layanan", idLayanan);
                    decimal hargaLayanan = (decimal)hargaCmd.ExecuteScalar();

                    decimal totalHarga = (decimal)berat * hargaLayanan + totalEkstra;

                    string query = @"
                UPDATE Pesanan 
                SET ID_Pelanggan = @ID_Pelanggan, ID_Layanan = @ID_Layanan, 
                    Tanggal_Masuk = @Tanggal_Masuk, Status_Laundry = @Status_Laundry, 
                    Berat = @Berat, Total_Ekstra = @Total_Ekstra, Total_Harga = @Total_Harga
                WHERE ID_Pesanan = @ID_Pesanan";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_Pelanggan", idPelanggan);
                        cmd.Parameters.AddWithValue("@ID_Layanan", idLayanan);
                        cmd.Parameters.AddWithValue("@Tanggal_Masuk", tanggalMasuk);
                        cmd.Parameters.AddWithValue("@Status_Laundry", status);
                        cmd.Parameters.AddWithValue("@Berat", berat);
                        cmd.Parameters.AddWithValue("@Total_Ekstra", totalEkstra);
                        cmd.Parameters.AddWithValue("@Total_Harga", totalHarga);
                        cmd.Parameters.AddWithValue("@ID_Pesanan", idPesanan);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Pesanan berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Gagal memperbarui pesanan!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefreshP_Click(object sender, EventArgs e)
        {
            LoadData();
            MessageBox.Show($"Jumlah Kolom: {dgvPesanan.ColumnCount}\nJumlah Baris: {dgvPesanan.RowCount}",
                "Debugging DataGridView", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvPesanan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPesanan.Rows[e.RowIndex];

                plhPelanggan.SelectedValue = row.Cells["ID_Pelanggan"].Value.ToString();
                plhLayanan.SelectedValue = row.Cells["ID_Layanan"].Value?.ToString();
                dtpMasuk.Value = Convert.ToDateTime(row.Cells["Tanggal_Masuk"].Value);
                plhStatus.SelectedItem = row.Cells["Status_Laundry"].Value.ToString();
                txtBerat.Text = row.Cells["Berat"].Value.ToString();
                txtBreks.Text = row.Cells["Total_Ekstra"].Value?.ToString();
                txtTotal.Text = row.Cells["Total_Harga"].Value?.ToString();
            }
        }

        private void LoadPelanggan()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT ID_Pelanggan, Nama FROM Pelanggan", conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    plhPelanggan.DataSource = dt;
                    plhPelanggan.DisplayMember = "Nama";  // Menggunakan Nama sebagai yang tampil
                    plhPelanggan.ValueMember = "ID_Pelanggan";  // Menggunakan ID_Pelanggan sebagai value
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data pelanggan: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadLayanan()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT ID_Layanan, Nama_Layanan FROM Layanan", conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    plhLayanan.DataSource = dt;
                    plhLayanan.DisplayMember = "Nama_Layanan";
                    plhLayanan.ValueMember = "ID_Layanan";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal memuat data layanan: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
