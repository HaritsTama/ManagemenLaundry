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
        private DataTable dtPelanggan;
        private DataTable dtLayanan;
        private DataTable dtBarang;

        private Dictionary<int, int> barangQuantities = new Dictionary<int, int>();

        public MasukanPesananForm()
        {
            InitializeComponent();
        }

        private void MasukanPesananForm_Load(object sender, EventArgs e)
        {
            LoadComboBoxData();
            LoadData();
            clbTambahan.ItemCheck += clbTambahan_ItemCheck;
        }

        private void LoadComboBoxData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Pelanggan
                dtPelanggan = new DataTable();
                new SqlDataAdapter("SELECT ID_Pelanggan, Nama FROM Pelanggan", conn).Fill(dtPelanggan);
                plhPelanggan.DataSource = dtPelanggan;
                plhPelanggan.DisplayMember = "Nama";
                plhPelanggan.ValueMember = "ID_Pelanggan";

                // Layanan
                dtLayanan = new DataTable();
                new SqlDataAdapter("SELECT ID_Layanan, Nama_Layanan FROM Layanan", conn).Fill(dtLayanan);
                plhLayanan.DataSource = dtLayanan;
                plhLayanan.DisplayMember = "Nama_Layanan";
                plhLayanan.ValueMember = "ID_Layanan";

                // Barang (extras)
                dtBarang = new DataTable();
                new SqlDataAdapter("SELECT ID_Barang, Ekstra_Barang FROM Barang", conn).Fill(dtBarang);
                clbTambahan.DataSource = dtBarang;
                clbTambahan.DisplayMember = "Ekstra_Barang";
                clbTambahan.ValueMember = "ID_Barang";

                // Status Laundry
                plhStatus.Items.Clear();
                plhStatus.Items.AddRange(new[] { "Proses", "Selesai", "Dibatalkan" });
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
                           pel.Nama AS Nama_Pelanggan,
                           l.Nama_Layanan,
                           p.Berat,
                           p.Status_Laundry
                    FROM Pesanan p
                    JOIN Pelanggan pel ON p.ID_Pelanggan = pel.ID_Pelanggan
                    JOIN Layanan l ON p.ID_Layanan = l.ID_Layanan";

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

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        // Insert Pesanan
                        string qPesanan = "INSERT INTO Pesanan (ID_Pelanggan, ID_Layanan, Berat, Status_Laundry) VALUES (@pel, @lay, @ber, @stat); SELECT CAST(SCOPE_IDENTITY() AS INT);";
                        var cmd = new SqlCommand(qPesanan, conn, tran);
                        cmd.Parameters.AddWithValue("@pel", plhPelanggan.SelectedValue);
                        cmd.Parameters.AddWithValue("@lay", plhLayanan.SelectedValue);
                        cmd.Parameters.AddWithValue("@ber", berat);
                        cmd.Parameters.AddWithValue("@stat", plhStatus.SelectedItem.ToString());
                        int newId = (int)cmd.ExecuteScalar();

                        // Insert PesananBarang (jumlah default 1)
                        foreach (DataRowView dr in clbTambahan.CheckedItems)
                        {
                            int idBarang = (int)dr.Row["ID_Barang"];
                            int jumlah = barangQuantities.ContainsKey(idBarang) ? barangQuantities[idBarang] : 1;

                            var qPB = "INSERT INTO PesananBarang (ID_Pesanan, ID_Barang, Jumlah) VALUES (@idp, @idb, @jml)";
                            var cmd2 = new SqlCommand(qPB, conn, tran);
                            cmd2.Parameters.AddWithValue("@idp", newId);
                            cmd2.Parameters.AddWithValue("@idb", idBarang);
                            cmd2.Parameters.AddWithValue("@jml", jumlah);
                            cmd2.ExecuteNonQuery();
                        }

                        tran.Commit();
                        MessageBox.Show("Pesanan berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            DialogResult result = MessageBox.Show("Apakah anda ingin menghapus pesanan ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No) return;

            int id = (int)dgvPesanan.SelectedRows[0].Cells["ID_Pesanan"].Value;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    var del = new SqlCommand("DELETE FROM Pesanan WHERE ID_Pesanan=@id", conn);
                    del.Parameters.AddWithValue("@id", id);
                    int rows = del.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Pesanan berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        // Update Pesanan
                        var upd = new SqlCommand(
                            "UPDATE Pesanan SET ID_Pelanggan=@pel, ID_Layanan=@lay, Berat=@ber, Status_Laundry=@stat WHERE ID_Pesanan=@id", conn, tran);
                        upd.Parameters.AddWithValue("@pel", plhPelanggan.SelectedValue);
                        upd.Parameters.AddWithValue("@lay", plhLayanan.SelectedValue);
                        upd.Parameters.AddWithValue("@ber", berat);
                        upd.Parameters.AddWithValue("@stat", plhStatus.SelectedItem.ToString());
                        upd.Parameters.AddWithValue("@id", id);
                        upd.ExecuteNonQuery();

                        // Reset PesananBarang
                        new SqlCommand("DELETE FROM PesananBarang WHERE ID_Pesanan=@id", conn, tran)
                            .Parameters.AddWithValue("@id", id);
                        new SqlCommand("DELETE FROM PesananBarang WHERE ID_Pesanan=@id", conn, tran).ExecuteNonQuery();

                        // Reinsert extras
                        foreach (DataRowView dr in clbTambahan.CheckedItems)
                        {
                            int idBarang = (int)dr.Row["ID_Barang"];
                            int jumlah = barangQuantities.ContainsKey(idBarang) ? barangQuantities[idBarang] : 1;

                            var qPB = "INSERT INTO PesananBarang (ID_Pesanan, ID_Barang, Jumlah) VALUES (@idp, @idb, @jml)";
                            var cmd2 = new SqlCommand(qPB, conn, tran);
                            cmd2.Parameters.AddWithValue("@idp", id); // <-- use id here
                            cmd2.Parameters.AddWithValue("@idb", idBarang);
                            cmd2.Parameters.AddWithValue("@jml", jumlah);
                            cmd2.ExecuteNonQuery();
                        }

                        tran.Commit();
                        MessageBox.Show("Pesanan berhasil diubah!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void dgvPesanan_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
            foreach (DataRowView dr in clbTambahan.Items)
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
    }
}
