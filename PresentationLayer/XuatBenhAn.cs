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
using System.Text.RegularExpressions;
using CrystalDecisions.Windows.Forms;


namespace PresentationLayer
{
    public partial class XuatBenhAn : Form
    {
        public XuatBenhAn(int maphieu)
        {
            InitializeComponent();
            lblMaPhieu.Text = maphieu.ToString();
        }
        private void XuatBenhAn_Load(object sender, EventArgs e)
        {

        }
        QLBVService.QLBVSoapClient bll_khambenh = new QLBVService.QLBVSoapClient();
        void LoadReport()
        {
            QLBVService.BenhAnDataSet ds = new QLBVService.BenhAnDataSet();
            QLBVService.QLBVSoapClient client = new QLBVService.QLBVSoapClient();
            ds = client.GetBenhNhanReport(int.Parse(lblMaPhieu.Text));
            BenhAnReport rpt = new BenhAnReport();
            rpt.SetDataSource(ds);
            crystalReportViewer1.ReportSource = rpt;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            int reg = check();
            switch (reg)
            {
                case 0:
                    if (bll_khambenh.InsertKetLuan(int.Parse(lblMaPhieu.Text), txbKetLuan.Text) > 0)
                    {
                        MessageBox.Show("Lưu thành công !", "thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        LoadReport();
                    }
                    else
                    {
                        MessageBox.Show("Lưu thất bại !", "thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
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
            //biểu thức chính quy kiểm tra lỗi: nếu là lỗi dữ liệu sẽ trả kết quả true, ngược lại là false
            string chuoikytu = @"[\s\w]";  //Ký tự khoảng trắng, [ \t\n\x0b\r\f]
            if ((Regex.IsMatch(txbKetLuan.Text, chuoikytu)) )
            {
                return 0;
            }
            else if (string.IsNullOrEmpty(txbKetLuan.Text) )
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }

       
    }
}