
namespace PresentationLayer
{
    partial class HoaDon
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
            this.lblMaDonThuoc = new System.Windows.Forms.Label();
            this.crt_HoaDon = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // lblMaDonThuoc
            // 
            this.lblMaDonThuoc.AutoSize = true;
            this.lblMaDonThuoc.Location = new System.Drawing.Point(-3, -1);
            this.lblMaDonThuoc.Name = "lblMaDonThuoc";
            this.lblMaDonThuoc.Size = new System.Drawing.Size(35, 13);
            this.lblMaDonThuoc.TabIndex = 0;
            this.lblMaDonThuoc.Text = "label1";
            // 
            // crt_HoaDon
            // 
            this.crt_HoaDon.ActiveViewIndex = -1;
            this.crt_HoaDon.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crt_HoaDon.Cursor = System.Windows.Forms.Cursors.Default;
            this.crt_HoaDon.Location = new System.Drawing.Point(0, 12);
            this.crt_HoaDon.Name = "crt_HoaDon";
            this.crt_HoaDon.Size = new System.Drawing.Size(903, 609);
            this.crt_HoaDon.TabIndex = 1;
            // 
            // HoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 622);
            this.Controls.Add(this.crt_HoaDon);
            this.Controls.Add(this.lblMaDonThuoc);
            this.Name = "HoaDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hóa Đơn";
            this.Load += new System.EventHandler(this.HoaDon_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMaDonThuoc;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crt_HoaDon;
    }
}