namespace ManagemenLaundry
{
    partial class PembayaranForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.IDPS = new System.Windows.Forms.Label();
            this.BrEkstra = new System.Windows.Forms.Label();
            this.Berat = new System.Windows.Forms.Label();
            this.plhSts = new System.Windows.Forms.ComboBox();
            this.tipeLayanan = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.plhMtd = new System.Windows.Forms.ComboBox();
            this.btnRefreshPe = new System.Windows.Forms.Button();
            this.btnUbahPe = new System.Windows.Forms.Button();
            this.btnHapusPe = new System.Windows.Forms.Button();
            this.btnTambahPe = new System.Windows.Forms.Button();
            this.dgvPembayaran = new System.Windows.Forms.DataGridView();
            this.dtpLns = new System.Windows.Forms.DateTimePicker();
            this.dtpBill = new System.Windows.Forms.DateTimePicker();
            this.txtPsn = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPembayaran)).BeginInit();
            this.SuspendLayout();
            // 
            // IDPS
            // 
            this.IDPS.AutoSize = true;
            this.IDPS.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDPS.Location = new System.Drawing.Point(55, 43);
            this.IDPS.Name = "IDPS";
            this.IDPS.Size = new System.Drawing.Size(106, 20);
            this.IDPS.TabIndex = 32;
            this.IDPS.Text = "ID Pesanan";
            // 
            // BrEkstra
            // 
            this.BrEkstra.AutoSize = true;
            this.BrEkstra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrEkstra.Location = new System.Drawing.Point(55, 232);
            this.BrEkstra.Name = "BrEkstra";
            this.BrEkstra.Size = new System.Drawing.Size(115, 20);
            this.BrEkstra.TabIndex = 44;
            this.BrEkstra.Text = "Batas Lunas";
            // 
            // Berat
            // 
            this.Berat.AutoSize = true;
            this.Berat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Berat.Location = new System.Drawing.Point(55, 186);
            this.Berat.Name = "Berat";
            this.Berat.Size = new System.Drawing.Size(123, 20);
            this.Berat.TabIndex = 42;
            this.Berat.Text = "Billing Dibuat";
            // 
            // plhSts
            // 
            this.plhSts.FormattingEnabled = true;
            this.plhSts.Items.AddRange(new object[] {
            "Lunas",
            "Belum Lunas"});
            this.plhSts.Location = new System.Drawing.Point(240, 137);
            this.plhSts.Name = "plhSts";
            this.plhSts.Size = new System.Drawing.Size(249, 24);
            this.plhSts.TabIndex = 41;
            // 
            // tipeLayanan
            // 
            this.tipeLayanan.AutoSize = true;
            this.tipeLayanan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tipeLayanan.Location = new System.Drawing.Point(55, 137);
            this.tipeLayanan.Name = "tipeLayanan";
            this.tipeLayanan.Size = new System.Drawing.Size(63, 20);
            this.tipeLayanan.TabIndex = 40;
            this.tipeLayanan.Text = "Status";
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.Location = new System.Drawing.Point(55, 89);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(70, 20);
            this.status.TabIndex = 39;
            this.status.Text = "Metode";
            // 
            // plhMtd
            // 
            this.plhMtd.FormattingEnabled = true;
            this.plhMtd.Items.AddRange(new object[] {
            "Tunai",
            "Transfer",
            "E-Wallet"});
            this.plhMtd.Location = new System.Drawing.Point(240, 89);
            this.plhMtd.Name = "plhMtd";
            this.plhMtd.Size = new System.Drawing.Size(249, 24);
            this.plhMtd.TabIndex = 38;
            // 
            // btnRefreshPe
            // 
            this.btnRefreshPe.Location = new System.Drawing.Point(561, 219);
            this.btnRefreshPe.Name = "btnRefreshPe";
            this.btnRefreshPe.Size = new System.Drawing.Size(186, 35);
            this.btnRefreshPe.TabIndex = 49;
            this.btnRefreshPe.Text = "Refresh";
            this.btnRefreshPe.UseVisualStyleBackColor = true;
            this.btnRefreshPe.Click += new System.EventHandler(this.btnRefreshPe_Click);
            // 
            // btnUbahPe
            // 
            this.btnUbahPe.Location = new System.Drawing.Point(561, 158);
            this.btnUbahPe.Name = "btnUbahPe";
            this.btnUbahPe.Size = new System.Drawing.Size(186, 35);
            this.btnUbahPe.TabIndex = 48;
            this.btnUbahPe.Text = "Ubah";
            this.btnUbahPe.UseVisualStyleBackColor = true;
            this.btnUbahPe.Click += new System.EventHandler(this.btnUbahPe_Click);
            // 
            // btnHapusPe
            // 
            this.btnHapusPe.Location = new System.Drawing.Point(561, 104);
            this.btnHapusPe.Name = "btnHapusPe";
            this.btnHapusPe.Size = new System.Drawing.Size(186, 35);
            this.btnHapusPe.TabIndex = 47;
            this.btnHapusPe.Text = "Hapus";
            this.btnHapusPe.UseVisualStyleBackColor = true;
            this.btnHapusPe.Click += new System.EventHandler(this.btnHapusPe_Click);
            // 
            // btnTambahPe
            // 
            this.btnTambahPe.Location = new System.Drawing.Point(561, 43);
            this.btnTambahPe.Name = "btnTambahPe";
            this.btnTambahPe.Size = new System.Drawing.Size(186, 35);
            this.btnTambahPe.TabIndex = 46;
            this.btnTambahPe.Text = "Tambah";
            this.btnTambahPe.UseVisualStyleBackColor = true;
            this.btnTambahPe.Click += new System.EventHandler(this.btnTambahPe_Click);
            // 
            // dgvPembayaran
            // 
            this.dgvPembayaran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPembayaran.Location = new System.Drawing.Point(59, 277);
            this.dgvPembayaran.Name = "dgvPembayaran";
            this.dgvPembayaran.RowHeadersWidth = 51;
            this.dgvPembayaran.RowTemplate.Height = 24;
            this.dgvPembayaran.Size = new System.Drawing.Size(688, 197);
            this.dgvPembayaran.TabIndex = 50;
            this.dgvPembayaran.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPembayaran_CellContentClick);
            // 
            // dtpLns
            // 
            this.dtpLns.Location = new System.Drawing.Point(240, 232);
            this.dtpLns.Name = "dtpLns";
            this.dtpLns.Size = new System.Drawing.Size(249, 22);
            this.dtpLns.TabIndex = 51;
            // 
            // dtpBill
            // 
            this.dtpBill.Location = new System.Drawing.Point(240, 184);
            this.dtpBill.Name = "dtpBill";
            this.dtpBill.Size = new System.Drawing.Size(249, 22);
            this.dtpBill.TabIndex = 52;
            // 
            // txtPsn
            // 
            this.txtPsn.Location = new System.Drawing.Point(240, 40);
            this.txtPsn.Name = "txtPsn";
            this.txtPsn.Size = new System.Drawing.Size(249, 22);
            this.txtPsn.TabIndex = 53;
            // 
            // PembayaranForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 496);
            this.Controls.Add(this.txtPsn);
            this.Controls.Add(this.dtpBill);
            this.Controls.Add(this.dtpLns);
            this.Controls.Add(this.dgvPembayaran);
            this.Controls.Add(this.btnRefreshPe);
            this.Controls.Add(this.btnUbahPe);
            this.Controls.Add(this.btnHapusPe);
            this.Controls.Add(this.btnTambahPe);
            this.Controls.Add(this.BrEkstra);
            this.Controls.Add(this.Berat);
            this.Controls.Add(this.plhSts);
            this.Controls.Add(this.tipeLayanan);
            this.Controls.Add(this.status);
            this.Controls.Add(this.plhMtd);
            this.Controls.Add(this.IDPS);
            this.Name = "PembayaranForm";
            this.Text = "PembayaranForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPembayaran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label IDPS;
        private System.Windows.Forms.Label BrEkstra;
        private System.Windows.Forms.Label Berat;
        private System.Windows.Forms.ComboBox plhSts;
        private System.Windows.Forms.Label tipeLayanan;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.ComboBox plhMtd;
        private System.Windows.Forms.Button btnRefreshPe;
        private System.Windows.Forms.Button btnUbahPe;
        private System.Windows.Forms.Button btnHapusPe;
        private System.Windows.Forms.Button btnTambahPe;
        private System.Windows.Forms.DataGridView dgvPembayaran;
        private System.Windows.Forms.DateTimePicker dtpLns;
        private System.Windows.Forms.DateTimePicker dtpBill;
        private System.Windows.Forms.TextBox txtPsn;
    }
}