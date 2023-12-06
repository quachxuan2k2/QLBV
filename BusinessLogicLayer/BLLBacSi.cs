using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataTransferObject;
using System.Data;

namespace BusinessLogicLayer
{
    public class BLLBacSi
    {
        DALBacSi dal_bacsi = new DALBacSi();
        public int InsertBacSi(DTOBacSi bacsi)
        {
            int IDExist = dal_bacsi.AddBS(bacsi);
            return IDExist;
        }
        public int InsertBacSiK(DTOBacSi bacsi)
        {
            int IDExist = dal_bacsi.AddBS1(bacsi);
            return IDExist;
        }
        public DataTable getBS()
        {
            return dal_bacsi.GetBS();
        }
    }
}
