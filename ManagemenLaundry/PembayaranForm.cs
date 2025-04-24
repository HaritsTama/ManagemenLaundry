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
    public partial class PembayaranForm: Form
    {
        private string connectionString = "Data Source= LAPTOP-RFI0KF85\\HARITSZHAFRAN ;Initial Catalog=SistemManajemenLaundry;Integrated Security=True";
        public PembayaranForm()
        {
            InitializeComponent();
        }
        private void PembayaranForm_Load(object sender, EventArgs e)
        {
            LoadData();
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
                            pe.ID_Pembayaran, 
                            p.ID_Pesanan,    
                            pe.Metode_Pembayaran,
                            pe.Status_Pembayaran, 
                            pe.Billing_Dibuat, Batas_Lunas 
                        FROM Pembayaran pe
                        INNER JOIN Pesanan p ON pe.ID_Pesanan = p.ID_Pesanan
                    ";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvPembayaran.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearForm()
        {
            txtPsn.Clear();
            plhMtd.SelectedIndex = -1;
            plhSts.SelectedIndex = -1;
            dtpBill.Value = DateTime.Now;
            dtpLns.Value = DateTime.Now;
        }

        private void btnTambahPe_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    if (txtPsn.Text == "" || plhMtd.SelectedIndex == -1 || plhSts.SelectedIndex == -1)
                    {
                        MessageBox.Show("Harap isi semua data!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    conn.Open();
                    string query = "INSERT INTO Pembayaran (ID_Pesanan, Metode_Pembayaran, Status_Pembayaran, Billing_Dibuat, Batas_Lunas) " +
                                   "VALUES (@ID_Pesanan, @Metode_Pembayaran, @Status_Pembayaran, @Billing_Dibuat, @Batas_Lunas)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_Pesanan", txtPsn.Text.Trim());
                        cmd.Parameters.AddWithValue("@Metode_Pembayaran", plhMtd.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Status_Pembayaran", plhSts.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Billing_Dibuat", dtpBill.Value);
                        cmd.Parameters.AddWithValue("@Batas_Lunas", dtpLns.Value);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Pembayaran berhasil ditambahkan!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadData();
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Pembayaran gagal ditambahkan!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnHapusPe_Click(object sender, EventArgs e)
        {
            if (dgvPembayaran.SelectedRows.Count > 0)
            {
                DialogResult confirm = MessageBox.Show("Yakin ingin menghapus data ini?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        try
                        {
                            string ID_Pembayaran = dgvPembayaran.SelectedRows[0].Cells["ID_Pembayaran"].Value.ToString();
                            conn.Open();
                            string query = "DELETE FROM Pembayaran WHERE ID_Pembayaran = @ID_Pembayaran";

                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@ID_Pembayaran", ID_Pembayaran);
                                int rowsAffected = cmd.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Pembayaran berhasil dihapus!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadData();
                                }
                                else
                                {
                                    MessageBox.Show("Pembayaran tidak ditemukan atau gagal dihapus!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Pilih data yang akan dihapus!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUbahPe_Click(object sender, EventArgs e)
        {
            if (dgvPembayaran.SelectedRows.Count > 0)
            {
                string ID_Pembayaran = dgvPembayaran.SelectedRows[0].Cells["ID_Pembayaran"].Value.ToString();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "UPDATE Pembayaran SET " +
                                       "ID_Pesanan = @ID_Pesanan, " +
                                       "Metode_Pembayaran = @Metode_Pembayaran, " +
                                       "Status_Pembayaran = @Status_Pembayaran, " +
                                       "Billing_Dibuat = @Billing_Dibuat, " +
                                       "Batas_Lunas = @Batas_Lunas " +
                                       "WHERE ID_Pembayaran = @ID_Pembayaran";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@ID_Pesanan", txtPsn.Text.Trim());
                            cmd.Parameters.AddWithValue("@Metode_Pembayaran", plhMtd.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@Status_Pembayaran", plhSts.SelectedItem.ToString());
                            cmd.Parameters.AddWithValue("@Billing_Dibuat", dtpBill.Value);
                            cmd.Parameters.AddWithValue("@Batas_Lunas", dtpLns.Value);
                            cmd.Parameters.AddWithValue("@ID_Pembayaran", ID_Pembayaran);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Pembayaran berhasil diperbarui!", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadData();
                                ClearForm();
                            }
                            else
                            {
                                MessageBox.Show("Pembayaran gagal diperbarui!", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Pilih data yang ingin diperbarui!", "Peringatan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnRefreshPe_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            pe.ID_Pembayaran, 
                            p.ID_Pesanan,    
                            pe.Metode_Pembayaran,
                            pe.Status_Pembayaran, 
                            pe.Billing_Dibuat, Batas_Lunas 
                        FROM Pembayaran pe
                        INNER JOIN Pesanan p ON pe.ID_Pesanan = p.ID_Pesanan
                    ";
                    using (SqlDataAdapter da = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgvPembayaran.DataSource = dt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error refreshing data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvPembayaran_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPembayaran.Rows[e.RowIndex];

                txtPsn.Text = row.Cells["ID_Pesanan"].Value.ToString();
                plhMtd.SelectedItem = row.Cells["Metode_Pembayaran"].Value.ToString();
                plhSts.SelectedItem = row.Cells["Status_Pembayaran"].Value.ToString();
                dtpBill.Value = Convert.ToDateTime(row.Cells["Billing_Dibuat"].Value);
                dtpLns.Value = Convert.ToDateTime(row.Cells["Batas_Lunas"].Value);
            }
        }
    }
}
