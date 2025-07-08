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
    public partial class TambahBarangForm: Form
    {
        Koneksi koneksi = new Koneksi();

        private string connectionString = "";

        private static readonly MemoryCache _cache = MemoryCache.Default;
        private const string CacheKeyBarang = "DataBarang"; // Kunci unik untuk cache barang

        public TambahBarangForm()
        {
            InitializeComponent();
            connectionString = koneksi.connectionString();
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

        // Method untuk menghapus cache berdasarkan kunci
        private void InvalidateCache()
        {
            _cache.Remove(CacheKeyBarang);
        }

        private void LoadData()
        {
            // Cek apakah data ada di cache menggunakan kunci
            if (_cache.Contains(CacheKeyBarang))
            {
                // Ambil data dari cache dan pastikan tipenya benar (casting)
                dgvBarang.DataSource = _cache.Get(CacheKeyBarang) as DataTable;
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(koneksi.connectionString()))
                {
                    SqlCommand cmd = new SqlCommand("sp_SelectAllBarang", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Simpan data ke cache dengan kebijakan kadaluwarsa (contoh: 5 menit)
                    var policy = new CacheItemPolicy //optimisasi
                    {
                        AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
                    };
                    _cache.Set(CacheKeyBarang, dt, policy);

                    dgvBarang.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memuat data: " + ex.Message);
            }
        }

        private void btnTambahB_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNBR.Text) || string.IsNullOrWhiteSpace(txtHBR.Text))
            {
                MessageBox.Show("Semua field harus diisi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(txtNBR.Text, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("Nama Barang tidak boleh mengandung karakter spesial atau angka.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var konfirmasi = MessageBox.Show($"Apakah Anda yakin ingin menyimpan data berikut?\n\nNama: {txtNBR.Text}\nHarga: {txtHBR.Text}\n", "Konfirmasi Simpan", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.No) return;

            try
            {
                using (SqlConnection con = new SqlConnection(koneksi.connectionString()))
                {
                    con.Open();
                    SqlTransaction transaction = con.BeginTransaction();
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("sp_InsertBarang", con, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Ekstra_Barang", txtNBR.Text);
                            cmd.Parameters.AddWithValue("@Harga_Ekstra", txtHBR.Text);
                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        MessageBox.Show("Data berhasil ditambahkan", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        InvalidateCache(); // Hapus cache karena data telah berubah
                        ClearForm();
                        LoadData();
                    }
                    catch (Exception ex)  // error hendling
                    {
                        transaction.Rollback();
                        MessageBox.Show("Gagal menambahkan data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal terhubung ke database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHapusB_Click(object sender, EventArgs e)
        {
            // Pastikan ada baris yang dipilih di DataGridView
            if (dgvBarang.CurrentRow == null)
            {
                MessageBox.Show("Silakan pilih data yang ingin dihapus.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Ambil ID dari baris yang dipilih
            int id = Convert.ToInt32(dgvBarang.CurrentRow.Cells["ID_Barang"].Value);

            // Konfirmasi kepada pengguna
            var confirm = MessageBox.Show("Apakah Anda yakin ingin menghapus data ini secara permanen?", "Konfirmasi Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(koneksi.connectionString()))
                    {
                        con.Open();
                        // BARIS KRITIS 1: Memulai transaksi
                        SqlTransaction transaction = con.BeginTransaction();

                        try
                        {
                            // BARIS KRITIS 2: Memastikan 'transaction' DILEWATKAN ke dalam command
                            using (SqlCommand cmd = new SqlCommand("sp_DeleteBarang", con, transaction))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@ID_Barang", id);
                                cmd.ExecuteNonQuery();
                            }

                            // BARIS KRITIS 3: Commit transaksi jika berhasil
                            transaction.Commit();

                            MessageBox.Show("Data berhasil dihapus.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Perbarui data di tampilan
                            InvalidateCache();
                            ClearForm();
                            LoadData();
                        }
                        catch (Exception ex)  // error hendling
                        {
                            // BARIS KRITIS 4: Rollback transaksi jika terjadi error
                            transaction.Rollback();
                            MessageBox.Show("Gagal menghapus data dari database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gagal terhubung ke database: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnUbahB_Click(object sender, EventArgs e)
        {
            if (dgvBarang.CurrentRow == null) return;

            int id = Convert.ToInt32(dgvBarang.CurrentRow.Cells["ID_Barang"].Value);

            if (string.IsNullOrWhiteSpace(txtNBR.Text) || string.IsNullOrWhiteSpace(txtHBR.Text))
            {
                MessageBox.Show("Semua field harus diisi.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var konfirmasi = MessageBox.Show($"Apakah Anda yakin ingin mengupdate data ini?\n\nNama: {txtNBR.Text}\nHarga: {txtHBR.Text}", "Konfirmasi Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (konfirmasi == DialogResult.No) return;

            try
            {
                using (SqlConnection con = new SqlConnection(koneksi.connectionString()
                    ))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateBarang", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID_Barang", id);
                        cmd.Parameters.AddWithValue("@Ekstra_Barang", txtNBR.Text);
                        cmd.Parameters.AddWithValue("@Harga_Ekstra", txtHBR.Text);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Data berhasil diperbarui", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    InvalidateCache(); // Hapus cache karena data telah berubah
                    ClearForm();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gagal memperbarui data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefreshB_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah anda ingin merefresh data dari database?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                InvalidateCache(); // Paksa hapus cache untuk memuat ulang
                LoadData();
                MessageBox.Show("Data berhasil direfresh.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvBarang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Mengganti CellContentClick dengan CellClick agar lebih responsif
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvBarang.Rows[e.RowIndex];
                txtNBR.Text = row.Cells["Ekstra_Barang"].Value?.ToString();
                txtHBR.Text = row.Cells["Harga_Ekstra"].Value?.ToString();
            }
        }
    }
}
