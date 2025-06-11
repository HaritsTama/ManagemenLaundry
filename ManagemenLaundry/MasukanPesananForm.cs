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
using System.Runtime.Caching;


namespace ManagemenLaundry
{
    public partial class MasukanPesananForm : Form
    {
        private string connectionString = "Data Source= LAPTOP-RFI0KF85\\HARITSZHAFRAN ;Initial Catalog=SistemManajemenLaundry;Integrated Security=True";

        private Dictionary<int, int> barangQuantities = new Dictionary<int, int>();

        private readonly MemoryCache _cache = MemoryCache.Default;
        private readonly CacheItemPolicy _policy = new CacheItemPolicy
        {
            AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5) // cache expires after 5 minutes
        };
        private const string PelangganCacheKey = "PelangganData";
        private const string LayananCacheKey = "LayananData";
        private const string BarangCacheKey = "BarangData";

        public MasukanPesananForm()
        {
            InitializeComponent();
        }

        private void MasukanPesananForm_Load(object sender, EventArgs e)
        {
            EnsureIndexes();
            LoadComboBoxData();
            LoadData();
            clbTambahan.ItemCheck += clbTambahan_ItemCheck;
        }

        private void LoadComboBoxData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Load Pelanggan (with cache)
                DataTable pelangganData = _cache.Get(PelangganCacheKey) as DataTable;
                if (pelangganData == null)
                {
                    pelangganData = new DataTable();
                    {
                        new SqlDataAdapter("SELECT ID_Pelanggan, Nama FROM Pelanggan", conn).Fill(pelangganData);
                    }
                    _cache.Set(PelangganCacheKey, pelangganData, _policy);
                }

                plhPelanggan.DataSource = pelangganData;
                plhPelanggan.DisplayMember = "Nama";
                plhPelanggan.ValueMember = "ID_Pelanggan";

                // Layanan
                DataTable LayananData = _cache.Get(LayananCacheKey) as DataTable;
                if (LayananData == null)
                {
                    LayananData = new DataTable();
                    {
                        new SqlDataAdapter("SELECT ID_Layanan, Nama_Layanan FROM Layanan", conn).Fill(LayananData);
                    }
                    _cache.Set(LayananCacheKey, LayananData, _policy);
                }

                plhLayanan.DataSource = LayananData;
                plhLayanan.DisplayMember = "Nama_Layanan";
                plhLayanan.ValueMember = "ID_Layanan";

                // Barang (extras)
                DataTable BarangData = _cache.Get(BarangCacheKey) as DataTable;
                if (BarangData == null)
                {
                    BarangData = new DataTable();
                    {
                        new SqlDataAdapter("SELECT ID_Barang, Ekstra_Barang FROM Barang", conn).Fill(BarangData);
                    }
                    _cache.Set(BarangCacheKey, BarangData, _policy);
                }

                clbTambahan.DataSource = BarangData;
                clbTambahan.DisplayMember = "Ekstra_Barang";
                clbTambahan.ValueMember = "ID_Barang";

            }
        }

        private void ClearForm()
        {
            plhPelanggan.SelectedIndex = -1;
            plhLayanan.SelectedIndex = -1;
            plhStatus.SelectedIndex = -1;
            txtBerat.Clear();
            for (int i = 0; i < clbTambahan.Items.Count; i++) clbTambahan.SetItemChecked(i, false);

            txtBerat.Focus();
        }
        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                var query = @"
                    SELECT p.ID_Pesanan,
                       p.ID_Pelanggan,
                       pel.Nama AS Nama_Pelanggan,
                       p.ID_Layanan,
                       l.Nama_Layanan,
                       p.Berat,
                       p.Status_Laundry
                FROM Pesanan p
                JOIN Pelanggan pel ON p.ID_Pelanggan = pel.ID_Pelanggan
                JOIN Layanan l ON p.ID_Layanan = l.ID_Layanan
                ";

                var da = new SqlDataAdapter(query, conn);
                var dt = new DataTable();
                da.Fill(dt);
                dgvPesanan.AutoGenerateColumns = true;
                dgvPesanan.DataSource = dt;
                ClearForm();
            }
        }

        private void btnTambahP_Click(object sender, EventArgs e)
        {
            if (plhPelanggan.SelectedIndex < 0 || plhLayanan.SelectedIndex < 0 || plhStatus.SelectedIndex < 0 || string.IsNullOrWhiteSpace(txtBerat.Text))
            {
                MessageBox.Show("Harap isi semua data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!float.TryParse(txtBerat.Text.Trim(), out float berat) || berat < 0)
            {
                MessageBox.Show("Berat harus angka positif!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Apakah anda ingin menambahkan data pesanan?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            var barangTable = new DataTable();
            barangTable.Columns.Add("ID_Barang", typeof(int));
            barangTable.Columns.Add("Jumlah", typeof(int));
            foreach (DataRowView dr in clbTambahan.CheckedItems)
            {
                int id = (int)dr["ID_Barang"];
                int qty = barangQuantities.ContainsKey(id) ? barangQuantities[id] : 1;
                barangTable.Rows.Add(id, qty);
            }

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand("AddFullPesanan", conn, tx))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_Pelanggan", plhPelanggan.SelectedValue);
                            cmd.Parameters.AddWithValue("@ID_Layanan", plhLayanan.SelectedValue);
                            cmd.Parameters.AddWithValue("@Berat", berat);
                            cmd.Parameters.AddWithValue("@Status_Laundry", plhStatus.SelectedItem.ToString());

                            var tvp = cmd.Parameters.AddWithValue("@BarangList", barangTable);
                            tvp.SqlDbType = SqlDbType.Structured;
                            tvp.TypeName = "PesananBarangType";

                            var output = new SqlParameter("@NewID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                            cmd.Parameters.Add(output);
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

        private void btnHapusP_Click(object sender, EventArgs e)
        {

            if (dgvPesanan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("Apakah anda ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            int id = (int)dgvPesanan.SelectedRows[0].Cells["ID_Pesanan"].Value;
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand("DeletePesanan", conn, tx))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_Pesanan", id);
                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                        MessageBox.Show("Pesanan berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        tx.Rollback();
                        MessageBox.Show("Error saat hapus: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnUbahP_Click(object sender, EventArgs e)
        {
            if (dgvPesanan.SelectedRows.Count == 0)
            {
                MessageBox.Show("Pilih data yang ingin diubah!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("Apakah anda ingin mengubah pesanan ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            int id = (int)dgvPesanan.SelectedRows[0].Cells["ID_Pesanan"].Value;
            if (!float.TryParse(txtBerat.Text.Trim(), out float berat) || berat < 0)
            {
                MessageBox.Show("Berat harus angka positif!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var barangTable = new DataTable();
            barangTable.Columns.Add("ID_Barang", typeof(int));
            barangTable.Columns.Add("Jumlah", typeof(int));
            foreach (DataRowView dr in clbTambahan.CheckedItems)
            {
                int bid = (int)dr["ID_Barang"];
                int qty = barangQuantities.ContainsKey(bid) ? barangQuantities[bid] : 1;
                barangTable.Rows.Add(bid, qty);
            }

            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand("UpdateFullPesanan", conn, tx))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@ID_Pesanan", id);
                            cmd.Parameters.AddWithValue("@ID_Pelanggan", plhPelanggan.SelectedValue);
                            cmd.Parameters.AddWithValue("@ID_Layanan", plhLayanan.SelectedValue);
                            cmd.Parameters.AddWithValue("@Berat", berat);
                            cmd.Parameters.AddWithValue("@Status_Laundry", plhStatus.SelectedItem.ToString());

                            var tvp = cmd.Parameters.AddWithValue("@BarangList", barangTable);
                            tvp.SqlDbType = SqlDbType.Structured;
                            tvp.TypeName = "PesananBarangType";

                            cmd.ExecuteNonQuery();
                        }
                        tx.Commit();
                        MessageBox.Show("Pesanan berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnRefreshP_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Apakah anda ingin merefresh data?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;
            LoadData();
            MessageBox.Show($"Jumlah Kolom: {dgvPesanan.ColumnCount}\nJumlah Baris: {dgvPesanan.RowCount}", "Debugging DataGridView", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvPesanan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvPesanan.Rows[e.RowIndex];
            int id = (int)row.Cells["ID_Pesanan"].Value;

            int idPelanggan = Convert.ToInt32(row.Cells["ID_Pelanggan"].Value);
            plhPelanggan.SelectedValue = idPelanggan;
            int idLayanan = Convert.ToInt32(row.Cells["ID_Layanan"].Value);
            plhLayanan.SelectedValue = idLayanan;
            txtBerat.Text = row.Cells["Berat"].Value.ToString();
            plhStatus.SelectedItem = row.Cells["Status_Laundry"].Value.ToString();

            // Check extras
            var itemsCopy = clbTambahan.Items.Cast<DataRowView>().ToList();
            foreach (DataRowView dr in itemsCopy)
            {
                bool cek = false;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT COUNT(*) FROM PesananBarang WHERE ID_Pesanan=@idp AND ID_Barang=@idb", conn);
                    cmd.Parameters.AddWithValue("@idp", id);
                    cmd.Parameters.AddWithValue("@idb", (int)dr.Row["ID_Barang"]);
                    cek = (int)cmd.ExecuteScalar() > 0;
                }
                clbTambahan.SetItemChecked(clbTambahan.Items.IndexOf(dr), cek);
            }
        }

        private void clbTambahan_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                if (e.NewValue == CheckState.Checked)
                {
                    var item = (DataRowView)clbTambahan.Items[e.Index];
                    int idBarang = (int)item["ID_Barang"];
                    string namaBarang = item["Ekstra_Barang"].ToString();

                    int jumlahDefault = barangQuantities.ContainsKey(idBarang) ? barangQuantities[idBarang] : 1;
                    using (var inputForm = new InputQuantitas(namaBarang, jumlahDefault))
                    {
                        if (inputForm.ShowDialog() == DialogResult.OK)
                        {
                            barangQuantities[idBarang] = inputForm.Jumlah;
                        }
                        else
                        {
                            // Batalkan check jika user batal
                            clbTambahan.SetItemCheckState(e.Index, CheckState.Unchecked);
                        }
                    }
                }
                else
                {
                    var item = (DataRowView)clbTambahan.Items[e.Index];
                    int idBarang = (int)item["ID_Barang"];
                    barangQuantities.Remove(idBarang);
                }
            });
        }

        private void AnalyzeQuery(string sqlQuery)
        {
            using (var conn = new SqlConnection(connectionString))
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

        private void btnAnalisis_Click(object sender, EventArgs e)
        {
            string query = @"
                    SELECT p.ID_Pesanan,
                           p.ID_Pelanggan,
                           pel.Nama AS Nama_Pelanggan,
                           p.ID_Layanan,
                           l.Nama_Layanan,
                           p.Berat,
                           p.Status_Laundry
                    FROM Pesanan p
                    JOIN Pelanggan pel ON p.ID_Pelanggan = pel.ID_Pelanggan
                    JOIN Layanan l ON p.ID_Layanan = l.ID_Layanan";

            AnalyzeQuery(query);
        }
        
        private void EnsureIndexes()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string indexScript = @"
                    IF OBJECT_ID('dbo.Pesanan', 'U') IS NOT NULL
                    BEGIN
                        IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Pesanan_ID_Pelanggan')
                            CREATE NONCLUSTERED INDEX IX_Pesanan_ID_Pelanggan ON dbo.Pesanan(ID_Pelanggan);

                        IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Pesanan_ID_Layanan')
                            CREATE NONCLUSTERED INDEX IX_Pesanan_ID_Layanan ON dbo.Pesanan(ID_Layanan);

                        IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_PesananBarang_ID_Pesanan')
                            CREATE NONCLUSTERED INDEX IX_PesananBarang_ID_Pesanan ON dbo.PesananBarang(ID_Pesanan);
                    END
                    ";
                SqlCommand cmd = new SqlCommand(indexScript, conn);
                cmd.ExecuteNonQuery();
            }
        }

    }
}
