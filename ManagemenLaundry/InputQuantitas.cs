using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManagemenLaundry
{
    public partial class InputQuantitas: Form
    {
        public InputQuantitas()
        {
            InitializeComponent();
        }
        public int Jumlah { get; private set; }
        public InputQuantitas(string namaBarang, int defaultJumlah = 1)
        {
            InitializeComponent();

            lblNamaBarang.Text = $"Masukkan jumlah untuk: {namaBarang}";

            numericJumlah.Minimum = 1;
            numericJumlah.Maximum = 100; // Bisa kamu sesuaikan
            numericJumlah.Value = defaultJumlah;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Jumlah = (int)numericJumlah.Value;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
