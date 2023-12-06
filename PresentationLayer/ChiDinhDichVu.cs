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

namespace PresentationLayer
{
    public partial class ChiDinhDichVu : Form
    {
        public mainform mainFrm;
        public int IDKhambenh;
        public string trieuchung;
        QLBVService.QLBVSoapClient bll_chidinhdichvu = new QLBVService.QLBVSoapClient();
        public ChiDinhDichVu(mainform _frm, string MaKhoa, string DiaChi, bool GioiTinh, string NgaySinh, string hoten, int MaPhieu, string trieuchung, int mabn)
        {
            InitializeComponent();
            this.mainFrm = _frm;
            lblHoTen.Text = hoten;
            lblNgaySinh.Text = NgaySinh;
            if(GioiTinh == true)
            {
                lblGioiTinh.Text = "Nam";
            }
            else
            {
                lblGioiTinh.Text = "Nữ";
            }
            lblDiaChi.Text = DiaChi;
            lblKhoa.Text = bll_chidinhdichvu.GetName(MaKhoa);
            lblMaPhieu.Text = MaPhieu.ToString();
            lblMaBN.Text = mabn.ToString();
            txbTrieuChung.Text = trieuchung;
        }
        QLBVService.DTOChiTietDV ct_DV()
        {
            QLBVService.DTOChiTietDV ct_dichvu = new QLBVService.DTOChiTietDV();
                ct_dichvu.MaPhieu = int.Parse(lblMaPhieu.Text);
                ct_dichvu.MaDV = cb_DichVu.SelectedValue.ToString();
                return ct_dichvu;
        }
        private void KhamBenh_Load(object sender, EventArgs e)
        {
            DataSet table = bll_chidinhdichvu.GetDichVu();
            cb_DichVu.DataSource = table.Tables[0];
            cb_DichVu.DisplayMember = "TenDV";
            cb_DichVu.ValueMember = "MaDV";
            load();
        }
        void load()
        {
            DataSet ds = bll_chidinhdichvu.GetChiTiet_DV(int.Parse(lblMaPhieu.Text));
            dataGridView1.DataSource = ds.Tables[0] ;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;

            dataGridView1.Columns["MaDV"].HeaderText = "Mã Dịch Vụ"; dataGridView1.Columns["MaDV"].Width = 160;
            dataGridView1.Columns["TenDV"].HeaderText = "Tên Dịch Vụ";          dataGridView1.Columns["TenDV"].Width = 160;
            dataGridView1.Columns["Gia"].HeaderText = "Giá";
            dataGridView1.Columns["MaDVCha"].HeaderText = "Thuộc Loại"; dataGridView1.Columns["MaDVCha"].Width = 160;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int reg = check();
            switch (reg)
            {
                case 0:
                    if (bll_chidinhdichvu.AddChiTiet_DV(ct_DV()) > 0)
                    {
                        //đổi trạng thái bệnh nhân thành đang khám
                        bll_chidinhdichvu.UpdateStatus(int.Parse(lblMaBN.Text));
                        MessageBox.Show("Thêm thành công !", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
                        load();
                    }
                    else
                    {
                        MessageBox.Show("Thêm thất bại !", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
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
        {
            if (cb_DichVu.SelectedIndex != -1)
            {
                return 0;
            }
            else if (cb_DichVu.SelectedIndex == -1)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }
    }
}