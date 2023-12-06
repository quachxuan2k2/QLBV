using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLogicLayer;
namespace PresentationLayer
{
    public partial class BS : Form
    {
        public BS()
        {
            InitializeComponent();
        }
        private void BS_Load(object sender, EventArgs e)
        {
            QLBVService.QLBVSoapClient bll_bacsi = new QLBVService.QLBVSoapClient();
            DataSet table = bll_bacsi.getBS();
            dataGridView1.DataSource = table.Tables[0];
            dataGridView1.Columns["MaBS"].Visible = false;
            dataGridView1.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;

            dataGridView1.Columns["HoTen"].HeaderText = "Họ Tên";          dataGridView1.Columns["HoTen"].Width = 160;
            dataGridView1.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dataGridView1.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dataGridView1.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dataGridView1.Columns["CCHN"].HeaderText = "CCHN";
            dataGridView1.Columns["MaKhoa"].HeaderText = "Khoa";
            dataGridView1.Columns["MaDV"].HeaderText = "Dịch Vụ";
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }

        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells["STT"].Value = e.RowIndex + 1;
        }
    }
}
