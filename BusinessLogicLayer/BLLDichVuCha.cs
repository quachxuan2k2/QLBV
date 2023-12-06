using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BusinessLogicLayer
{
    public class BLLDichVuCha
    {
        DALDichVuCha dal_dichvucha = new DALDichVuCha();
        public DataTable getDVCha()
        {
            return dal_dichvucha.GetDVCha();
        }
    }
}
