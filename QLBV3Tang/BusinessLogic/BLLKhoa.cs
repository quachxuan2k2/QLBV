using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BLLKhoa
    {
        DALKhoa dal_Khoa = new DALKhoa();
        public DataTable GetAllKhoa()
        {
            return dal_Khoa.GetKhoa();
        }
        public DataTable GetAllBenhNhanByKhoa(string MaKhoa)
        {
            return dal_Khoa.GetBenhNhanByMaKhoa(MaKhoa);
        }
        public string GetName(string makhoa)
        {
            return dal_Khoa.GetName(makhoa);
        }
    }
}
