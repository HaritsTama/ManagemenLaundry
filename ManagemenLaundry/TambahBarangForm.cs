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
    public partial class TambahBarangForm: Form
    {
        private string connectionString = "Data Source= LAPTOP-RFI0KF85\\HARITSZHAFRAN ;Initial Catalog=SistemManajemenLaundry;Integrated Security=True";

        public TambahBarangForm()
        {
            InitializeComponent();
        }

        private void TambahBarangForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void ClearForm()
        {
            txtNBR.Clear();
            txtHBR.Clear();
            txtNBR.Focus();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT ID_Barang, Ekstra_Barang, Harga_Ekstra FROM Barang";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvBarang.DataSource = dt;
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTambahB_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah anda ingin menambahkan data baru?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            if (txtNBR.Text == "" || txtHBR.Text == "")
            {
                MessageBox.Show("Harap isi semua data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Barang (Ekstra_Barang, Harga_Ekstra) VALUES (@Ekstra_Barang, @Harga_Ekstra)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Ekstra_Barang", txtNBR.Text.Trim());
                        cmd.Parameters.AddWithValue("@Harga_Ekstra", decimal.Parse(txtHBR.Text.Trim()));

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Data berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Gagal menambahkan data.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHapusB_Click(object sender, EventArgs e)
        {

            if (dgvBarang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang ingin dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            string id = dgvBarang.SelectedRows[0].Cells["ID_Barang"].Value.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Barang WHERE ID_Barang = @ID_Barang";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_Barang", id);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Data gagal dihapus!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUbahB_Click(object sender, EventArgs e)
        {
            if (dgvBarang.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang ingin diubah!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string id = dgvBarang.SelectedRows[0].Cells["ID_Barang"].Value.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Barang SET Ekstra_Barang = @Ekstra_Barang, Harga_Ekstra = @Harga_Ekstra WHERE ID_Barang = @ID_Barang";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Ekstra_Barang", txtNBR.Text.Trim());
                        cmd.Parameters.AddWithValue("@Harga_Ekstra", decimal.Parse(txtHBR.Text.Trim()));
                        cmd.Parameters.AddWithValue("@ID_Barang", id);

                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Data berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Data gagal diubah!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefreshB_Click(object sender, EventArgs e)
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
            MessageBox.Show($"Jumlah Kolom: {dgvBarang.ColumnCount}\nJumlah Baris: {dgvBarang.RowCount}",
                "Debugging DataGridView", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvBarang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvBarang.Rows[e.RowIndex];
                txtNBR.Text = row.Cells["Ekstra_Barang"].Value?.ToString();
                txtHBR.Text = row.Cells["Harga_Ekstra"].Value?.ToString();
            }
        }
    }
}
