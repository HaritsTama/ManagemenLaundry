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
    public partial class TambahPelangganForm: Form
    {
        private string connectionString = "Data Source= LAPTOP-RFI0KF85\\HARITSZHAFRAN ;Initial Catalog=SistemManajemenLaundry;Integrated Security=True";
        private int selectedID = -1;

        public TambahPelangganForm()
        {
            InitializeComponent();
        }

        private void TambahPelangganForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ClearForm()
        {
            txtNPG.Clear();
            txtTPG.Clear();
            txtEPG.Clear();
            selectedID = -1;

            txtNPG.Focus();
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtNPG.Text) ||
                string.IsNullOrWhiteSpace(txtTPG.Text) ||
                string.IsNullOrWhiteSpace(txtEPG.Text))
            {
                MessageBox.Show("Harap isi semua data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ID_Pelanggan, Nama, NoTelp, Email FROM Pelanggan";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvPelanggan.DataSource = dt;
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat load data: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            if (!ValidateForm()) return;

            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menambahkan data baru?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Pelanggan (Nama, NoTelp, Email) VALUES (@Nama, @NoTelp, @Email)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nama", txtNPG.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoTelp", txtTPG.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEPG.Text.Trim());

                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show(rowsAffected > 0 ? "Data berhasil ditambahkan!" : "Data gagal ditambahkan!", 
                            rowsAffected > 0 ? "Sukses" : "Kesalahan", 
                            MessageBoxButtons.OK, 
                            rowsAffected > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                        if (rowsAffected > 0)
                        {
                            LoadData();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat tambah data: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (selectedID == -1)
            {
                MessageBox.Show("Silakan pilih data yang ingin dihapus.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Pelanggan WHERE ID_Pelanggan = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", selectedID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show(rowsAffected > 0 ? "Data berhasil dihapus!" : "Data gagal dihapus!",
                            rowsAffected > 0 ? "Sukses" : "Kesalahan",
                            MessageBoxButtons.OK,
                            rowsAffected > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                        if (rowsAffected > 0)
                            LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat hapus data: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (selectedID == -1 || !ValidateForm())
            {
                MessageBox.Show("Silakan pilih data yang ingin diubah.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Apakah Anda yakin ingin mengubah data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Pelanggan SET Nama = @Nama, NoTelp = @NoTelp, Email = @Email WHERE ID_Pelanggan = @ID";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nama", txtNPG.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoTelp", txtTPG.Text.Trim());
                        cmd.Parameters.AddWithValue("@Email", txtEPG.Text.Trim());
                        cmd.Parameters.AddWithValue("@ID", selectedID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show(rowsAffected > 0 ? "Data berhasil diperbarui!" : "Data gagal diperbarui!",
                            rowsAffected > 0 ? "Sukses" : "Kesalahan",
                            MessageBoxButtons.OK,
                            rowsAffected > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error);

                        if (rowsAffected > 0)
                            LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saat update data: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
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
            MessageBox.Show($"Jumlah Kolom: {dgvPelanggan.ColumnCount}\nJumlah Baris: {dgvPelanggan.RowCount}",
                "Info Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvPelanggan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPelanggan.Rows[e.RowIndex];
                selectedID = Convert.ToInt32(row.Cells["ID_Pelanggan"].Value);
                txtNPG.Text = row.Cells["Nama"].Value?.ToString();
                txtTPG.Text = row.Cells["NoTelp"].Value?.ToString();
                txtEPG.Text = row.Cells["Email"].Value?.ToString();
            }
        }
    }
}
