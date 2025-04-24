namespace ManagemenLaundry
{
    partial class TambahLayananForm
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
            this.txtIDLY = new System.Windows.Forms.TextBox();
            this.IDLY = new System.Windows.Forms.Label();
            this.txtHLY = new System.Windows.Forms.TextBox();
            this.txtNLY = new System.Windows.Forms.TextBox();
            this.HargaLY = new System.Windows.Forms.Label();
            this.NamaLY = new System.Windows.Forms.Label();
            this.btnRefreshL = new System.Windows.Forms.Button();
            this.btnUbahL = new System.Windows.Forms.Button();
            this.btnHapusL = new System.Windows.Forms.Button();
            this.btnTambahL = new System.Windows.Forms.Button();
            this.dgvLayanan = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayanan)).BeginInit();
            this.SuspendLayout();
            // 
            // txtIDLY
            // 
            this.txtIDLY.Location = new System.Drawing.Point(199, 48);
            this.txtIDLY.Name = "txtIDLY";
            this.txtIDLY.Size = new System.Drawing.Size(249, 22);
            this.txtIDLY.TabIndex = 6;
            // 
            // IDLY
            // 
            this.IDLY.AutoSize = true;
            this.IDLY.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IDLY.Location = new System.Drawing.Point(30, 48);
            this.IDLY.Name = "IDLY";
            this.IDLY.Size = new System.Drawing.Size(100, 20);
            this.IDLY.TabIndex = 5;
            this.IDLY.Text = "ID (LY###)";
            // 
            // txtHLY
            // 
            this.txtHLY.Location = new System.Drawing.Point(199, 185);
            this.txtHLY.Name = "txtHLY";
            this.txtHLY.Size = new System.Drawing.Size(249, 22);
            this.txtHLY.TabIndex = 10;
            // 
            // txtNLY
            // 
            this.txtNLY.Location = new System.Drawing.Point(199, 118);
            this.txtNLY.Name = "txtNLY";
            this.txtNLY.Size = new System.Drawing.Size(249, 22);
            this.txtNLY.TabIndex = 9;
            // 
            // HargaLY
            // 
            this.HargaLY.AutoSize = true;
            this.HargaLY.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HargaLY.Location = new System.Drawing.Point(30, 185);
            this.HargaLY.Name = "HargaLY";
            this.HargaLY.Size = new System.Drawing.Size(60, 20);
            this.HargaLY.TabIndex = 8;
            this.HargaLY.Text = "Harga";
            // 
            // NamaLY
            // 
            this.NamaLY.AutoSize = true;
            this.NamaLY.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NamaLY.Location = new System.Drawing.Point(30, 118);
            this.NamaLY.Name = "NamaLY";
            this.NamaLY.Size = new System.Drawing.Size(57, 20);
            this.NamaLY.TabIndex = 7;
            this.NamaLY.Text = "Nama";
            // 
            // btnRefreshL
            // 
            this.btnRefreshL.Location = new System.Drawing.Point(536, 185);
            this.btnRefreshL.Name = "btnRefreshL";
            this.btnRefreshL.Size = new System.Drawing.Size(186, 23);
            this.btnRefreshL.TabIndex = 15;
            this.btnRefreshL.Text = "Refresh";
            this.btnRefreshL.UseVisualStyleBackColor = true;
            this.btnRefreshL.Click += new System.EventHandler(this.btnRefreshL_Click);
            // 
            // btnUbahL
            // 
            this.btnUbahL.Location = new System.Drawing.Point(536, 140);
            this.btnUbahL.Name = "btnUbahL";
            this.btnUbahL.Size = new System.Drawing.Size(186, 23);
            this.btnUbahL.TabIndex = 14;
            this.btnUbahL.Text = "Ubah";
            this.btnUbahL.UseVisualStyleBackColor = true;
            this.btnUbahL.Click += new System.EventHandler(this.btnUbahL_Click);
            // 
            // btnHapusL
            // 
            this.btnHapusL.Location = new System.Drawing.Point(536, 95);
            this.btnHapusL.Name = "btnHapusL";
            this.btnHapusL.Size = new System.Drawing.Size(186, 23);
            this.btnHapusL.TabIndex = 13;
            this.btnHapusL.Text = "Hapus";
            this.btnHapusL.UseVisualStyleBackColor = true;
            this.btnHapusL.Click += new System.EventHandler(this.btnHapusL_Click);
            // 
            // btnTambahL
            // 
            this.btnTambahL.Location = new System.Drawing.Point(536, 48);
            this.btnTambahL.Name = "btnTambahL";
            this.btnTambahL.Size = new System.Drawing.Size(186, 23);
            this.btnTambahL.TabIndex = 12;
            this.btnTambahL.Text = "Tambah";
            this.btnTambahL.UseVisualStyleBackColor = true;
            this.btnTambahL.Click += new System.EventHandler(this.btnTambahL_Click);
            // 
            // dgvLayanan
            // 
            this.dgvLayanan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLayanan.Location = new System.Drawing.Point(34, 248);
            this.dgvLayanan.Name = "dgvLayanan";
            this.dgvLayanan.RowHeadersWidth = 51;
            this.dgvLayanan.RowTemplate.Height = 24;
            this.dgvLayanan.Size = new System.Drawing.Size(688, 173);
            this.dgvLayanan.TabIndex = 16;
            this.dgvLayanan.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLayanan_CellContentClick);
            // 
            // TambahLayananForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvLayanan);
            this.Controls.Add(this.btnRefreshL);
            this.Controls.Add(this.btnUbahL);
            this.Controls.Add(this.btnHapusL);
            this.Controls.Add(this.btnTambahL);
            this.Controls.Add(this.txtHLY);
            this.Controls.Add(this.txtNLY);
            this.Controls.Add(this.HargaLY);
            this.Controls.Add(this.NamaLY);
            this.Controls.Add(this.txtIDLY);
            this.Controls.Add(this.IDLY);
            this.Name = "TambahLayananForm";
            this.Text = "TambahLayananForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvLayanan)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIDLY;
        private System.Windows.Forms.Label IDLY;
        private System.Windows.Forms.TextBox txtHLY;
        private System.Windows.Forms.TextBox txtNLY;
        private System.Windows.Forms.Label HargaLY;
        private System.Windows.Forms.Label NamaLY;
        private System.Windows.Forms.Button btnRefreshL;
        private System.Windows.Forms.Button btnUbahL;
        private System.Windows.Forms.Button btnHapusL;
        private System.Windows.Forms.Button btnTambahL;
        private System.Windows.Forms.DataGridView dgvLayanan;
    }
}