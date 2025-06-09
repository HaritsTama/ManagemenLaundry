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
    public partial class TambahLayananForm : Form
    {
        private string connectionString = "Data Source= LAPTOP-RFI0KF85\\HARITSZHAFRAN ;Initial Catalog=SistemManajemenLaundry;Integrated Security=True";

        public TambahLayananForm()
        {
            InitializeComponent();
        }

        private void TambahLayananForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ClearForm()
        {
            txtNLY.Clear();
            txtPLY.Clear();
            txtHLY.Clear();

            txtNLY.Focus();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ID_Layanan, Nama_Layanan, Panjang_Hari, L_Harga FROM Layanan";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvLayanan.AutoGenerateColumns = true;
                    dgvLayanan.DataSource = dt;

                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTambahL_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Apakah anda ingin menambahkan data baru?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            if (txtNLY.Text == "" || txtPLY.Text == "" || txtHLY.Text == "")
            {
                MessageBox.Show("Harap isi semua data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Layanan (Nama_Layanan, Panjang_Hari, L_Harga) VALUES (@Nama_Layanan, @Panjang_Hari, @L_Harga)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nama_Layanan", txtNLY.Text.Trim());
                        cmd.Parameters.AddWithValue("@Panjang_Hari", int.Parse(txtPLY.Text.Trim()));
                        cmd.Parameters.AddWithValue("@L_Harga", decimal.Parse(txtHLY.Text.Trim()));

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Data tidak berhasil ditambahkan!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHapusL_Click(object sender, EventArgs e)
        {

            if (dgvLayanan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Apakah anda ingin menghapus data yang dipilih?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            string idLayanan = dgvLayanan.SelectedRows[0].Cells["ID_Layanan"].Value.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Layanan WHERE ID_Layanan = @ID_Layanan";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_Layanan", idLayanan);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Data tidak ditemukan atau gagal dihapus!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUbahL_Click(object sender, EventArgs e)
        {
            if (dgvLayanan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang ingin diperbarui!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Apakah anda ingin mengubah data yang dipilih?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            string idLayanan = dgvLayanan.SelectedRows[0].Cells["ID_Layanan"].Value.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Layanan SET Nama_Layanan = @Nama_Layanan, Panjang_Hari = @Panjang_Hari, L_Harga = @L_Harga WHERE ID_Layanan = @ID_Layanan";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_Layanan", idLayanan);
                        cmd.Parameters.AddWithValue("@Nama_Layanan", txtNLY.Text.Trim());
                        cmd.Parameters.AddWithValue("@Panjang_Hari", int.Parse(txtPLY.Text.Trim()));
                        cmd.Parameters.AddWithValue("@L_Harga", decimal.Parse(txtHLY.Text.Trim()));

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Data berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Data tidak ditemukan atau gagal diperbarui!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefreshL_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Apakah anda ingin merefresh data?",
                "Konfirmasi",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                return;
            }

            LoadData();
            MessageBox.Show($"Jumlah Kolom: {dgvLayanan.ColumnCount}\nJumlah Baris: {dgvLayanan.RowCount}",
                "Debugging DataGridView", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvLayanan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvLayanan.Rows[e.RowIndex];

                // Kolom: ID_Layanan | Nama_Layanan | Panjang_Hari | L_Harga
                txtNLY.Text = row.Cells["Nama_Layanan"].Value?.ToString();
                txtPLY.Text = row.Cells["Panjang_Hari"].Value?.ToString();
                txtHLY.Text = row.Cells["L_Harga"].Value?.ToString();
            }
        }
    }

}
