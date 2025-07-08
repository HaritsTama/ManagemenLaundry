using Microsoft.Reporting.WinForms;
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
    public partial class FormLaporanPembelian : Form
    {
        Koneksi koneksi = new Koneksi();

        string connectionString = "";

        public FormLaporanPembelian()
        {
            InitializeComponent();
            connectionString = koneksi.connectionString();
        }

        private void FormLaporanPembelian_Load(object sender, EventArgs e)
        {

            SetupReportViewer();
            this.reportViewer1.RefreshReport();
        }

        private void SetupReportViewer()
        {
            // connection string to your database


            // SQL query to retrieve the required data from the database
            string query = @"
                SELECT 
                        pgn.Nama,
                        lyn.Nama_Layanan,
                        psn.ID_Pesanan,
                        psn.Berat,
                        ISNULL(SUM(pb.Jumlah * brg.Harga_Ekstra), 0) AS Total_Harga_Ekstra,
                        pmby.Total_Harga
                    FROM 
                        Pesanan psn
                    JOIN 
                        Pelanggan pgn ON psn.ID_Pelanggan = pgn.ID_Pelanggan
                    JOIN 
                        Layanan lyn ON psn.ID_Layanan = lyn.ID_Layanan
                    LEFT JOIN 
                        PesananBarang pb ON psn.ID_Pesanan = pb.ID_Pesanan
                    LEFT JOIN 
                        Barang brg ON pb.ID_Barang = brg.ID_Barang
                    JOIN 
                        Pembayaran pmby ON psn.ID_Pesanan = pmby.ID_Pesanan
                    GROUP BY 
                        pgn.Nama, 
                        lyn.Nama_Layanan, 
                        psn.ID_Pesanan, 
                        psn.Berat, 
                        pmby.Total_Harga;
                    ";

            // Create a DataTable to store the data
            DataTable dt = new DataTable();

            // use SqlDataAdapter to fill the DataTable with data from the database
            using (SqlConnection conn = new SqlConnection(koneksi.connectionString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dt);
            }

            // Create a ReportDataSource
            ReportDataSource rds = new ReportDataSource("DataSet1", dt); // Make sure "DataSet1" matches your RDLC dataset name

            // Clear any existing data sources and add the new data source
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            // Set the path to the report (.rdlc file)
            // Change this to the actual path of your RDLC file
            reportViewer1.LocalReport.ReportPath = @"C:\Users\user\source\repos\ManagemenLaundry\ManagemenLaundry\LaporanLaundry.rdlc";
            // Refresh the ReportViewer to show the updated report
            reportViewer1.RefreshReport();
        }
    }
}
