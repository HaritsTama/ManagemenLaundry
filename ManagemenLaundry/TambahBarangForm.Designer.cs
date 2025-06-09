namespace ManagemenLaundry
{
    partial class TambahBarangForm
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
            this.dgvBarang = new System.Windows.Forms.DataGridView();
            this.btnRefreshB = new System.Windows.Forms.Button();
            this.btnUbahB = new System.Windows.Forms.Button();
            this.btnHapusB = new System.Windows.Forms.Button();
            this.btnTambahB = new System.Windows.Forms.Button();
            this.txtHBR = new System.Windows.Forms.TextBox();
            this.txtNBR = new System.Windows.Forms.TextBox();
            this.HargaBR = new System.Windows.Forms.Label();
            this.NamaBR = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarang)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvBarang
            // 
            this.dgvBarang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBarang.Location = new System.Drawing.Point(58, 239);
            this.dgvBarang.Name = "dgvBarang";
            this.dgvBarang.RowHeadersWidth = 51;
            this.dgvBarang.RowTemplate.Height = 24;
            this.dgvBarang.Size = new System.Drawing.Size(688, 173);
            this.dgvBarang.TabIndex = 27;
            this.dgvBarang.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBarang_CellContentClick);
            // 
            // btnRefreshB
            // 
            this.btnRefreshB.Location = new System.Drawing.Point(560, 176);
            this.btnRefreshB.Name = "btnRefreshB";
            this.btnRefreshB.Size = new System.Drawing.Size(186, 23);
            this.btnRefreshB.TabIndex = 26;
            this.btnRefreshB.Text = "Refresh";
            this.btnRefreshB.UseVisualStyleBackColor = true;
            this.btnRefreshB.Click += new System.EventHandler(this.btnRefreshB_Click);
            // 
            // btnUbahB
            // 
            this.btnUbahB.Location = new System.Drawing.Point(560, 131);
            this.btnUbahB.Name = "btnUbahB";
            this.btnUbahB.Size = new System.Drawing.Size(186, 23);
            this.btnUbahB.TabIndex = 25;
            this.btnUbahB.Text = "Ubah";
            this.btnUbahB.UseVisualStyleBackColor = true;
            this.btnUbahB.Click += new System.EventHandler(this.btnUbahB_Click);
            // 
            // btnHapusB
            // 
            this.btnHapusB.Location = new System.Drawing.Point(560, 86);
            this.btnHapusB.Name = "btnHapusB";
            this.btnHapusB.Size = new System.Drawing.Size(186, 23);
            this.btnHapusB.TabIndex = 24;
            this.btnHapusB.Text = "Hapus";
            this.btnHapusB.UseVisualStyleBackColor = true;
            this.btnHapusB.Click += new System.EventHandler(this.btnHapusB_Click);
            // 
            // btnTambahB
            // 
            this.btnTambahB.Location = new System.Drawing.Point(560, 39);
            this.btnTambahB.Name = "btnTambahB";
            this.btnTambahB.Size = new System.Drawing.Size(186, 23);
            this.btnTambahB.TabIndex = 23;
            this.btnTambahB.Text = "Tambah";
            this.btnTambahB.UseVisualStyleBackColor = true;
            this.btnTambahB.Click += new System.EventHandler(this.btnTambahB_Click);
            // 
            // txtHBR
            // 
            this.txtHBR.Location = new System.Drawing.Point(223, 152);
            this.txtHBR.Name = "txtHBR";
            this.txtHBR.Size = new System.Drawing.Size(249, 22);
            this.txtHBR.TabIndex = 22;
            // 
            // txtNBR
            // 
            this.txtNBR.Location = new System.Drawing.Point(223, 56);
            this.txtNBR.Name = "txtNBR";
            this.txtNBR.Size = new System.Drawing.Size(249, 22);
            this.txtNBR.TabIndex = 21;
            // 
            // HargaBR
            // 
            this.HargaBR.AutoSize = true;
            this.HargaBR.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HargaBR.Location = new System.Drawing.Point(54, 152);
            this.HargaBR.Name = "HargaBR";
            this.HargaBR.Size = new System.Drawing.Size(60, 20);
            this.HargaBR.TabIndex = 20;
            this.HargaBR.Text = "Harga";
            // 
            // NamaBR
            // 
            this.NamaBR.AutoSize = true;
            this.NamaBR.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NamaBR.Location = new System.Drawing.Point(54, 56);
            this.NamaBR.Name = "NamaBR";
            this.NamaBR.Size = new System.Drawing.Size(57, 20);
            this.NamaBR.TabIndex = 19;
            this.NamaBR.Text = "Nama";
            // 
            // TambahBarangForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvBarang);
            this.Controls.Add(this.btnRefreshB);
            this.Controls.Add(this.btnUbahB);
            this.Controls.Add(this.btnHapusB);
            this.Controls.Add(this.btnTambahB);
            this.Controls.Add(this.txtHBR);
            this.Controls.Add(this.txtNBR);
            this.Controls.Add(this.HargaBR);
            this.Controls.Add(this.NamaBR);
            this.Name = "TambahBarangForm";
            this.Text = "TambahBarangForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvBarang)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvBarang;
        private System.Windows.Forms.Button btnRefreshB;
        private System.Windows.Forms.Button btnUbahB;
        private System.Windows.Forms.Button btnHapusB;
        private System.Windows.Forms.Button btnTambahB;
        private System.Windows.Forms.TextBox txtHBR;
        private System.Windows.Forms.TextBox txtNBR;
        private System.Windows.Forms.Label HargaBR;
        private System.Windows.Forms.Label NamaBR;
    }
}