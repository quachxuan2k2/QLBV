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
    public partial class HoaDon : Form
    {
        public HoaDon(int madonthuoc)
        {
            InitializeComponent();
            lblMaDonThuoc.Text = madonthuoc.ToString();
        }
        QLBVService.QLBVSoapClient bll_ctDonthuoc = new QLBVService.QLBVSoapClient();
        private void HoaDon_Load(object sender, EventArgs e)
        {
            
            LoadReport();
        }
        void LoadReport()
        {
            int madonthuoc = int.Parse(lblMaDonThuoc.Text);
            QLBVService.BenhAnDataSet ds = new QLBVService.BenhAnDataSet();
            QLBVService.QLBVSoapClient client = new QLBVService.QLBVSoapClient();
            ds = client.GetDonThuocReport(madonthuoc);
            HoaDonThuoc hoadon = new HoaDonThuoc();
            hoadon.SetDataSource(ds);
            crt_HoaDon.ReportSource = hoadon;
        }
    }
}