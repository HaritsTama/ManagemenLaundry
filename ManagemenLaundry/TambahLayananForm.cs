using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagemenLaundry
{
    public partial class TambahLayananForm : Form
    {
        private string connectionString = "Data Source= LAPTOP-RFI0KF85\\HARITSZHAFRAN ;Initial Catalog=SistemManajemenLaundry;Integrated Security=True";

        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly string _cacheKey = "LayananData";
        private readonly CacheItemPolicy _cachePolicy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };

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
        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtNLY.Text) ||
                string.IsNullOrWhiteSpace(txtPLY.Text) ||
                string.IsNullOrWhiteSpace(txtHLY.Text))
            {
                MessageBox.Show("Harap isi semua data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void LoadData()
        {
            try
            {
                DataTable cachedData = _cache.Get(_cacheKey) as DataTable;

                if (cachedData != null)
                {
                    dgvLayanan.DataSource = cachedData;
                    return;
                }

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_Layanan_GetAll", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Simpan ke cache
                        _cache.Set(_cacheKey, dt, _cachePolicy);

                        dgvLayanan.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTambahL_Click(object sender, EventArgs e)
        {
            string nama = txtNLY.Text;
            string waktuPengerjaan = txtPLY.Text;
            string Harga = txtHLY.Text;

            // Validasi input
            if (string.IsNullOrWhiteSpace(nama) || string.IsNullOrWhiteSpace(waktuPengerjaan) ||
                string.IsNullOrWhiteSpace(Harga))
            {
                MessageBox.Show("Harap isi semua data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi tanggal masuk
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNLY.Text, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Nama pelanggan tidak boleh mengandung karakter spesial atau angka.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ;


            // Konfirmasi sebelum simpan
            var konfirmasi = MessageBox.Show(
                $"Apakah Anda yakin ingin menyimpan data berikut?\n\n" +
                $"Nama_Layanan: {nama}\n" +
                $"Panjang_Hari: {waktuPengerjaan}\n" +
                $"L_Harga: {Harga}\n" +
                MessageBoxButtons.YesNo
            );

            if (konfirmasi == DialogResult.No)
            {
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();
                    try
                    {
                        SqlCommand cmd = new SqlCommand("usp_Layanan_Insert", con, transaction);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Nama_Layanan", nama);
                        cmd.Parameters.AddWithValue("@Panjang_Hari", waktuPengerjaan);
                        cmd.Parameters.AddWithValue("@L_Harga", Harga);


                        int result = cmd.ExecuteNonQuery();
                        transaction.Commit();


                        if (result > 0)
                        {
                            _cache.Remove(_cacheKey);
                            MessageBox.Show("Data berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Data gagal ditambahkan.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        MessageBox.Show("Terjadi kesalahan saat menyimpan data. Silakan coba lagi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnHapusL_Click(object sender, EventArgs e)
        {
            if (dgvLayanan.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvLayanan.CurrentRow.Cells["ID_Layanan"].Value);

                var confirm = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes) return;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();
                    try
                    {
                        SqlCommand cmd = new SqlCommand("usp_Layanan_Delete", con, transaction);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Layanan", id);
                        int result = cmd.ExecuteNonQuery();
                        transaction.Commit();
                        if (result > 0)
                        {
                            _cache.Remove(_cacheKey);
                            MessageBox.Show("Data berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Hapus gagal.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        MessageBox.Show("Terjadi kesalahan saat menghapus data. Silakan coba lagi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else
            {
                MessageBox.Show("Silakan pilih baris data yang ingin dihapus terlebih dahulu.", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUbahL_Click(object sender, EventArgs e)
        {
            if (dgvLayanan.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvLayanan.CurrentRow.Cells["ID_Layanan"].Value);
                string nama = txtNLY.Text;
                string waktuPengerjaan = txtPLY.Text;
                string Harga = txtHLY.Text;

                var confirm = MessageBox.Show("Apakah Anda yakin ingin mengupdate data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();
                    try
                    {
                        SqlCommand cmd = new SqlCommand("usp_Layanan_Update", con, transaction);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_Layanan", id);
                        cmd.Parameters.AddWithValue("@Nama_Layanan", nama);
                        cmd.Parameters.AddWithValue("@Panjang_Hari", waktuPengerjaan);
                        cmd.Parameters.AddWithValue("@L_Harga", Harga);


                        int result = cmd.ExecuteNonQuery();
                        transaction.Commit();

                        if (result > 0)
                        {
                            _cache.Remove(_cacheKey);
                            MessageBox.Show("Data berhasil diperbarui.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                            LoadData();
                        }
                        else
                        {
                            MessageBox.Show("Update gagal.", "Gagal", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                        MessageBox.Show("Terjadi kesalahan saat menyimpan data. Silakan coba lagi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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
