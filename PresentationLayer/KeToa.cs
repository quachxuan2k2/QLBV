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
    public partial class KeToa : Form
    {
        public KeToa(string HoTen, string NgaySinh, string DiaChi, string GioiTinh, int maphieu, int madonthuoc)
        {
            InitializeComponent();
            lblHoTen.Text = HoTen;
            lblNgaySinh.Text = NgaySinh;
            lblQueQuan.Text = DiaChi;
            lblGIoiTinh.Text = GioiTinh;
            lblMaPhieu.Text = maphieu.ToString();
            lblmaDT.Text = madonthuoc.ToString();
        }
        QLBVService.QLBVSoapClient bll_ctdonthuoc = new QLBVService.QLBVSoapClient();
        private void KeToa_Load(object sender, EventArgs e)
        {
            int madonthuoc = int.Parse(lblmaDT.Text);
            DataSet table = bll_ctdonthuoc.GetMaThuoc();
            cbb_TenThuoc.DataSource = table.Tables[0];
            cbb_TenThuoc.DisplayMember = "TenThuoc";
            cbb_TenThuoc.ValueMember = "MaThuoc";

            DataSet tbl_thuoc = bll_ctdonthuoc.GetThuoc(madonthuoc);
            dgvThuoc.DataSource = tbl_thuoc.Tables[0];
            dgvThuoc.Columns["MaDonThuoc"].Visible = false;
            dgvThuoc.EnableHeadersVisualStyles = false;
            dgvThuoc.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dgvThuoc.ColumnHeadersHeight = 40;
            dgvThuoc.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;

            dgvThuoc.Columns["MaThuoc"].HeaderText = "Mã Thuốc";
            dgvThuoc.Columns["SoLuong"].HeaderText = "Số Lượng";
            dgvThuoc.Columns["ThanhTien"].HeaderText = "Thành Tiền";
            dgvThuoc.Columns["LieuDung"].HeaderText = "Liều Dùng";      dgvThuoc.Columns["LieuDung"].Width = 160;
            dgvThuoc.Columns["TenThuoc"].HeaderText = "Tên Thuốc";      dgvThuoc.Columns["TenThuoc"].Width = 160;
            dgvThuoc.Columns["GiaBan"].HeaderText = "Giá Bán";
            dgvThuoc.Columns["HangSX"].HeaderText = "Hãng Sản Xuất";
            load(madonthuoc);
        }
        void load(int madonthuoc)
        {
            DataSet tbl_thuoc = bll_ctdonthuoc.GetThuoc(madonthuoc);
            dgvThuoc.DataSource = tbl_thuoc.Tables[0];
        }
        void load(string mathuoc)
        {
            txbGiaBan.Text = bll_ctdonthuoc.GetGiabyMaThuoc(mathuoc).ToString();
            //txbThanhTien.Text = (int.Parse(txbSoLuong.Text) * giaban).ToString();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            // update trạng thái đã khám
            int mabn = bll_ctdonthuoc.selectMaBN(lblHoTen.Text);
            bll_ctdonthuoc.UpdateStatus2(mabn);
            // xuất hóa đơn thuốc
            int madonthuoc = int.Parse(lblmaDT.Text);
            HoaDon hoadon = new HoaDon(madonthuoc);
            hoadon.Show();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbSoLuong.Text))
            {
                txbThanhTien.Text = "0";
            }
            else
            {
                int gb = int.Parse(txbGiaBan.Text);
                int soluong = int.Parse(txbSoLuong.Text);
                int thanhtien = gb * soluong;
                txbThanhTien.Text = thanhtien.ToString();
            }
        }
        private void cbb_TenThuoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbb_TenThuoc.SelectedIndex != -1)
            {
                string mathuoc = cbb_TenThuoc.SelectedValue.ToString();
                int giaban = bll_ctdonthuoc.GetGiabyMaThuoc(mathuoc);
                txbGiaBan.Text = giaban.ToString();
            }
        }
        private void txbThanhTien_TextChanged(object sender, EventArgs e)
        {
           
        }
        QLBVService.DTOCT_DonThuoc ct_donthuoc()
        {
            QLBVService.DTOCT_DonThuoc ct_donthuoc = new QLBVService.DTOCT_DonThuoc();
            ct_donthuoc.maDonThuoc = int.Parse(lblmaDT.Text);
            ct_donthuoc.maThuoc = cbb_TenThuoc.SelectedValue.ToString();
            ct_donthuoc.soLuong = int.Parse(txbSoLuong.Text);
            ct_donthuoc.thanhTien = int.Parse(txbThanhTien.Text);
            ct_donthuoc.lieuDung = txbLieuDung.Text;
            return ct_donthuoc;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            int reg = check();
            switch (reg)
            {
                case 0:
                    if (bll_ctdonthuoc.CreateCTDonThuoc(ct_donthuoc()) > 0)
                    {
                        load(int.Parse(lblmaDT.Text));
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
            string so = @"[\d]";   // số bất kỳ [0-9]
            string chuoikytu = @"[\s\w]";  //Ký tự khoảng trắng, [ \t\n\x0b\r\f]
            if ((Regex.IsMatch(txbSoLuong.Text, so)) && (Regex.IsMatch(txbLieuDung.Text, chuoikytu)) && cbb_TenThuoc.SelectedIndex != -1)
            {
                return 0;
            }
            else if (string.IsNullOrEmpty(txbSoLuong.Text) || string.IsNullOrEmpty(txbLieuDung.Text) || cbb_TenThuoc.SelectedIndex == -1)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        private void dgvThuoc_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }
    }
}