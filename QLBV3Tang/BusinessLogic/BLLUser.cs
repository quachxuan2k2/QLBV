using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.Data;
using DataTransferObject;

namespace BusinessLogicLayer
{
    public class BLLUser
    {
        //0: hop le
        //1: username khong ton tai trong csdl
        //2: pass va username khong hop le

        DALUser daluser = new DALUser();
        public DataTable getuser()
        {
            return daluser.GetUser();
        }
        public DataTable getalluser()
        {
            return daluser.getalluser();
        }
        public int CheckUser(string username, string pass)
        {
            if (!daluser.CheckUserExits(username))
            {
                return -1;
            }
            else
            {
                if (!daluser.CheckUser(username, pass))
                {
                    return -2;
                }
                else
                {
                    return daluser.GetMaBacsi(username);
                }
            }
        }
        public int selectMaTK(string username)
        {
            return daluser.selectMaTK(username);
        }
        public int InsertUser(DTOTaiKhoan taikhoan)
        {
            // 1: người dùng đã tồn tại, không được thêm
            //2: người dùng chưa tồn tại, cho phép thêm
            if (daluser.CheckUserExits(taikhoan.tenDangNhap))
            {
                return 0;
            }
            else
            {
                return daluser.AddUser(taikhoan);
            }
        }

        public int InsertUserIsBS(DTOTaiKhoan taikhoan)
        {
            if (daluser.CheckUserExits(taikhoan.tenDangNhap))
            {
                return 0;
            }
            else
            {
                return daluser.AddUserIsBS(taikhoan);
            }
        }
        
    }

}
