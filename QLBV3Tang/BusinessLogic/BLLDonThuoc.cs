using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BLLDonThuoc
    {
        DALDonThuoc dal_donthuoc = new DALDonThuoc();
        public int InsertMaphieu(int maphieu)
        {
            return dal_donthuoc.InsertMaPhieu(maphieu);
        }
        public int countMaDonThuoc(int madt)
        {
            return dal_donthuoc.countMaDonThuoc(madt);
        }
        public int selectMaDonThuoc(int maphieu)
        {
            return dal_donthuoc.selectMaDonThuoc(maphieu);
        }
    }
}