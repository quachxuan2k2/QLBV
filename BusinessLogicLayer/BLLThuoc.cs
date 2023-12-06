using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BusinessLogicLayer
{
    public class BLLThuoc
    {
        DALThuoc dal_thuoc = new DALThuoc();
        public DataTable GetMaThuoc()
        {
            return dal_thuoc.GetMaThuoc();
        }
        public int GetGiabyMaThuoc(string mathuoc)
        {
            return dal_thuoc.GetGiabyMaThuoc(mathuoc);
        }
    }
}
