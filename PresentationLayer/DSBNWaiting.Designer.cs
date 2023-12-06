
namespace PresentationLayer
{
    partial class DSBNWaiting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DSBNWaiting));
            this.dataGridViewDSBNCho = new System.Windows.Forms.DataGridView();
            this.Khambenh = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnThem = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDSBNCho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewDSBNCho
            // 
            this.dataGridViewDSBNCho.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewDSBNCho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDSBNCho.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Khambenh});
            this.dataGridViewDSBNCho.Location = new System.Drawing.Point(40, 136);
            this.dataGridViewDSBNCho.Name = "dataGridViewDSBNCho";
            this.dataGridViewDSBNCho.Size = new System.Drawing.Size(938, 316);
            this.dataGridViewDSBNCho.TabIndex = 0;
            this.dataGridViewDSBNCho.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDSBNCho_CellClick);
            this.dataGridViewDSBNCho.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDSBNCho_CellContentClick);
            this.dataGridViewDSBNCho.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridViewDSBNCho_CellPainting);
            // 
            // Khambenh
            // 
            this.Khambenh.HeaderText = "Chỉ định dịch vụ";
            this.Khambenh.Name = "Khambenh";
            this.Khambenh.Text = "Chỉ định dịch vụ";
            this.Khambenh.UseColumnTextForButtonValue = true;
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.Navy;
            this.btnThem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(40, 86);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(85, 35);
            this.btnThem.TabIndex = 2;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Navy;
            this.label8.Location = new System.Drawing.Point(357, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(299, 25);
            this.label8.TabIndex = 36;
            this.label8.Text = "Danh Sách Bệnh Nhân Chờ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(85, 76);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // DSBNWaiting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1030, 475);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dataGridViewDSBNCho);
            this.Name = "DSBNWaiting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Danh Sách Bệnh Nhân Chờ";
            this.Load += new System.EventHandler(this.DSBNWaiting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDSBNCho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewDSBNCho;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DataGridViewButtonColumn Khambenh;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}