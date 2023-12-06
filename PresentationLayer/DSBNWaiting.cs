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
    public partial class DSBNWaiting : Form
    {
        mainform mainFrm;
        public string trieuchung;
        public DSBNWaiting(mainform _frm)
        {
            InitializeComponent();
            mainFrm = _frm;
        }
        public void SetRole()
        {
            if (mainFrm.IDDoctor == 0)
            {
                dataGridViewDSBNCho.Columns["Khambenh"].Visible = false;
                btnThem.Enabled = false;
            }
        }
        public DSBNWaiting()
        {
            InitializeComponent();
        }
        QLBVService.QLBVSoapClient bll_dsbn = new QLBVService.QLBVSoapClient();
        void loadBN()
        {
            DataSet table = bll_dsbn.GetBenhNhanCho();
            dataGridViewDSBNCho.DataSource = table.Tables[0];
            dataGridViewDSBNCho.Columns["MaBN"].Visible = false;
            dataGridViewDSBNCho.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridViewDSBNCho.EnableHeadersVisualStyles = false;
            dataGridViewDSBNCho.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGridViewDSBNCho.ColumnHeadersHeight = 40;
            dataGridViewDSBNCho.ColumnHeadersDefaultCellStyle.BackColor = Color.LightSkyBlue;

            dataGridViewDSBNCho.Columns["HoTen"].HeaderText = "Họ Tên";             dataGridViewDSBNCho.Columns["HoTen"].Width = 160;
            dataGridViewDSBNCho.Columns["NgaySinh"].HeaderText = "Ngày Sinh";
            dataGridViewDSBNCho.Columns["DiaChi"].HeaderText = "Địa Chỉ";
            dataGridViewDSBNCho.Columns["GioiTinh"].HeaderText = "Giới Tính";
            dataGridViewDSBNCho.Columns["DienThoai"].HeaderText = "Điện Thoại";
            dataGridViewDSBNCho.Columns["MaBHYT"].HeaderText = "Mã BHYT";
            dataGridViewDSBNCho.Columns["TrieuChung"].HeaderText = "Triệu Chứng"; dataGridViewDSBNCho.Columns["TrieuChung"].Width = 200;
            dataGridViewDSBNCho.Columns["MaKhoa"].HeaderText = "Khoa";
            dataGridViewDSBNCho.Columns["TrangThai"].HeaderText = "Trạng Thái";
        }
        private void DSBNWaiting_Load(object sender, EventArgs e)
        {
            loadBN();
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            AddBN addbn = new AddBN();
            addbn.ShowDialog();
            trieuchung = addbn.themTC;
            loadBN();
        }
        private void dataGridViewDSBNCho_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridViewDSBNCho.Columns[e.ColumnIndex].Name== "Khambenh")
            {
                DataGridViewRow selectedRow = dataGridViewDSBNCho.Rows[e.RowIndex];
                string hoten = selectedRow.Cells["HoTen"].Value.ToString();
                string NgaySinh = selectedRow.Cells["NgaySinh"].Value.ToString();
                bool GioiTinh = bool.Parse(selectedRow.Cells["GioiTinh"].Value.ToString());
                string DiaChi = selectedRow.Cells["DiaChi"].Value.ToString();
                string MaKhoa = selectedRow.Cells["MaKhoa"].Value.ToString();
                string trieuchung = selectedRow.Cells["TrieuChung"].Value.ToString();
                int mabn = int.Parse(selectedRow.Cells["MaBN"].Value.ToString());
               QLBVService.DTOKhamBenh khambenh = new QLBVService.DTOKhamBenh();
                khambenh.MaBS = mainFrm.IDDoctor;
                khambenh.MaBN = int.Parse(selectedRow.Cells["MaBN"].Value.ToString());
                khambenh.KetLuan = "";
                khambenh.ThoiGian = DateTime.Now.ToString("dd/MM/yyyy");

                int idKhambenh;
                if (bll_dsbn.selectBN(mabn) > 0)
                {
                    idKhambenh = bll_dsbn.selectID(mabn);
                }
                else
                {
                    idKhambenh = bll_dsbn.InsertCTKhambenh(khambenh);
                }
                ChiDinhDichVu khambenhFrm = new ChiDinhDichVu(mainFrm, MaKhoa, DiaChi, GioiTinh, NgaySinh, hoten, idKhambenh, trieuchung, mabn);
                khambenhFrm.IDKhambenh = idKhambenh;
                khambenhFrm.ShowDialog();
            }
        }
        private void dataGridViewDSBNCho_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewDSBNCho_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            
        }
    }
}
