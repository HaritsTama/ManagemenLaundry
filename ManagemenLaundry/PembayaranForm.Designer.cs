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
            this.plhSts = new System.Windows.Forms.ComboBox();
            this.tipeLayanan = new System.Windows.Forms.Label();
            this.status = new System.Windows.Forms.Label();
            this.plhMtd = new System.Windows.Forms.ComboBox();
            this.btnRefreshPe = new System.Windows.Forms.Button();
            this.btnUbahPe = new System.Windows.Forms.Button();
            this.btnHapusPe = new System.Windows.Forms.Button();
            this.btnTambahPe = new System.Windows.Forms.Button();
            this.dgvPembayaran = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbPsn = new System.Windows.Forms.ComboBox();
            this.btnAnalisis = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPembayaran)).BeginInit();
            this.SuspendLayout();
            // 
            // plhSts
            // 
            this.plhSts.FormattingEnabled = true;
            this.plhSts.Items.AddRange(new object[] {
            "Lunas",
            "Belum Bayar"});
            this.plhSts.Location = new System.Drawing.Point(240, 230);
            this.plhSts.Name = "plhSts";
            this.plhSts.Size = new System.Drawing.Size(249, 24);
            this.plhSts.TabIndex = 41;
            // 
            // tipeLayanan
            // 
            this.tipeLayanan.AutoSize = true;
            this.tipeLayanan.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tipeLayanan.Location = new System.Drawing.Point(55, 230);
            this.tipeLayanan.Name = "tipeLayanan";
            this.tipeLayanan.Size = new System.Drawing.Size(63, 20);
            this.tipeLayanan.TabIndex = 40;
            this.tipeLayanan.Text = "Status";
            // 
            // status
            // 
            this.status.AutoSize = true;
            this.status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.status.Location = new System.Drawing.Point(55, 134);
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
            "Qris"});
            this.plhMtd.Location = new System.Drawing.Point(240, 134);
            this.plhMtd.Name = "plhMtd";
            this.plhMtd.Size = new System.Drawing.Size(249, 24);
            this.plhMtd.TabIndex = 38;
            // 
            // btnRefreshPe
            // 
            this.btnRefreshPe.Location = new System.Drawing.Point(561, 178);
            this.btnRefreshPe.Name = "btnRefreshPe";
            this.btnRefreshPe.Size = new System.Drawing.Size(186, 35);
            this.btnRefreshPe.TabIndex = 49;
            this.btnRefreshPe.Text = "Refresh";
            this.btnRefreshPe.UseVisualStyleBackColor = true;
            this.btnRefreshPe.Click += new System.EventHandler(this.btnRefreshPe_Click);
            // 
            // btnUbahPe
            // 
            this.btnUbahPe.Location = new System.Drawing.Point(561, 131);
            this.btnUbahPe.Name = "btnUbahPe";
            this.btnUbahPe.Size = new System.Drawing.Size(186, 35);
            this.btnUbahPe.TabIndex = 48;
            this.btnUbahPe.Text = "Ubah";
            this.btnUbahPe.UseVisualStyleBackColor = true;
            this.btnUbahPe.Click += new System.EventHandler(this.btnUbahPe_Click);
            // 
            // btnHapusPe
            // 
            this.btnHapusPe.Location = new System.Drawing.Point(561, 83);
            this.btnHapusPe.Name = "btnHapusPe";
            this.btnHapusPe.Size = new System.Drawing.Size(186, 35);
            this.btnHapusPe.TabIndex = 47;
            this.btnHapusPe.Text = "Hapus";
            this.btnHapusPe.UseVisualStyleBackColor = true;
            this.btnHapusPe.Click += new System.EventHandler(this.btnHapusPe_Click);
            // 
            // btnTambahPe
            // 
            this.btnTambahPe.Location = new System.Drawing.Point(561, 37);
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
            this.dgvPembayaran.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPembayaran_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(55, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 20);
            this.label1.TabIndex = 52;
            this.label1.Text = "Pesanan";
            // 
            // cmbPsn
            // 
            this.cmbPsn.FormattingEnabled = true;
            this.cmbPsn.Location = new System.Drawing.Point(240, 39);
            this.cmbPsn.Name = "cmbPsn";
            this.cmbPsn.Size = new System.Drawing.Size(249, 24);
            this.cmbPsn.TabIndex = 53;
            this.cmbPsn.SelectedIndexChanged += new System.EventHandler(this.cmbPsn_SelectedIndexChanged);
            // 
            // btnAnalisis
            // 
            this.btnAnalisis.Location = new System.Drawing.Point(561, 224);
            this.btnAnalisis.Name = "btnAnalisis";
            this.btnAnalisis.Size = new System.Drawing.Size(186, 35);
            this.btnAnalisis.TabIndex = 54;
            this.btnAnalisis.Text = "Analisis";
            this.btnAnalisis.UseVisualStyleBackColor = true;
            this.btnAnalisis.Click += new System.EventHandler(this.btnAnalisis_Click_1);
            // 
            // PembayaranForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 496);
            this.Controls.Add(this.btnAnalisis);
            this.Controls.Add(this.cmbPsn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvPembayaran);
            this.Controls.Add(this.btnRefreshPe);
            this.Controls.Add(this.btnUbahPe);
            this.Controls.Add(this.btnHapusPe);
            this.Controls.Add(this.btnTambahPe);
            this.Controls.Add(this.plhSts);
            this.Controls.Add(this.tipeLayanan);
            this.Controls.Add(this.status);
            this.Controls.Add(this.plhMtd);
            this.Name = "PembayaranForm";
            this.Text = "PembayaranForm";
            this.Load += new System.EventHandler(this.PembayaranForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPembayaran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox plhSts;
        private System.Windows.Forms.Label tipeLayanan;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.ComboBox plhMtd;
        private System.Windows.Forms.Button btnRefreshPe;
        private System.Windows.Forms.Button btnUbahPe;
        private System.Windows.Forms.Button btnHapusPe;
        private System.Windows.Forms.Button btnTambahPe;
        private System.Windows.Forms.DataGridView dgvPembayaran;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbPsn;
        private System.Windows.Forms.Button btnAnalisis;
    }
}