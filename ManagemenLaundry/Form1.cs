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
    }
}