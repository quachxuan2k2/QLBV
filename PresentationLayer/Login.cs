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

namespace PresentationLayer
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        QLBVService.QLBVSoapClient blluser = new QLBVService.QLBVSoapClient();
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtTenDN.Text;
            string pass = txtMatkhau.Text;
            int matk = blluser.selectMaTK(username);
            if (matk >= 0)
            {
                int res = blluser.CheckUser(username, pass);
                switch (res)
                {
                    case -1: label3.Text = "Người dùng không tồn tại !"; break;
                    case -2: label3.Text = "Tên đăng nhập và mật khẩu không hợp lệ !"; break;
                    default:
                        mainform main = new mainform(matk, this);
                        main.IDDoctor = res;
                        main.SetDoctor(matk);
                        main.ShowDialog();
                        this.Close();
                        break;
                }
            }
        }
        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}