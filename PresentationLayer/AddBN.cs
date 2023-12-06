using BusinessLogicLayer;
using DataTransferObject;
using DataTransferObject.QLBVDataSetTableAdapters;
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
    public partial class AddBN : Form
    {
        public string themTC;
        public AddBN()
        {
            InitializeComponent();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            int reg = check();
            switch (reg)
            {
                case 0:
                    benhnhan.InsertBenhNhan(getInfoBenhnhan());
                    MessageBox.Show("Lưu thành công!", "thông báo", MessageBoxButtons.OK);
                    themTC = txtTrieuChung.Text;
                    load(); break;
                case 1:
                    MessageBox.Show("Thông tin trống, nhập lại!", "thông báo", MessageBoxButtons.OK);
                    break;
                case 2:
                    MessageBox.Show("Thông tin sai định dạng, nhập lại!", "thông báo", MessageBoxButtons.OK);
                    break;
            }
        }
        QLBVService.QLBVSoapClient benhnhan = new QLBVService.QLBVSoapClient();

        // 0: nếu nhập đúng định dạng
        // 1: nếu bỏ trống không nhập
        // 2: nhập sai định dạng
        int check()
        {   //biểu thức chính quy kiểm tra lỗi: nếu là lỗi dữ liệu sẽ trả kết quả true, ngược lại là false
            string so = @"[\d]";   // số bất kỳ [0-9]
            string chuoikytu = @"[\s\w]";  //Ký tự khoảng trắng, [ \t\n\x0b\r\f]
            if ((Regex.IsMatch(txtHoTen.Text, chuoikytu)) && (Regex.IsMatch(txtDiaChi.Text, chuoikytu)) && (Regex.IsMatch(txtTrieuChung.Text, chuoikytu)) && (Regex.IsMatch(txtDienThoai.Text, so)) && cbb_Khoa.SelectedIndex != -1)
            {
                return 0;
            }
            else if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtDienThoai.Text) || string.IsNullOrEmpty(txtDiaChi.Text) || string.IsNullOrEmpty(txtTrieuChung.Text) || cbb_Khoa.SelectedIndex == -1)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        void loadKhoa()
        {
            DataSet table = benhnhan.GetAllKhoa();
            cbb_Khoa.DataSource = table.Tables[0];
            cbb_Khoa.DisplayMember = "TenKhoa";
            cbb_Khoa.ValueMember = "MaKhoa";
        }
        void load()
        {
            loadKhoa();
            BenhNhanTableAdapter tblAdapter = new BenhNhanTableAdapter();
            dataGridView1.DataSource = tblAdapter.GetDataBy();
            QLBVDataSet.BenhNhanDataTable table_benhnhan = new QLBVDataSet.BenhNhanDataTable();
            tblAdapter.FillBy(table_benhnhan);
            dataGridView1.DataSource = table_benhnhan;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;

            dataGridView1.Columns["MaBN"].Visible = false;
            dataGridView1.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.Columns["HoTen"].HeaderText = "Họ Tên";                 dataGridView1.Columns["HoTen"].Width = 160;
            dataGridView1.Columns["NgaySinh"].HeaderText = "Ngày Sinh";    
            dataGridView1.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dataGridView1.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dataGridView1.Columns["DienThoai"].HeaderText = "Điện Thoại";
            dataGridView1.Columns["MaBHYT"].HeaderText = "Mã BHYT";
            dataGridView1.Columns["TrieuChung"].HeaderText = "Triệu Chứng";       dataGridView1.Columns["TrieuChung"].Width = 200;
            dataGridView1.Columns["MaKhoa"].HeaderText = "Khoa";
        }
        QLBVService.DTOBenhNhan getInfoBenhnhan()
        {
            // 1: Nam 
            // 0: Nu
            QLBVService.DTOBenhNhan benhnhan = new QLBVService.DTOBenhNhan();
            benhnhan.HoTen = txtHoTen.Text;
            benhnhan.DiaChi = txtDiaChi.Text;
            benhnhan.NgaySinh = dtp_NgaySinh.Text;
            benhnhan.DienThoai = txtDienThoai.Text;
            benhnhan.MaBHYT = txtMaBHYT.Text;
            benhnhan.TrieuChung = txtTrieuChung.Text;
            benhnhan.GioiTinh = rdoNam.Checked;
            benhnhan.MaKhoa = cbb_Khoa.SelectedValue.ToString();
            return benhnhan;
        }
        private void AddPatient_Load(object sender, EventArgs e)
        {
            load();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells["STT"].Value = e.RowIndex + 1;

        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }
    }
}
