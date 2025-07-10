using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagemenLaundry
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source= LAPTOP-RFI0KF85\\HARITSZHAFRAN ;Initial Catalog=SistemManajemenLaundry;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }
        private void BtnPelangBaru_Click(object sender, EventArgs e)
        {
            TambahPelangganForm tambahPelangganForm = new TambahPelangganForm();
            tambahPelangganForm.ShowDialog();
        }

        private void BtnBuatPesan_Click(object sender, EventArgs e)
        {
            MasukanPesananForm masukanPesananForm = new MasukanPesananForm();
            masukanPesananForm.ShowDialog();
        }

        private void BtnPembayaran_Click(object sender, EventArgs e)
        {
            PembayaranForm pembayaranForm = new PembayaranForm();
            pembayaranForm.ShowDialog();
        }

        private void btnTambah_Click(object sender, EventArgs e)
        {
            TambahLayananForm tambahLayananForm = new TambahLayananForm();
            tambahLayananForm.ShowDialog();
        }

        private void btnBarang_Click(object sender, EventArgs e)
        {
            TambahBarangForm tambahBarangForm = new TambahBarangForm();
            tambahBarangForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormLaporanPembelian formLaporanPembelian = new FormLaporanPembelian();
            formLaporanPembelian.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnTestKoneksi_Click(object sender, EventArgs e)
        {
            string result = Koneksi.TestConnectionWithMessage();
            MessageBox.Show(result, "Test Koneksi Database");

            // Or update a label
            lblStatusKoneksi.Text = result;
        }

        private void btnAnalisis_Click(object sender, EventArgs e)
        {
            string query = @"
        SELECT 
            p.ID_Pelanggan, p.Nama AS NamaPelanggan, p.NoTelp, p.Email,
            ps.ID_Pesanan, ps.Tanggal_Masuk, ps.Status_Laundry, ps.Berat,
            l.Nama_Layanan, l.Panjang_Hari, l.L_Harga,
            b.Ekstra_Barang, pb.Jumlah AS JumlahBarang,
            pm.Total_Harga, pm.Metode_Pembayaran, pm.Status_Pembayaran, pm.Billing_Dibuat, pm.Batas_Lunas
        FROM Pelanggan p
        JOIN Pesanan ps ON p.ID_Pelanggan = ps.ID_Pelanggan
        JOIN Layanan l ON ps.ID_Layanan = l.ID_Layanan
        LEFT JOIN PesananBarang pb ON ps.ID_Pesanan = pb.ID_Pesanan
        LEFT JOIN Barang b ON pb.ID_Barang = b.ID_Barang
        LEFT JOIN Pembayaran pm ON ps.ID_Pesanan = pm.ID_Pesanan
    ";

            try
            {
                using (SqlConnection conn = Koneksi.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);

                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    stopwatch.Stop();

                    int rowCount = dt.Rows.Count;
                    long duration = stopwatch.ElapsedMilliseconds;

                    MessageBox.Show(
                        $"Analisis Telah Dijalankan.\nJumlah baris hasil: {rowCount}\nDurasi eksekusi: {duration} ms",
                        "Analisis Query",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Query gagal dijalankan:\n{ex.Message}", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}