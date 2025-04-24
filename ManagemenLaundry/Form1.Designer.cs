namespace ManagemenLaundry
{
    partial class Form1
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
            this.MenuUtama = new System.Windows.Forms.Label();
            this.BtnPelangBaru = new System.Windows.Forms.Button();
            this.BtnBuatPesan = new System.Windows.Forms.Button();
            this.BtnPembayaran = new System.Windows.Forms.Button();
            this.btnTambah = new System.Windows.Forms.Button();
            this.btnBarang = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MenuUtama
            // 
            this.MenuUtama.AutoSize = true;
            this.MenuUtama.Font = new System.Drawing.Font("Noto Sans JP Medium", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MenuUtama.Location = new System.Drawing.Point(307, 9);
            this.MenuUtama.Name = "MenuUtama";
            this.MenuUtama.Size = new System.Drawing.Size(164, 34);
            this.MenuUtama.TabIndex = 0;
            this.MenuUtama.Text = "MENU UTAMA";
            // 
            // BtnPelangBaru
            // 
            this.BtnPelangBaru.Font = new System.Drawing.Font("Garamond", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPelangBaru.Location = new System.Drawing.Point(235, 70);
            this.BtnPelangBaru.Name = "BtnPelangBaru";
            this.BtnPelangBaru.Size = new System.Drawing.Size(317, 48);
            this.BtnPelangBaru.TabIndex = 1;
            this.BtnPelangBaru.Text = "Pelanggan Baru";
            this.BtnPelangBaru.UseVisualStyleBackColor = true;
            this.BtnPelangBaru.Click += new System.EventHandler(this.BtnPelangBaru_Click);
            // 
            // BtnBuatPesan
            // 
            this.BtnBuatPesan.Font = new System.Drawing.Font("Garamond", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBuatPesan.Location = new System.Drawing.Point(235, 140);
            this.BtnBuatPesan.Name = "BtnBuatPesan";
            this.BtnBuatPesan.Size = new System.Drawing.Size(317, 48);
            this.BtnBuatPesan.TabIndex = 2;
            this.BtnBuatPesan.Text = "Buat Pesanan";
            this.BtnBuatPesan.UseVisualStyleBackColor = true;
            this.BtnBuatPesan.Click += new System.EventHandler(this.BtnBuatPesan_Click);
            // 
            // BtnPembayaran
            // 
            this.BtnPembayaran.Font = new System.Drawing.Font("Garamond", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPembayaran.Location = new System.Drawing.Point(235, 210);
            this.BtnPembayaran.Name = "BtnPembayaran";
            this.BtnPembayaran.Size = new System.Drawing.Size(317, 48);
            this.BtnPembayaran.TabIndex = 3;
            this.BtnPembayaran.Text = "Pembayaran";
            this.BtnPembayaran.UseVisualStyleBackColor = true;
            this.BtnPembayaran.Click += new System.EventHandler(this.BtnPembayaran_Click);
            // 
            // btnTambah
            // 
            this.btnTambah.Font = new System.Drawing.Font("Garamond", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTambah.Location = new System.Drawing.Point(235, 279);
            this.btnTambah.Name = "btnTambah";
            this.btnTambah.Size = new System.Drawing.Size(317, 48);
            this.btnTambah.TabIndex = 4;
            this.btnTambah.Text = "Tambah Layanan";
            this.btnTambah.UseVisualStyleBackColor = true;
            this.btnTambah.Click += new System.EventHandler(this.btnTambah_Click);
            // 
            // btnBarang
            // 
            this.btnBarang.Font = new System.Drawing.Font("Garamond", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBarang.Location = new System.Drawing.Point(235, 350);
            this.btnBarang.Name = "btnBarang";
            this.btnBarang.Size = new System.Drawing.Size(317, 48);
            this.btnBarang.TabIndex = 5;
            this.btnBarang.Text = "Tambah Barang";
            this.btnBarang.UseVisualStyleBackColor = true;
            this.btnBarang.Click += new System.EventHandler(this.btnBarang_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBarang);
            this.Controls.Add(this.btnTambah);
            this.Controls.Add(this.BtnPembayaran);
            this.Controls.Add(this.BtnBuatPesan);
            this.Controls.Add(this.BtnPelangBaru);
            this.Controls.Add(this.MenuUtama);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label MenuUtama;
        private System.Windows.Forms.Button BtnPelangBaru;
        private System.Windows.Forms.Button BtnBuatPesan;
        private System.Windows.Forms.Button BtnPembayaran;
        private System.Windows.Forms.Button btnTambah;
        private System.Windows.Forms.Button btnBarang;
    }
}

