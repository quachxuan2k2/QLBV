using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class DTODonThuoc
    {
        public int maDonThuoc { get; set; }
        public int maPhieu { get; set; }
        public int tongTien { get; set; }
        public string htThanhToan { get; set; }
    }
    public class DTOCT_DonThuoc
    {
        public int maDonThuoc { get; set; }
        public string maThuoc { get; set; }
        public int soLuong { get; set; }
        public int thanhTien { get; set; }
        public string lieuDung { get; set; }

    }
}
