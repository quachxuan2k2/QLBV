using BusinessLogicLayer;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace PresentationLayer
{
    public partial class DSBN : Form
    {
        public DSBN()
        {
            InitializeComponent();
        }
        QLBVService.QLBVSoapClient bllkhoa = new QLBVService.QLBVSoapClient();
        void loadKhoa()
        {
            DataSet table = bllkhoa.GetAllKhoa();
            cbb_Khoa.DataSource = table.Tables[0];
            cbb_Khoa.DisplayMember = "TenKhoa";
            cbb_Khoa.ValueMember = "MaKhoa";
            string makhoa = cbb_Khoa.SelectedValue.ToString();
            loadBN(makhoa);
        }
        void loadBN(string makhoa)
        {
            DataSet table = bllkhoa.GetAllBenhNhanByKhoa(makhoa);
            dataGridView1.DataSource = table.Tables[0];
            dataGridView1.Columns["MaBN"].Visible = false;
            dataGridView1.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;

            dataGridView1.Columns["HoTen"].HeaderText = "Họ Tên";               dataGridView1.Columns["HoTen"].Width = 160;
            dataGridView1.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dataGridView1.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dataGridView1.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dataGridView1.Columns["DienThoai"].HeaderText = "Điện Thoại";
            dataGridView1.Columns["MaBHYT"].HeaderText = "Mã BHYT";
            dataGridView1.Columns["TrieuChung"].HeaderText = "Triệu Chứng";         dataGridView1.Columns["TrieuChung"].Width = 200;
            dataGridView1.Columns["MaKhoa"].HeaderText = "Khoa";
            dataGridView1.Columns["TrangThai"].HeaderText = "Trạng Thái";
        }
        private void DSBN_Load(object sender, EventArgs e)
        {
            loadKhoa();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AddBN addbn = new AddBN();
            addbn.Show();
        }
        private void cbb_Khoa_SelectedValueChanged(object sender, EventArgs e)
        {
            string makhoa = cbb_Khoa.SelectedValue.ToString();
            loadBN(makhoa);
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }
    }
}