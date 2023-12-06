using DataTransferObject.QLBVDataSetTableAdapters;
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
using BusinessLogicLayer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;

namespace PresentationLayer
{
    public partial class AddBS : Form
    {
        public AddBS()
        {
            InitializeComponent();
        }
        QLBVService.QLBVSoapClient bacsi = new QLBVService.QLBVSoapClient();
        void load()
        {
            DataSet ds = bacsi.getBS();
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns["MaBS"].Visible = false;
            dataGridView1.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;

            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.Columns["HoTen"].HeaderText = "Họ Tên";               dataGridView1.Columns["HoTen"].Width = 160;
            dataGridView1.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dataGridView1.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dataGridView1.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dataGridView1.Columns["CCHN"].HeaderText = "CCHN";
            dataGridView1.Columns["MaKhoa"].HeaderText = "Khoa";
            dataGridView1.Columns["MaDV"].HeaderText = "Dịch Vụ";
        }
        private void AddBS_Load(object sender, EventArgs e)
        {
            DataSet ds = bacsi.getDVCha();
            cb_DV.DataSource = ds.Tables[0];
            cb_DV.DisplayMember = "TenDVCha";
            cb_DV.ValueMember = "MaDVCha";
            load();
        }
        private void dataGridView1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].Cells["STT"].Value = e.RowIndex + 1;

        }
        QLBVService.DTOBacSi getInfoBacSi()
        {
            // 1: Nam 
            // 2: Nu
            QLBVService.DTOBacSi bacsi = new QLBVService.DTOBacSi();
            bacsi.HoTen = txtHoTen.Text;
            bacsi.DiaChi = txtDiaChi.Text;
            bacsi.CCHN = txtCCHN.Text;
            bacsi.NgaySinh = dateTimePicker1.Text;
            bacsi.MaDV = cb_DV.SelectedValue.ToString();
            if(rdoNam.Checked == true)
            {
                bacsi.GioiTinh = 1;
            }
            else
            {
                bacsi.GioiTinh = 0;
            }
            return bacsi;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            int reg = check();
            switch (reg)
            {
                case 0:
                    if (rdoCo.Checked)
                    {
                        if(bacsi.InsertBacSi(getInfoBacSi()) > 0)
                        {
                            MessageBox.Show("Lưu thành công !","Thông báo",MessageBoxButtons.OK);
                        }
                    }
                    else
                    {
                        bacsi.InsertBacSiK(getInfoBacSi());
                        MessageBox.Show("Lưu thành công !", "Thông báo", MessageBoxButtons.OK);
                    }
                    load();
                    txtHoTen.Clear();
                    txtDiaChi.Clear();
                    txtCCHN.Clear();
                    txtHoTen.Focus();
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
            string chuoikytu = @"[\s\w]";  //Ký tự khoảng trắng, [ \t\n\x0b\r\f]
            if ((Regex.IsMatch(txtHoTen.Text, chuoikytu)) && (Regex.IsMatch(txtDiaChi.Text, chuoikytu)) && (Regex.IsMatch(txtCCHN.Text, chuoilien)) )
            {
                return 0;
            }
            else if (string.IsNullOrEmpty(txtHoTen.Text) || string.IsNullOrEmpty(txtDiaChi.Text) || string.IsNullOrEmpty(txtCCHN.Text))
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        private void rdoCo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdoCo.Checked)
            {
                cb_DV.Visible = true;
            }
            if (rdoKhong.Checked)
            {
                cb_DV.Visible = false;
            }
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
           
        }
    }
}
