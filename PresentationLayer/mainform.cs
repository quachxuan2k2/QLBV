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
using FontAwesome.Sharp;

namespace PresentationLayer
{
    public partial class mainform : Form
    {
        //field
        IconButton currentBtn;
        Panel leftBorderBtn;

        public int IDDoctor;
        public int IDDoctor1;
        frmLogin login;
        public mainform(int matk, frmLogin _frm)
        {
            InitializeComponent();
            lblMaTK.Text = matk.ToString();
            login = _frm;
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 45);
            panelMenu.Controls.Add(leftBorderBtn);
        }
        private struct RGBColor
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(253, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);
        }
        private void ActivateButton(object senderBtn, Color color)
        {
            if(senderBtn != null)
            {
                DisableButton();
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;
                // left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();
            }
        }
        private void DisableButton()
        {
            if(currentBtn != null)
            {
                currentBtn.BackColor = Color.Navy;
                currentBtn.ForeColor = Color.White;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.White;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }
        QLBVService.QLBVSoapClient bll_dichvu = new QLBVService.QLBVSoapClient();
        public void SetDoctor(int matk)
        {
                int res= bll_dichvu.CheckDV(int.Parse(lblMaTK.Text)) ;
            switch (res)
            {
                case 0: // không có mã bác sĩ : admin, lễ tân
                    btnXetNghiem.Enabled = false;   btnXetNghiem.ForeColor = Color.White;
                    btnCDHA.Enabled = false;        btnCDHA.ForeColor = Color.White;
                    break;
                case 1: // XQ, Siêu âm, Nội soi
                    btnDichVu.Enabled = false;      btnDichVu.ForeColor = Color.White;
                    btnDuoc.Enabled = false;        btnDuoc.ForeColor = Color.White;
                    btnKhamBenh.Enabled = false;    btnKhamBenh.ForeColor = Color.White;
                    btnBS.Enabled = false;          btnBS.ForeColor = Color.White;
                    btnUser.Enabled = false;        btnUser.ForeColor = Color.White;
                    btnXetNghiem.Enabled = false;   btnXetNghiem.ForeColor = Color.White;
                    btnBenhNhanCho.Enabled = false; btnBenhNhanCho.ForeColor = Color.White;
                    break;
                case 2: // Xét nghiệm
                    btnDichVu.Enabled = false;      btnDichVu.ForeColor = Color.White;
                    btnDuoc.Enabled = false;        btnDuoc.ForeColor = Color.White;
                    btnKhamBenh.Enabled = false;    btnKhamBenh.ForeColor = Color.White;
                    btnBS.Enabled = false;          btnBS.ForeColor = Color.White;
                    btnUser.Enabled = false;        btnUser.ForeColor = Color.White;
                    btnCDHA.Enabled = false;        btnCDHA.ForeColor = Color.White;
                    btnBenhNhanCho.Enabled = false; btnBenhNhanCho.ForeColor = Color.White;
                    break;

                case 4: // không có mã dịch vụ : bác sĩ chỉ định
                    btnDichVu.Enabled = false;          btnDichVu.ForeColor = Color.White;
                    btnDuoc.Enabled = false;            btnDuoc.ForeColor = Color.White;
                    btnBS.Enabled = false;              btnBS.ForeColor = Color.White;
                    btnUser.Enabled = false;            btnUser.ForeColor = Color.White;
                    btnCDHA.Enabled = false;            btnCDHA.ForeColor = Color.White;
                    btnXetNghiem.Enabled = false;       btnXetNghiem.ForeColor = Color.White;
                    break;
            }
        }
        private void btnBN_Click(object sender, EventArgs e)
        {
            DSBN dsbn = new DSBN();
            dsbn.ShowDialog();
        }
        public void SetRole()
        {
            if (IDDoctor == 0)
            {
                AddBS addbs = new AddBS();
                addbs.ShowDialog();
            }
            else
            {
                BS bs = new BS();
                bs.ShowDialog();
            }
        }
        private void btnBS_Click(object sender, EventArgs e)
        {
            SetRole();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            XET xetnghiem = new XET(this);
            xetnghiem.SetRole();
            xetnghiem.Show();
        }
        private void btnDuoc_Click(object sender, EventArgs e)
        {
            Duoc duoc = new Duoc();
            duoc.Show();
        }
        BLLUser bll_user = new BLLUser();
        private void mainform_Load(object sender, EventArgs e)
        {
           
        }
        private void btnKhamBenh_Click(object sender, EventArgs e)
        {
            DSBNWaiting dscho = new DSBNWaiting(this);
            dscho.SetRole();
            dscho.Show();
        }
        private void btnDichVu_Click(object sender, EventArgs e)
        {
            DichVu dichvu = new DichVu();
            dichvu.ShowDialog();
        }
        private void btnUser_Click(object sender, EventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            CDHA cdha = new CDHA(this);
            cdha.SetRole();
            cdha.Show();
        }
        private void btnKhamBenh_Click_1(object sender, EventArgs e)
        {
            KhamBenh khambenh = new KhamBenh(this);
            khambenh.SetRole();
            khambenh.Show();
        }

        private void iconButtonDV_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color1);
        }

        private void iconButtonDuoc_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color2);
        }

        private void iconButtonKB_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color4);
        }
        private void iconButtonBNC_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color3);
        }

        private void iconButtonBS_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color5);
        }

        private void iconButtonND_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, RGBColor.color6);
        }

        private void iconButtonXN_Click(object sender, EventArgs e)
        {
            //ActivateButton(sender, RGBColor.color1);
        }

        private void iconButtonCDHA_Click(object sender, EventArgs e)
        {
            //ActivateButton(sender, RGBColor.color2);
        }
    }
}