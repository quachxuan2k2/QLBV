using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DataTransferObject;
namespace BusinessLogicLayer
{
    public class BLLCT_DonThuoc
    {
        DALCT_DonThuoc dal_ctdonthuoc = new DALCT_DonThuoc();
        public DataTable GetThuoc(int madonthuoc)
        {
            return dal_ctdonthuoc.GetThuoc(madonthuoc);
        }
        public int CreateCTDonThuoc(DTOCT_DonThuoc ct_donthuoc)
        {
            return dal_ctdonthuoc.CreateCTDonThuoc(ct_donthuoc);
        }
        public void GetThuocReport(int madonthuoc, DataTable table)
        {
            dal_ctdonthuoc.GetThuocReport(madonthuoc,table);
        }
    }
}