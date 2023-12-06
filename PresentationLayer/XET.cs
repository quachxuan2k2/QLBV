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
using System.Text.RegularExpressions;

namespace PresentationLayer
{
    public partial class XET : Form
    {
        mainform mainFrm;
        public XET(mainform _frm)
        {
            InitializeComponent();
            mainFrm = _frm;
        }
        public void SetRole()
        {
            if (mainFrm.IDDoctor == 0)
            {
                btnLuu.Enabled = false;
            }
        }
        QLBVService.QLBVSoapClient bll_benhnhan = new QLBVService.QLBVSoapClient();
        private void XET_Load(object sender, EventArgs e)
        {
            load();
        }
        void load()
        {
            DataSet table = bll_benhnhan.GetBenhNhanXetNghiem();
            dgvBenhNhan.DataSource = table.Tables[0];
            dgvBenhNhan.Columns["MaBN"].HeaderText = "Mã Bệnh Nhân";
            dgvBenhNhan.Columns["GioiTinh"].Visible = false;
            dgvBenhNhan.Columns["DiaChi"].Visible = false;
            dgvBenhNhan.Columns["NgaySinh"].Visible = false;
            dgvBenhNhan.EnableHeadersVisualStyles = false;
            dgvBenhNhan.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dgvBenhNhan.ColumnHeadersHeight = 40;
            dgvBenhNhan.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;

            dgvBenhNhan.Columns["HoTen"].HeaderText = "Họ Tên";          dgvBenhNhan.Columns["HoTen"].Width = 160;
            dgvBenhNhan.Columns["TrieuChung"].HeaderText = "Triệu Chứng"; dgvBenhNhan.Columns["TrieuChung"].Width = 160;
            dgvBenhNhan.Columns["TenDV"].HeaderText = "Tên Dịch Vụ";     dgvBenhNhan.Columns["TenDV"].Width = 160;
            dgvBenhNhan.Columns["Gia"].HeaderText = "Giá";
            dgvBenhNhan.Columns["HoTen1"].HeaderText = "Bác Sĩ Phụ Trách"; dgvBenhNhan.Columns["HoTen1"].Width = 160;
        }
        private void dgvBenhNhan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                lblHoTen.Text = dgvBenhNhan.Rows[e.RowIndex].Cells["HoTen"].Value.ToString();
                lblNgaySinh.Text = dgvBenhNhan.Rows[e.RowIndex].Cells["NgaySinh"].Value.ToString();
                if (Convert.ToBoolean(dgvBenhNhan.Rows[e.RowIndex].Cells["GioiTinh"].Value))
                {
                    lblGioiTinh.Text = "Nam";
                }
                else
                {
                    lblGioiTinh.Text = "Nữ";
                }
                lblQueQuan.Text = dgvBenhNhan.Rows[e.RowIndex].Cells["DiaChi"].Value.ToString();
                lblPhieu.Text = dgvBenhNhan.Rows[e.RowIndex].Cells["MaPhieu"].Value.ToString();
                lblMaBN.Text = dgvBenhNhan.Rows[e.RowIndex].Cells["MaBN"].Value.ToString();
            }
        }
        QLBVService.DTOChiTietDV ct_dv()
        {
            QLBVService.DTOChiTietDV ct_dv = new QLBVService.DTOChiTietDV();
            ct_dv.KetQuaDV = txbKetQua.Text;
            return ct_dv;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            int reg = check();
            switch (reg)
            {
                case 0:
                    if (bll_benhnhan.UpdateCTDV1(int.Parse(lblPhieu.Text),ct_dv()) > 0)
                    {
                        bll_benhnhan.UpdateStatus1(int.Parse(lblMaBN.Text.ToString()));
                        MessageBox.Show("Lưu thành công !", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        txbKetQua.Clear();
                        txbLD.Clear();
                        txbNCL.Clear();
                        txbNVS.Clear();
                        txbPHBT.Clear();
                        txbTTHT.Clear();
                        load();
                    }
                    else
                    {
                        MessageBox.Show("Không lưu được !", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    };
                    ;
                    break;
                case 1:
                    MessageBox.Show("Thông tin trống, nhập lại!", "thông báo", MessageBoxButtons.OK);
                    break;
                case 2:
                    MessageBox.Show("Thông tin sai định dạng, nhập lại!", "thông báo", MessageBoxButtons.OK);
                    break;
            }
            // 0: nếu nhập đúng định dạng
            // 1: nếu bỏ trống không nhập
            // 2: nhập sai định dạng
            int check()
            {   //biểu thức chính quy kiểm tra lỗi: nếu là lỗi dữ liệu sẽ trả kết quả true, ngược lại là false
                string chuoikytu = @"[\s\w]";  //Ký tự khoảng trắng, [ \t\n\x0b\r\f]
                if ((Regex.IsMatch(txbKetQua.Text, chuoikytu)) && (Regex.IsMatch(txbLD.Text, chuoikytu)) && (Regex.IsMatch(txbNCL.Text, chuoikytu)) && (Regex.IsMatch(txbNVS.Text, chuoikytu)) && (Regex.IsMatch(txbPHBT.Text, chuoikytu)) && (Regex.IsMatch(txbTTHT.Text, chuoikytu)))
                {
                    return 0;
                }
                else if (string.IsNullOrEmpty(txbKetQua.Text) || string.IsNullOrEmpty(txbLD.Text) || string.IsNullOrEmpty(txbNCL.Text) || string.IsNullOrEmpty(txbNVS.Text) || string.IsNullOrEmpty(txbPHBT.Text) || string.IsNullOrEmpty(txbTTHT.Text))
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
        }
        private void dgvBenhNhan_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }
    }
}