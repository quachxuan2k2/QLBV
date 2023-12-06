using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataTransferObject;
using BusinessLogicLayer;
using System.Text.RegularExpressions;

namespace PresentationLayer
{
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();
        }
        QLBVService.DTOTaiKhoan getTaiKhoan()
        {
            QLBVService.DTOTaiKhoan taikhoan = new QLBVService.DTOTaiKhoan();
            taikhoan.tenDangNhap = txtTenDangNhap.Text;
            taikhoan.matKhau = txtMatKhau.Text;
            taikhoan.MaBS = txtMaBS.Text;
            
            return taikhoan;
        }
        QLBVService.QLBVSoapClient bll_user = new QLBVService.QLBVSoapClient();
        private void btnThem_Click(object sender, EventArgs e)
        {
            int reg = check();
            switch (reg)
            {
                case 0:
                    if (txtMaBS.Visible == false)
                    {
                        int result = bll_user.InsertUser(getTaiKhoan());
                        if (result == 0)
                        {
                            lblThongBao.Text = "Tài khoản đã tồn tại, nhập lại !";
                            txtTenDangNhap.Focus();
                        }
                        else
                        {
                            lblThongBao.Text = "Tạo tài khoản thành công !";
                            txtTenDangNhap.Clear();
                            txtMatKhau.Clear();
                            txtMaBS.Clear();
                            load();
                        }
                    }
                    else
                    {
                        int result = bll_user.InsertUserIsBS(getTaiKhoan());
                        if (result == 0)
                        {
                            lblThongBao.Text = "Tài khoản đã tồn tại, nhập lại !";
                            txtTenDangNhap.Focus();
                        }
                        else
                        {
                            lblThongBao.Text = "Tạo tài khoản thành công !";
                        }
                    };
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
            string chuoilien = @"[\w]";   //Ký tự chữ, viết ngắn gọn cho[a - zA - Z_0 - 9]
            if ((Regex.IsMatch(txtTenDangNhap.Text, chuoilien)) && (Regex.IsMatch(txtMatKhau.Text, chuoilien)) && cbLoaiTaiKhoan.SelectedIndex != -1)
            {
                return 0;
            }
            else if (string.IsNullOrEmpty(txtTenDangNhap.Text) || string.IsNullOrEmpty(txtMatKhau.Text) || cbLoaiTaiKhoan.SelectedIndex == -1)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        void load()
        {
            // DataTable table = bll_user.getalluser();

            DataSet table = bll_user.getalluser();
            dgvUser.DataSource = table.Tables[0];
            dgvUser.Columns["MaTK"].Visible = false;
            dgvUser.EnableHeadersVisualStyles = false;
            dgvUser.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dgvUser.ColumnHeadersHeight = 40;
            dgvUser.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;

            dgvUser.Columns["TenDangNhap"].HeaderText = "Tên Đăng Nhập";    dgvUser.Columns["TenDangNhap"].Width = 140;
            dgvUser.Columns["MaBS"].HeaderText = "Mã Bác Sĩ";               dgvUser.Columns["MaBS"].Width = 140;
            dgvUser.Columns["MatKhau"].HeaderText = "Mật Khẩu";
        }
        private void AddUser_Load(object sender, EventArgs e)
        {
            load();
        }
        private void cbLoaiTaiKhoan_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (cbLoaiTaiKhoan.SelectedItem.ToString() == "Khác")
            {
                txtMaBS.Visible = false;
            }
            else 
            {
                txtMaBS.Visible = true;
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult tb = MessageBox.Show("Bạn có muốn thoát!", "thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (tb == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void dgvUser_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
           
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                lblmatk.Text = dgvUser.Rows[e.RowIndex].Cells["MaTK"].Value.ToString();
                txtTenDangNhap.Text = dgvUser.Rows[e.RowIndex].Cells["TenDangNhap"].Value.ToString();
                txtMatKhau.Text = dgvUser.Rows[e.RowIndex].Cells["MatKhau"].Value.ToString();
                txtMaBS.Text = dgvUser.Rows[e.RowIndex].Cells["MaBS"].Value.ToString();
            }
        }
        QLBVService.DTOTaiKhoan taikhoan()
        {
            QLBVService.DTOTaiKhoan taikhoan = new QLBVService.DTOTaiKhoan();
            taikhoan.maTK = int.Parse( lblmatk.Text);
            taikhoan.tenDangNhap = txtTenDangNhap.Text;
            taikhoan.matKhau = txtMatKhau.Text;
            taikhoan.MaBS = txtMaBS.Text;
            return taikhoan;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenDangNhap.Text))
            {
                MessageBox.Show("Bạn Chưa Chọn Tài Khoản !", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            else
            {
                if (string.IsNullOrEmpty(txtMaBS.Text))
                {
                    if (bll_user.UpdateUser(taikhoan()) >= 1)
                    {
                        MessageBox.Show("Cập nhật thành công !", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        load();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật không thành công !", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        load();
                    }
                }
                else
                {
                    if (bll_user.UpdateUserIsDocTor(taikhoan()) >= 1)
                    {
                        MessageBox.Show("Cập nhật thành công !", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật không thành công !", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}