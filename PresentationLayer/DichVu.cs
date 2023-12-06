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
using DataTransferObject;
using System.Text.RegularExpressions;

namespace PresentationLayer
{
    public partial class DichVu : Form
    {
        public DichVu()
        {
            InitializeComponent();
        }
        QLBVService.QLBVSoapClient bll_dichvu = new QLBVService.QLBVSoapClient();
        void loaddv()
        {
            DataSet table = bll_dichvu.getDVCha();
            cbb_dichvu.DataSource = table.Tables[0];
            cbb_dichvu.DisplayMember = "TenDVCha";
            cbb_dichvu.ValueMember = "MaDVCha";
        }
        void load()
        {
            loaddv();
            DataSet table = bll_dichvu.getDV();
            dataGridView1.DataSource = table.Tables[0];
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridView1.ColumnHeadersHeight = 40;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;

            dataGridView1.Columns["MaDV"].HeaderText = "Mã Dịch Vụ";
            dataGridView1.Columns["TenDV"].HeaderText = "Tên Dịch Vụ";          dataGridView1.Columns["TenDV"].Width = 160;
            dataGridView1.Columns["Gia"].HeaderText = "Giá";
            dataGridView1.Columns["MaDVCha"].HeaderText = "Thuộc Loại";
            dataGridView1.Columns["Delete"].DisplayIndex = dataGridView1.Columns.Count - 1;
            dataGridView1.Columns["Update"].DisplayIndex = dataGridView1.Columns.Count - 2;
        }
        private void DichVu_Load(object sender, EventArgs e)
        {
            load();
        }
        QLBVService.DTODichVu dichvu()
        {
            QLBVService.DTODichVu dichvu = new QLBVService.DTODichVu();
            dichvu.maDV = txbMaDV.Text  ;
            dichvu.tenDV = txbTenDV.Text  ;
            dichvu.gia = int.Parse(txbGia.Text) ;
            dichvu.MaDVCha = cbb_dichvu.SelectedValue.ToString();
            return dichvu;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            int reg = check();
            switch (reg)
            {
                case 0:
                    if (bll_dichvu.AddDichvu(dichvu()) > 0)
                    {
                        MessageBox.Show("Thêm thành công !", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand);
                        txbMaDV.Clear();
                        txbTenDV.Clear();
                        txbGia.Clear();
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
        {   //biểu thức chính quy kiểm tra lỗi: nếu là lỗi dữ liệu sẽ trả kết quả true, ngược lại là false
            string chuoilien = @"[\w]";   //Ký tự chữ, viết ngắn gọn cho[a - zA - Z_0 - 9]
            string so = @"[\d]";   // số bất kỳ [0-9]
            string chuoikytu = @"[\s\w]";  //Ký tự khoảng trắng, [ \t\n\x0b\r\f]
            if ((Regex.IsMatch(txbMaDV.Text, chuoilien)) && (Regex.IsMatch(txbTenDV.Text, chuoikytu)) && (Regex.IsMatch(txbGia.Text, so)) && cbb_dichvu.SelectedIndex != -1)
            {
                return 0;
            }
            else if (string.IsNullOrEmpty(txbMaDV.Text) || string.IsNullOrEmpty(txbTenDV.Text) || string.IsNullOrEmpty(txbGia.Text) || cbb_dichvu.SelectedIndex == -1)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                        DialogResult tb = MessageBox.Show("Bạn có muốn xóa không ?", "thông báo !", MessageBoxButtons.YesNo);
                    if (tb == DialogResult.Yes)
                    {

                        if (bll_dichvu.DeleteDV(txbMaDV.Text) > 0)
                        {
                            MessageBox.Show("xóa thành công !");
                            load();
                        }
                        else
                        {
                            MessageBox.Show("Lỗi trong quá trình xóa !");
                        }
                    }
                }
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Update")
                {
                    if (bll_dichvu.UpdateDV(txbMaDV.Text, dichvu()) > 0)
                    {
                        MessageBox.Show("Cập nhật thành công !");
                        load();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi cập nhật !");
                    }
                }
                else
                {
                    txbMaDV.Text = dataGridView1.Rows[e.RowIndex].Cells["MaDV"].Value.ToString();
                    txbTenDV.Text = dataGridView1.Rows[e.RowIndex].Cells["TenDV"].Value.ToString();
                    txbGia.Text = dataGridView1.Rows[e.RowIndex].Cells["Gia"].Value.ToString();
                }
                
            }
        }
        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
           
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            txbMaDV.Clear();
            txbTenDV.Clear();
            txbGia.Clear(); 
        }
    }
}