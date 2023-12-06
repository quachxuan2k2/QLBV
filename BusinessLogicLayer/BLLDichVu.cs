using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataAccessLayer;
using DataTransferObject;

namespace BusinessLogicLayer
{
    public class BLLDichVu
    {
        DALDichVu dal_dichvu = new DALDichVu();
        public DataTable getDV()
        {
            return dal_dichvu.GetDV();
        }
        public int AddDichvu(DTODichVu dichvu)
        {
            return dal_dichvu.AddDichvu(dichvu);
        }
        public int AddChiTiet_DV(DTOChiTietDV ct_DV)
        {
            return dal_dichvu.AddChiTiet_DV(ct_DV);
        }
        public DataTable GetDichVu()
        {
            return dal_dichvu.GetDichVu();
        }
        public DataTable GetChiTiet_DV(int maphieu)
        {
            return dal_dichvu.GetChiTiet_DV(maphieu);
        }
        public int UpdateCTDV(int maphieu, DTOChiTietDV ct_dv)
        {
            return dal_dichvu.UpdateCTDV(maphieu, ct_dv);
        }
    }
}
