using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObject;

namespace BusinessLogicLayer
{
    public class BLLBenhNhan
    {
        DALBenhNhan dalBenhnhan = new DALBenhNhan();
        public DataTable GetBenhNhanXetNghiem()
        {
            return dalBenhnhan.GetBenhNhanXetNghiem();
        }
        public void GetBenhNhanReport(int maphieu, DataTable table)
        {
             dalBenhnhan.GetBenhNhanReport(maphieu,table);
        }
        public DataTable GetBenhNhanCDHA()
        {
            return dalBenhnhan.GetBenhNhanCDHA();
        }
        public DataTable GetBenhNhanCoKQ()
        {
            return dalBenhnhan.GetBenhNhanCoKQ();
        }
        public DataTable GetAllBenhNhan()
        {
            return dalBenhnhan.GetBenhNhan();
        }
        public DataTable GetBenhNhanCho()
        {
            return dalBenhnhan.GetBenhNhanCho();
        }
        public int InsertBenhNhan(DTOBenhNhan benhnhan)
        {
            int IDExist = dalBenhnhan.CreateBenhNhan(benhnhan);
            return IDExist;
        }
        public int UpdateStatus(int mabn)
        {
            return dalBenhnhan.UpdateStatus(mabn);
        }
        public int UpdateStatus1(int mabn)
        {
            return dalBenhnhan.UpdateStatus1(mabn);
        }
        public int UpdateStatus2(int mabn)
        {
            return dalBenhnhan.UpdateStatus2(mabn);
        }
        public int selectMaBN(string tenbn)
        {
            return dalBenhnhan.selectMaBN(tenbn);
        }
    }
}
