namespace ManagemenLaundry
{
    partial class TambahPelangganForm
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
            this.NamaPG = new System.Windows.Forms.Label();
            this.TeleponPG = new System.Windows.Forms.Label();
            this.EmailPG = new System.Windows.Forms.Label();
            this.txtNPG = new System.Windows.Forms.TextBox();
            this.txtEPG = new System.Windows.Forms.TextBox();
            this.txtTPG = new System.Windows.Forms.TextBox();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnHapus = new System.Windows.Forms.Button();
            this.btnUbah = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgvPelanggan = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPelanggan)).BeginInit();
            this.SuspendLayout();
            // 
            // NamaPG
            // 
            this.NamaPG.AutoSize = true;
            this.NamaPG.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NamaPG.Location = new System.Drawing.Point(42, 34);
            this.NamaPG.Name = "NamaPG";
            this.NamaPG.Size = new System.Drawing.Size(57, 20);
            this.NamaPG.TabIndex = 1;
            this.NamaPG.Text = "Nama";
            // 
            // TeleponPG
            // 
            this.TeleponPG.AutoSize = true;
            this.TeleponPG.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TeleponPG.Location = new System.Drawing.Point(42, 171);
            this.TeleponPG.Name = "TeleponPG";
            this.TeleponPG.Size = new System.Drawing.Size(75, 20);
            this.TeleponPG.TabIndex = 2;
            this.TeleponPG.Text = "Telepon";
            // 
            // EmailPG
            // 
            this.EmailPG.AutoSize = true;
            this.EmailPG.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmailPG.Location = new System.Drawing.Point(42, 104);
            this.EmailPG.Name = "EmailPG";
            this.EmailPG.Size = new System.Drawing.Size(56, 20);
            this.EmailPG.TabIndex = 3;
            this.EmailPG.Text = "Email";
            // 
            // txtNPG
            // 
            this.txtNPG.Location = new System.Drawing.Point(211, 34);
            this.txtNPG.Name = "txtNPG";
            this.txtNPG.Size = new System.Drawing.Size(249, 22);
            this.txtNPG.TabIndex = 5;
            // 
            // txtEPG
            // 
            this.txtEPG.Location = new System.Drawing.Point(211, 104);
            this.txtEPG.Name = "txtEPG";
            this.txtEPG.Size = new System.Drawing.Size(249, 22);
            this.txtEPG.TabIndex = 6;
            // 
            // txtTPG
            // 
            this.txtTPG.Location = new System.Drawing.Point(211, 171);
            this.txtTPG.Name = "txtTPG";
            this.txtTPG.Size = new System.Drawing.Size(249, 22);
            this.txtTPG.TabIndex = 7;
            // 
            // btnTambah
            // 
            this.btnTambah.Location = new System.Drawing.Point(532, 34);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(186, 23);
            this.btnTambah.TabIndex = 8;
            this.btnTambah.Text = "Tambah";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnHapus
            // 
            this.btnHapus.Location = new System.Drawing.Point(532, 81);
            this.btnHapus.Name = "btnHapus";
            this.btnHapus.Size = new System.Drawing.Size(186, 23);
            this.btnHapus.TabIndex = 9;
            this.btnHapus.Text = "Hapus";
            this.btnHapus.UseVisualStyleBackColor = true;
            this.btnHapus.Click += new System.EventHandler(this.btnHapus_Click);
            // 
            // btnUbah
            // 
            this.btnUbah.Location = new System.Drawing.Point(532, 126);
            this.btnUbah.Name = "btnUbah";
            this.btnUbah.Size = new System.Drawing.Size(186, 23);
            this.btnUbah.TabIndex = 10;
            this.btnUbah.Text = "Ubah";
            this.btnUbah.UseVisualStyleBackColor = true;
            this.btnUbah.Click += new System.EventHandler(this.btnUbah_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(532, 171);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(186, 23);
            this.btnRefresh.TabIndex = 11;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvPelanggan
            // 
            this.dgvPelanggan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPelanggan.Location = new System.Drawing.Point(46, 220);
            this.dgvPelanggan.Name = "dgvPelanggan";
            this.dgvPelanggan.RowHeadersWidth = 51;
            this.dgvPelanggan.RowTemplate.Height = 24;
            this.dgvPelanggan.Size = new System.Drawing.Size(672, 204);
            this.dgvPelanggan.TabIndex = 12;
            this.dgvPelanggan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPelanggan_CellContentClick);
            // 
            // TambahPelangganForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvPelanggan);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnUbah);
            this.Controls.Add(this.btnHapus);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.txtTPG);
            this.Controls.Add(this.txtEPG);
            this.Controls.Add(this.txtNPG);
            this.Controls.Add(this.EmailPG);
            this.Controls.Add(this.TeleponPG);
            this.Controls.Add(this.NamaPG);
            this.Name = "TambahPelangganForm";
            this.Text = "TambahPelangganForm";
            this.Load += new System.EventHandler(this.TambahPelangganForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPelanggan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label NamaPG;
        private System.Windows.Forms.Label TeleponPG;
        private System.Windows.Forms.Label EmailPG;
        private System.Windows.Forms.TextBox txtNPG;
        private System.Windows.Forms.TextBox txtEPG;
        private System.Windows.Forms.TextBox txtTPG;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnHapus;
        private System.Windows.Forms.Button btnUbah;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvPelanggan;
    }
}