using Org.BouncyCastle.Asn1.Cmp;
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
    public partial class PembayaranForm: Form
    {
        Koneksi koneksi = new Koneksi();

        private string connectionString = "";
        private int selectedPesananId = -1;

        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) // cache expires after 5 minutes
        };
        private const string PesananComboKey = "PesananComboData";



        public PembayaranForm()
        {
            InitializeComponent();
            connectionString = koneksi.connectionString();
        }

        private void PembayaranForm_Load(object sender, EventArgs e)
        {
            EnsureIndexes();
            LoadComboBoxPesanan();
            LoadData();
        }

        private void LoadComboBoxPesanan()
        {
            using (SqlConnection conn = new SqlConnection(koneksi.connectionString()))
            {
                conn.Open();

                DataTable PesananData = _cache.Get(PesananComboKey) as DataTable;
                if (PesananData == null)
                {
                    PesananData = new DataTable();
                    {
                        new SqlDataAdapter("SELECT ID_Pesanan FROM Pesanan ORDER BY ID_Pesanan", conn).Fill(PesananData);
                    }
                    _cache.Set(PesananComboKey, PesananData, _policy);
                }

                cmbPsn.DataSource = PesananData;
                cmbPsn.DisplayMember = "ID_Pesanan";
                cmbPsn.ValueMember = "ID_Pesanan";

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
            using (SqlConnection conn = new SqlConnection(koneksi.connectionString()))
            {
                string query = @"
                    SELECT 
                        ID_Pesanan,
                        Total_Harga,
                        Metode_Pembayaran,
                        Status_Pembayaran,
                        Billing_Dibuat,
                        Batas_Lunas
                    FROM Pembayaran";
                var da = new SqlDataAdapter(query, conn);
                var dt = new DataTable();
                da.Fill(dt);
                dgvPembayaran.AutoGenerateColumns = true;
                dgvPembayaran.DataSource = dt;
                ClearForm();
            }
        }



        private decimal HitungTotalHarga(int idPesanan)
        {
            using (SqlConnection conn = new SqlConnection(koneksi.connectionString()))
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
            using (SqlConnection conn = new SqlConnection(koneksi.connectionString()))
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

            DialogResult result = MessageBox.Show("Apakah anda ingin menambahkan data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            int idPesanan = Convert.ToInt32(cmbPsn.SelectedValue);
            decimal totalHarga = HitungTotalHarga(idPesanan);
            DateTime batasLunas = HitungBatasLunas(idPesanan);

            using (var conn = new SqlConnection(koneksi.connectionString()))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand("AddPembayaran", conn, tx))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_Pesanan", idPesanan);
                            cmd.Parameters.AddWithValue("@Total_Harga", totalHarga);
                            cmd.Parameters.AddWithValue("@Metode_Pembayaran", plhMtd.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@Status_Pembayaran", plhSts.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@Batas_Lunas", batasLunas);

                            var pNew = new SqlParameter("@NewID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                            cmd.Parameters.Add(pNew);

                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                        MessageBox.Show("Pesanan berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        MessageBox.Show("Error saat menambahkan: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        

        private void btnHapusPe_Click(object sender, EventArgs e)
        {
            if (dgvPembayaran.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            int idPesanan = (int)dgvPembayaran.SelectedRows[0].Cells["ID_Pesanan"].Value;
            using (SqlConnection conn = new SqlConnection(koneksi.connectionString()))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand("DeletePembayaran", conn, tx))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_Pesanan", idPesanan);
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                        MessageBox.Show("Data berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        MessageBox.Show("Error saat menambahkan: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            DialogResult result = MessageBox.Show("Apakah anda ingin mengubah data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            int idPesanan = (int)dgvPembayaran.SelectedRows[0].Cells["ID_Pesanan"].Value;
            decimal totalHarga = HitungTotalHarga(idPesanan);
            DateTime batasLunas = HitungBatasLunas(idPesanan);

            using (var conn = new SqlConnection(koneksi.connectionString()))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand("UpdatePembayaran", conn, tx))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_Pesanan", idPesanan);
                            cmd.Parameters.AddWithValue("@Total_Harga", totalHarga);
                            cmd.Parameters.AddWithValue("@Metode_Pembayaran", plhMtd.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@Status_Pembayaran", plhSts.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@Batas_Lunas", batasLunas);

                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                        MessageBox.Show("Data berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        MessageBox.Show("Error saat ubah: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private void dgvPembayaran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvPembayaran.Rows[e.RowIndex];

            int idPesanan = Convert.ToInt32(row.Cells["ID_Pesanan"].Value);
            cmbPsn.SelectedValue = idPesanan;

            plhMtd.SelectedItem = row.Cells["Metode_Pembayaran"].Value.ToString();
            plhSts.SelectedItem = row.Cells["Status_Pembayaran"].Value.ToString();
        }

        private void EnsureIndexes()
        {
            using (SqlConnection conn = new SqlConnection(koneksi.connectionString()))
            {
                conn.Open();
                string indexScript = @"
                    IF OBJECT_ID('dbo.Pembayaran', 'U') IS NOT NULL
                    BEGIN
                         IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Pembayaran_ID_Pesanan')
                            CREATE NONCLUSTERED INDEX IX_Pembayaran_ID_Pesanan ON dbo.Pembayaran(ID_Pesanan);
                    END
                    ";
                SqlCommand cmd = new SqlCommand(indexScript, conn);
                cmd.ExecuteNonQuery();
            }
        }

        private void AnalyzeQuery(string sqlQuery)
        {
            using (var conn = new SqlConnection(koneksi.connectionString()))
            {
                conn.InfoMessage += (s, e) => MessageBox.Show(e.Message, "STATISTICS INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                conn.Open();

                var wrapped = $@"
                        SET STATISTICS IO ON;
                        SET STATISTICS TIME ON;
                        {sqlQuery};
                        SET STATISTICS IO OFF;
                        SET STATISTICS TIME OFF;";

                using (var cmd = new SqlCommand(wrapped, conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void cmbPsn_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnAnalisis_Click_1(object sender, EventArgs e)
        {
            string query = @"
                    SELECT 
                        ID_Pembayaran,
                        ID_Pesanan,
                        Total_Harga,
                        Metode_Pembayaran,
                        Status_Pembayaran,
                        Billing_Dibuat,
                        Batas_Lunas
                    FROM Pembayaran;
                ";

            AnalyzeQuery(query);
        }
    }
}
