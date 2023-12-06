using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class BLLKhamBenh
    {
        DataAccessLayer.DALKhamBenh dal_khambenh = new DataAccessLayer.DALKhamBenh();

        public int InsertCTKhambenh(DTOKhamBenh khambenh)
        {
            return dal_khambenh.Insert(khambenh);
        }
        public int InsertKetLuan(int maphieu, string ketluan)
        {
            return dal_khambenh.InsertKetLuan(maphieu, ketluan);
        }
        //số lượng bn
        public int selectBN(int mabn)
        {
            return dal_khambenh.selectBN(mabn);
        }
        // lấy mã phiếu của bệnh nhân
        public int selectID(int mabn)
        {
            return dal_khambenh.selectID(mabn);
        }
    }
}