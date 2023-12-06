using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PresentationLayer
{
    public partial class KhamBenh : Form
    {
        mainform mainFrm;
        public KhamBenh(mainform _frm)
        {
            InitializeComponent();
            mainFrm = _frm;
        }
        public void SetRole()
        {
            if (mainFrm.IDDoctor == 0)
            {
                btnBenhAn.Enabled = false;
                btnKeToa.Enabled = false;
            }
        }
        QLBVService.QLBVSoapClient bll_benhnhan = new QLBVService.QLBVSoapClient();
        private void KhamBenh_Load(object sender, EventArgs e)
        {
            DataSet table = bll_benhnhan.GetBenhNhanCoKQ();
            dgvBenhNhan.DataSource = table.Tables[0];
            dgvBenhNhan.Columns["KetQuaDV"].Visible = false;
            dgvBenhNhan.EnableHeadersVisualStyles = false;
            dgvBenhNhan.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dgvBenhNhan.ColumnHeadersHeight = 40;
            dgvBenhNhan.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;

            dgvBenhNhan.Columns["MaPhieu"].HeaderText = "Mã Phiếu";
            dgvBenhNhan.Columns["KetQuaDV"].HeaderText = "Kết Quả Dịch Vụ";      dgvBenhNhan.Columns["KetQuaDV"].Width = 160;
            dgvBenhNhan.Columns["HoTen"].HeaderText = "Họ Tên";                  dgvBenhNhan.Columns["HoTen"].Width = 160;
            dgvBenhNhan.Columns["NgaySinh"].HeaderText = "Ngày Sinh";   
            dgvBenhNhan.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dgvBenhNhan.Columns["DiaChi"].HeaderText = "Địa chỉ";
            dgvBenhNhan.Columns["TrieuChung"].HeaderText = "Triệu Chứng";        dgvBenhNhan.Columns["TrieuChung"].Width = 160;
            dgvBenhNhan.Columns["TenDV"].HeaderText = "Tên Dịch Vụ";                dgvBenhNhan.Columns["TenDV"].Width = 160;
            dgvBenhNhan.Columns["Gia"].HeaderText = "Giá";
            dgvBenhNhan.Columns["HoTen1"].HeaderText = "Bác Sĩ Phụ Trách";          dgvBenhNhan.Columns["HoTen1"].Width = 160;
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
                txbKetLuanDV.Text = dgvBenhNhan.Rows[e.RowIndex].Cells["KetQuaDV"].Value.ToString();
                lblMaPhieu.Text = dgvBenhNhan.Rows[e.RowIndex].Cells["MaPhieu"].Value.ToString();
            }
        }
        private void btnBenhAn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbKetLuanDV.Text))
            {
                MessageBox.Show("Chưa chọn bênh nhân !", "thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                XuatBenhAn benhan = new XuatBenhAn(int.Parse(lblMaPhieu.Text));
                benhan.Show();
                this.Close();
            }
        }
        private void btnKeToa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txbKetLuanDV.Text))
            {
                MessageBox.Show("Chưa chọn bênh nhân !", "thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            else
            {
                int madonthuoc;
                int maphieu = int.Parse(lblMaPhieu.Text);
                if (bll_benhnhan.countMaDonThuoc(maphieu) > 0)
                {
                    madonthuoc = bll_benhnhan.selectMaDonThuoc(maphieu);
                }
                else
                {
                    madonthuoc = bll_benhnhan.InsertMaphieu(maphieu);
                }
                KeToa ketoathuoc = new KeToa(lblHoTen.Text, lblNgaySinh.Text, lblQueQuan.Text, lblGioiTinh.Text, maphieu, madonthuoc);
                ketoathuoc.Show();
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult tb = MessageBox.Show("Bạn có muốn thoát!", "thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(tb == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dgvBenhNhan_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
           
        }
    }
}