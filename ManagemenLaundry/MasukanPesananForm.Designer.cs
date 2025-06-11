namespace ManagemenLaundry
{
    partial class MasukanPesananForm
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
            this.IDPG = new System.Windows.Forms.Label();
            this.plhStatus = new System.Windows.Forms.ComboBox();
            this.status = new System.Windows.Forms.Label();
            this.tipeLayanan = new System.Windows.Forms.Label();
            this.plhLayanan = new System.Windows.Forms.ComboBox();
            this.Berat = new System.Windows.Forms.Label();
            this.txtBerat = new System.Windows.Forms.TextBox();
            this.btnRefreshP = new System.Windows.Forms.Button();
            this.btnUbahP = new System.Windows.Forms.Button();
            this.btnHapusP = new System.Windows.Forms.Button();
            this.btnTambahP = new System.Windows.Forms.Button();
            this.plhPelanggan = new System.Windows.Forms.ComboBox();
            this.dgvPesanan = new System.Windows.Forms.DataGridView();
            this.BrEkstra = new System.Windows.Forms.Label();
            this.clbTambahan = new System.Windows.Forms.CheckedListBox();
            this.btnAnalisis = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesanan)).BeginInit();
            this.SuspendLayout();
            // 
            // IDPG
            // 
            this.IDPG.AutoSize = true;
            this.IDPG.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDPG.Location = new System.Drawing.Point(48, 50);
            this.IDPG.Name = "IDPG";
            this.IDPG.Size = new System.Drawing.Size(150, 20);
            this.IDPG.TabIndex = 5;
            this.IDPG.Text = "Nama Pelanggan";
            // 
            // plhStatus
            // 
            this.plhStatus.FormattingEnabled = true;
            this.plhStatus.Items.AddRange(new object[] {
            "Proses",
            "Selesai",
            "Dibatalkan"});
            this.plhStatus.Location = new System.Drawing.Point(233, 105);
            this.plhStatus.Name = "plhStatus";
            this.plhStatus.Size = new System.Drawing.Size(249, 24);
            this.plhStatus.TabIndex = 8;
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.Location = new System.Drawing.Point(48, 105);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(136, 20);
            this.status.TabIndex = 11;
            this.status.Text = "Status Laundry";
            // 
            // tipeLayanan
            // 
            this.tipeLayanan.AutoSize = true;
            this.tipeLayanan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tipeLayanan.Location = new System.Drawing.Point(48, 155);
            this.tipeLayanan.Name = "tipeLayanan";
            this.tipeLayanan.Size = new System.Drawing.Size(121, 20);
            this.tipeLayanan.TabIndex = 12;
            this.tipeLayanan.Text = "Tipe Layanan";
            // 
            // plhLayanan
            // 
            this.plhLayanan.FormattingEnabled = true;
            this.plhLayanan.Location = new System.Drawing.Point(233, 155);
            this.plhLayanan.Name = "plhLayanan";
            this.plhLayanan.Size = new System.Drawing.Size(249, 24);
            this.plhLayanan.TabIndex = 13;
            // 
            // Berat
            // 
            this.Berat.AutoSize = true;
            this.Berat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Berat.Location = new System.Drawing.Point(48, 204);
            this.Berat.Name = "Berat";
            this.Berat.Size = new System.Drawing.Size(55, 20);
            this.Berat.TabIndex = 14;
            this.Berat.Text = "Berat";
            // 
            // txtBerat
            // 
            this.txtBerat.Location = new System.Drawing.Point(233, 204);
            this.txtBerat.Name = "txtBerat";
            this.txtBerat.Size = new System.Drawing.Size(249, 22);
            this.txtBerat.TabIndex = 15;
            // 
            // btnRefreshP
            // 
            this.btnRefreshP.Location = new System.Drawing.Point(519, 262);
            this.btnRefreshP.Name = "btnRefreshP";
            this.btnRefreshP.Size = new System.Drawing.Size(224, 49);
            this.btnRefreshP.TabIndex = 30;
            this.btnRefreshP.Text = "Refresh";
            this.btnRefreshP.UseVisualStyleBackColor = true;
            this.btnRefreshP.Click += new System.EventHandler(this.btnRefreshP_Click);
            // 
            // btnUbahP
            // 
            this.btnUbahP.Location = new System.Drawing.Point(519, 191);
            this.btnUbahP.Name = "btnUbahP";
            this.btnUbahP.Size = new System.Drawing.Size(224, 49);
            this.btnUbahP.TabIndex = 29;
            this.btnUbahP.Text = "Ubah";
            this.btnUbahP.UseVisualStyleBackColor = true;
            this.btnUbahP.Click += new System.EventHandler(this.btnUbahP_Click);
            // 
            // btnHapusP
            // 
            this.btnHapusP.Location = new System.Drawing.Point(519, 117);
            this.btnHapusP.Name = "btnHapusP";
            this.btnHapusP.Size = new System.Drawing.Size(224, 49);
            this.btnHapusP.TabIndex = 28;
            this.btnHapusP.Text = "Hapus";
            this.btnHapusP.UseVisualStyleBackColor = true;
            this.btnHapusP.Click += new System.EventHandler(this.btnHapusP_Click);
            // 
            // btnTambahP
            // 
            this.btnTambahP.Location = new System.Drawing.Point(519, 50);
            this.btnTambahP.Name = "btnTambahP";
            this.btnTambahP.Size = new System.Drawing.Size(224, 49);
            this.btnTambahP.TabIndex = 27;
            this.btnTambahP.Text = "Tambah";
            this.btnTambahP.UseVisualStyleBackColor = true;
            this.btnTambahP.Click += new System.EventHandler(this.btnTambahP_Click);
            // 
            // plhPelanggan
            // 
            this.plhPelanggan.FormattingEnabled = true;
            this.plhPelanggan.Location = new System.Drawing.Point(233, 50);
            this.plhPelanggan.Name = "plhPelanggan";
            this.plhPelanggan.Size = new System.Drawing.Size(249, 24);
            this.plhPelanggan.TabIndex = 31;
            // 
            // dgvPesanan
            // 
            this.dgvPesanan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPesanan.Location = new System.Drawing.Point(52, 394);
            this.dgvPesanan.Name = "dgvPesanan";
            this.dgvPesanan.RowHeadersWidth = 51;
            this.dgvPesanan.RowTemplate.Height = 24;
            this.dgvPesanan.Size = new System.Drawing.Size(691, 266);
            this.dgvPesanan.TabIndex = 32;
            this.dgvPesanan.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPesanan_CellClick);
            // 
            // BrEkstra
            // 
            this.BrEkstra.AutoSize = true;
            this.BrEkstra.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BrEkstra.Location = new System.Drawing.Point(48, 291);
            this.BrEkstra.Name = "BrEkstra";
            this.BrEkstra.Size = new System.Drawing.Size(95, 20);
            this.BrEkstra.TabIndex = 36;
            this.BrEkstra.Text = "Tambahan";
            // 
            // clbTambahan
            // 
            this.clbTambahan.FormattingEnabled = true;
            this.clbTambahan.Location = new System.Drawing.Point(233, 256);
            this.clbTambahan.Name = "clbTambahan";
            this.clbTambahan.Size = new System.Drawing.Size(249, 106);
            this.clbTambahan.TabIndex = 37;
            // 
            // btnAnalisis
            // 
            this.btnAnalisis.Location = new System.Drawing.Point(519, 329);
            this.btnAnalisis.Name = "btnAnalisis";
            this.btnAnalisis.Size = new System.Drawing.Size(224, 33);
            this.btnAnalisis.TabIndex = 38;
            this.btnAnalisis.Text = "Analisis";
            this.btnAnalisis.UseVisualStyleBackColor = true;
            this.btnAnalisis.Click += new System.EventHandler(this.btnAnalisis_Click);
            // 
            // MasukanPesananForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 691);
            this.Controls.Add(this.btnAnalisis);
            this.Controls.Add(this.clbTambahan);
            this.Controls.Add(this.BrEkstra);
            this.Controls.Add(this.dgvPesanan);
            this.Controls.Add(this.plhPelanggan);
            this.Controls.Add(this.btnRefreshP);
            this.Controls.Add(this.btnUbahP);
            this.Controls.Add(this.btnHapusP);
            this.Controls.Add(this.btnTambahP);
            this.Controls.Add(this.txtBerat);
            this.Controls.Add(this.Berat);
            this.Controls.Add(this.plhLayanan);
            this.Controls.Add(this.tipeLayanan);
            this.Controls.Add(this.status);
            this.Controls.Add(this.plhStatus);
            this.Controls.Add(this.IDPG);
            this.Name = "MasukanPesananForm";
            this.Text = "MasukanPesananForm";
            this.Load += new System.EventHandler(this.MasukanPesananForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPesanan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label IDPG;
        private System.Windows.Forms.ComboBox plhStatus;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.Label tipeLayanan;
        private System.Windows.Forms.ComboBox plhLayanan;
        private System.Windows.Forms.Label Berat;
        private System.Windows.Forms.TextBox txtBerat;
        private System.Windows.Forms.Button btnRefreshP;
        private System.Windows.Forms.Button btnUbahP;
        private System.Windows.Forms.Button btnHapusP;
        private System.Windows.Forms.Button btnTambahP;
        private System.Windows.Forms.ComboBox plhPelanggan;
        private System.Windows.Forms.DataGridView dgvPesanan;
        private System.Windows.Forms.Label BrEkstra;
        private System.Windows.Forms.CheckedListBox clbTambahan;
        private System.Windows.Forms.Button btnAnalisis;
    }
}