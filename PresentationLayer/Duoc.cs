using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace PresentationLayer
{
    public partial class Duoc : Form
    {
        public Duoc()
        {
            InitializeComponent();
        }
        QLBVService.QLBVSoapClient bll_thuoc = new QLBVService.QLBVSoapClient();
        private void Duoc_Load(object sender, EventArgs e)
        {
            load();
        }
        QLBVService.DTOThuoc thuoc()
        {
            QLBVService.DTOThuoc thuoc = new QLBVService.DTOThuoc();
            thuoc.maThuoc = txbMaThuoc.Text;
            thuoc.tenThuoc = txbTenThuoc.Text;
            thuoc.giaNhap = int.Parse(txbGiaNhap.Text);
            thuoc.giaBan = int.Parse(txbGiaBan.Text);
            thuoc.hangSanXuat = txbHangSX.Text;
            thuoc.nuoc = txbNuoc.Text;
            return thuoc;

        }
        void load()
        {
            DataSet table = bll_thuoc.getThuoc1();
            dataGridView1.DataSource = table.Tables[0];
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;

            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.Columns["MaThuoc"].HeaderText = "Mã Thuốc"; dataGridView1.Columns["MaThuoc"].Width = 150;
            dataGridView1.Columns["TenThuoc"].HeaderText = "Tên Thuốc"; dataGridView1.Columns["TenThuoc"].Width = 200;
            dataGridView1.Columns["GiaNhap"].HeaderText = "Giá Nhập"; dataGridView1.Columns["GiaNhap"].Width = 150;
            dataGridView1.Columns["GiaBan"].HeaderText = "Giá Bán"; dataGridView1.Columns["GiaBan"].Width = 150;
            dataGridView1.Columns["HangSX"].HeaderText = "Hãng Sản Xuất"; dataGridView1.Columns["HangSX"].Width = 150;
            dataGridView1.Columns["Nuoc"].HeaderText = "Nước"; dataGridView1.Columns["Nuoc"].Width = 110;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                txbMaThuoc.Text = dataGridView1.Rows[e.RowIndex].Cells["MaThuoc"].Value.ToString();
                txbTenThuoc.Text = dataGridView1.Rows[e.RowIndex].Cells["TenThuoc"].Value.ToString();
                txbGiaNhap.Text = dataGridView1.Rows[e.RowIndex].Cells["GiaNhap"].Value.ToString();
                txbGiaBan.Text = dataGridView1.Rows[e.RowIndex].Cells["GiaBan"].Value.ToString();
                txbHangSX.Text = dataGridView1.Rows[e.RowIndex].Cells["HangSX"].Value.ToString();
                txbNuoc.Text = dataGridView1.Rows[e.RowIndex].Cells["Nuoc"].Value.ToString();

            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {

            int reg = check();
            switch (reg)
            {
                case 0:
                    if (bll_thuoc.InsertThuoc(thuoc()) == 1)
                    {
                        MessageBox.Show("Lưu Thành Công !", "thông báo", MessageBoxButtons.OK);
                        load();
                        txbMaThuoc.Clear();
                        txbTenThuoc.Clear();
                        txbGiaNhap.Clear();
                        txbGiaBan.Clear();
                        txbHangSX.Clear();
                        txbNuoc.Clear();
                        txbMaThuoc.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Lưu Thất bại", "thông báo", MessageBoxButtons.OK);
                    }
                    break;
                case 1:
                    MessageBox.Show("Thông tin trống, nhập lại!", "thông báo", MessageBoxButtons.OK);
                    break;
                case 2:
                    MessageBox.Show("Thông tin sai định dạng, nhập lại!", "thông báo", MessageBoxButtons.OK);
                    break;
            }
        }
        // 0: nếu nhập đúng định dạng
        // 1: nếu bỏ trống không nhập
        // 2: nhập sai định dạng
        int check()
        {   //biểu thức chính quy kiểm tra lỗi: nếu là lỗi dữ liệu sẽ trả kết quả true, ngược lại là false
            string so = @"[\d]";   // số bất kỳ [0-9]
            string chuoilien = @"[\w]";   //Ký tự chữ, viết ngắn gọn cho[a - zA - Z_0 - 9]
            string chuoikytu = @"[\s\w]";  //Ký tự khoảng trắng, [ \t\n\x0b\r\f]
            if ((Regex.IsMatch(txbTenThuoc.Text, chuoikytu)) && (Regex.IsMatch(txbNuoc.Text, chuoikytu)) && (Regex.IsMatch(txbHangSX.Text, chuoikytu)) && (Regex.IsMatch(txbMaThuoc.Text, chuoilien)) && (Regex.IsMatch(txbGiaNhap.Text, so)) && (Regex.IsMatch(txbGiaBan.Text, so)))
            {
                return 0;
            }
            else if (string.IsNullOrEmpty(txbTenThuoc.Text) || string.IsNullOrEmpty(txbNuoc.Text) || string.IsNullOrEmpty(txbHangSX.Text) || string.IsNullOrEmpty(txbMaThuoc.Text) || string.IsNullOrEmpty(txbGiaNhap.Text) || string.IsNullOrEmpty(txbGiaBan.Text))
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int reg = check();
            switch (reg)
            {
                case 0:
                    if (bll_thuoc.UpdateThuoc(thuoc()) >= 1) 
                    {
                        MessageBox.Show("Cập nhật thành công!", "thông báo", MessageBoxButtons.OK);
                        load();
                        txbMaThuoc.Clear();
                        txbTenThuoc.Clear();
                        txbGiaNhap.Clear();
                        txbGiaBan.Clear();
                        txbHangSX.Clear();
                        txbNuoc.Clear();
                        txbMaThuoc.Focus();
                    } ;
                    break;
                case 1:
                    MessageBox.Show("Thông tin trống, nhập lại!", "thông báo", MessageBoxButtons.OK);
                    break;
                case 2:
                    MessageBox.Show("Thông tin sai định dạng, nhập lại!", "thông báo", MessageBoxButtons.OK);
                    break;
            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            txbMaThuoc.Clear();
            txbTenThuoc.Clear();
            txbGiaNhap.Clear();
            txbGiaBan.Clear();
            txbHangSX.Clear();
            txbNuoc.Clear();
            txbMaThuoc.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txbMaThuoc.Text))
            {
                MessageBox.Show("Bạn chưa chọn Thuốc!", "thông báo", MessageBoxButtons.OKCancel);
            }
            else
            {
                DialogResult tb = MessageBox.Show("Bạn có muốn xóa không !", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(tb == DialogResult.Yes)
                {
                    if(bll_thuoc.DeleteThuoc(txbMaThuoc.Text) == 1)
                    {
                        MessageBox.Show("Xóa thành công!", "thông báo", MessageBoxButtons.OKCancel);
                        txbMaThuoc.Clear();
                        txbTenThuoc.Clear();
                        txbGiaNhap.Clear();
                        txbGiaBan.Clear();
                        txbHangSX.Clear();
                        txbNuoc.Clear();
                        txbMaThuoc.Focus();
                    }
                    else
                    {
                        MessageBox.Show("Xóa thất bại!", "thông báo", MessageBoxButtons.OKCancel);
                    }
                }
                load();
            }
               
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}