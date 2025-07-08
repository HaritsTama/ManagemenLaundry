using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagemenLaundry
{
    public partial class TambahPelangganForm: Form
    {
        Koneksi koneksi = new Koneksi();

        private string connectionString = "";
        private int selectedID = -1;

        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly string _cacheKey = "PelangganData";
        private readonly CacheItemPolicy _cachePolicy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
        };

        public TambahPelangganForm()
        {
            InitializeComponent();
            connectionString = koneksi.connectionString();
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



        private void btnTambah_Click(object sender, EventArgs e)
        {
            string nama = txtNPG.Text;
            string email = txtEPG.Text;
            string telepon = txtTPG.Text;

            // Validasi input
            if (string.IsNullOrWhiteSpace(nama) || string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Harap isi semua data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Regex emailRegex = new Regex(
                @"^(?!.*\.\.)(?!.*\.$)[^\W][\w!#$%&'*+/=?^_`{|}~.-]{0,63}@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"
            );

            if (!emailRegex.IsMatch(email))
            {
                MessageBox.Show("Format email tidak valid.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validasi tanggal masuk
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNPG.Text, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Nama pelanggan tidak boleh mengandung karakter spesial atau angka.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ;


            // Konfirmasi sebelum simpan
            var konfirmasi = MessageBox.Show(
                $"Apakah Anda yakin ingin menyimpan data berikut?\n\n" +
                $"Nama: {nama}\n" +
                $"Enail: {email}\n" +
                $"Telepon: {telepon}\n" +
                MessageBoxButtons.YesNo
            );

            if (konfirmasi == DialogResult.No)
            {
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(koneksi.connectionString()))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();
                    try
                    {
                        SqlCommand cmd = new SqlCommand("usp_Pelanggan_Insert", con, transaction);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@Nama", nama);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@NoTelp", telepon);


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

        private void btnHapus_Click(object sender, EventArgs e)
        {
            if (dgvPelanggan.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvPelanggan.CurrentRow.Cells["ID_Pelanggan"].Value);

                var confirm = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (confirm != DialogResult.Yes) return;

                using (SqlConnection con = new SqlConnection(koneksi.connectionString()))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();
                    try
                    {
                        SqlCommand cmd = new SqlCommand("usp_Pelanggan_Delete", con, transaction);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Pelanggan", id);
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

        private void TambahPelangganForm_Load_1(object sender, EventArgs e)
        {

        }


        private void btnUbah_Click(object sender, EventArgs e)
        {
            if (dgvPelanggan.CurrentRow != null)
            {
                int id = Convert.ToInt32(dgvPelanggan.CurrentRow.Cells["ID_Pelanggan"].Value);
                string nama = txtNPG.Text;
                string email = txtEPG.Text;
                string telepon = txtTPG.Text;

                Regex emailRegex = new Regex(
                @"^(?!.*\.\.)(?!.*\.$)[^\W][\w!#$%&'*+/=?^_`{|}~.-]{0,63}@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$"
                );

                if (!emailRegex.IsMatch(email))
                {
                    MessageBox.Show("Format email tidak valid.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var confirm = MessageBox.Show("Apakah Anda yakin ingin mengupdate data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm != DialogResult.Yes) return;

                using (SqlConnection con = new SqlConnection(koneksi.connectionString()))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();
                    try
                    {
                        SqlCommand cmd = new SqlCommand("usp_Pelanggan_Update", con, transaction);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID_Pelanggan", id);
                        cmd.Parameters.AddWithValue("@Nama", nama);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@NoTelp", telepon);


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

        private void LoadData()
        {
            try
            {
                DataTable cachedData = _cache.Get(_cacheKey) as DataTable;

                if (cachedData != null)
                {
                    dgvPelanggan.DataSource = cachedData;
                    return;
                }

                using (SqlConnection con = new SqlConnection(koneksi.connectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("usp_Pelanggan_GetAll", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        // Simpan ke cache
                        _cache.Set(_cacheKey, dt, _cachePolicy);

                        dgvPelanggan.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
