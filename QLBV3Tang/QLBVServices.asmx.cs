using DataAccessLayer;
using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;

namespace QLBV3Tang
{
    /// <summary>
    /// Summary description for QLBV
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class QLBV : System.Web.Services.WebService
    {
        /*****************************************************************************************/
        DALBacSi dal_bacsi = new DALBacSi();
        [WebMethod]
        public int InsertBacSi(DTOBacSi bacsi)
        {
            int IDExist = dal_bacsi.AddBS(bacsi);
            return IDExist;
        }
        [WebMethod]
        public int InsertBacSiK(DTOBacSi bacsi)
        {
            int IDExist = dal_bacsi.AddBS1(bacsi);
            return IDExist;
        }
        [WebMethod]
        public DataSet getBS()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dal_bacsi.GetBS());
            return ds;
        }
        /************************************************************************************************/
        DALDichVu dal_dichvu = new DALDichVu();
        [WebMethod]
        public DataSet getDV()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dal_dichvu.GetDV());
            return ds;
        }
        [WebMethod]
        // nếu mã dịch vụ là XQ,SIE,NSOI => trả về 1
        // nếu mã dịch vụ khác trả về 2
        // ngoại lệ là 3
        public int CheckDV(int matk)
        {
            if (dal_dichvu.CheckMaBS(matk))   // có mã bác sĩ
            {
                if (dal_dichvu.CheckDVExits(matk))   // có mã dịch vụ
                {
                    if (dal_dichvu.CheckDV(matk) == 1)
                    {
                        return 1;   // XQ, Siêu âm, Nội soi
                    }
                    else if (dal_dichvu.CheckDV(matk) == 2)
                    {
                        return 2;  // Xét nghiệm
                    }
                    else
                    {
                        return 4;
                    }
                }
                else
                {
                    return 0; // không có mã dịch vụ
                }
            }
            if(!dal_dichvu.CheckMaBS(matk))
            {
                return 0;   // admin, lễ tân
            }
            else
            {
                return 0;
            }
        }
        [WebMethod]
        public int AddDichvu(DTODichVu dichvu)
        {
            return dal_dichvu.AddDichvu(dichvu);
        }
        [WebMethod]
        public int AddChiTiet_DV(DTOChiTietDV ct_DV)
        {
            return dal_dichvu.AddChiTiet_DV(ct_DV);
        }
        [WebMethod]
        public DataSet GetDichVu()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add( dal_dichvu.GetDichVu());
            return ds;
        }
        [WebMethod]
        public DataSet GetChiTiet_DV(int maphieu)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dal_dichvu.GetChiTiet_DV(maphieu));
            return ds;
        }
        [WebMethod]
        public int UpdateCTDV(int maphieu, DTOChiTietDV ct_dv)
        {
            return dal_dichvu.UpdateCTDV(maphieu, ct_dv);
        }
        [WebMethod]
        public int UpdateCTDV1(int maphieu, DTOChiTietDV ct_dv)
        {
            return dal_dichvu.UpdateCTDV1(maphieu, ct_dv);
        }
        [WebMethod]
        public int UpdateDV(string madv, DTODichVu dv)
        {
            return dal_dichvu.UpdateDV(madv, dv);
        }
        [WebMethod]
        public int DeleteDV(string madv)
        {
            return dal_dichvu.DeleteDV(madv);
        }
        /*******************************************************************************************/
        DALBenhNhan dalBenhnhan = new DALBenhNhan();
        [WebMethod]
        public DataSet GetBenhNhanXetNghiem()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dalBenhnhan.GetBenhNhanXetNghiem());
            return ds;
        }
        [WebMethod]
        public BenhAnDataSet GetBenhNhanReport(int maphieu)
        {
            string connection = "Data Source = ADMIN\\XUANDEBUG; " + "Initial Catalog = QLBV; " + "Integrated Security = True";
            BenhAnDataSet ds = new BenhAnDataSet();
            SqlConnection conn = new SqlConnection(connection);
            string sql = "SELECT DIStinct BN.HoTen AS HoTenBenhNhan, BN.NgaySinh, BN.DiaChi, BN.GioiTinh, DVu.TenDV, DVu.Gia, DV.AnhChup, CT.KetLuan, BS.HoTen AS HoTenBacSi FROM ct_KhamBenh CT JOIN BenhNhan BN ON CT.MaBN = BN.MaBN JOIN ct_DichVu DV ON CT.MaPhieu = DV.MaPhieu JOIN DichVu DVu ON DV.MaDV = DVu.MaDV JOIN BacSi BS ON CT.MaBS = BS.MaBS WHERE CT.MaPhieu = @MaPhieu; ";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@MaPhieu",maphieu);
            adapter.Fill(ds.Tables[0]);
            return ds;
        }
        [WebMethod]
        public BenhAnDataSet GetDonThuocReport(int madonthuoc)
        {
            string connection = "Data Source = ADMIN\\XUANDEBUG; " + "Initial Catalog = QLBV; " + "Integrated Security = True";
            BenhAnDataSet ds = new BenhAnDataSet();
            SqlConnection conn = new SqlConnection(connection);
            string sql = "SELECT dt.SoLuong,dt.ThanhTien,dt.LieuDung,t.TenThuoc,t.GiaBan FROM ct_DonThuoc dt JOIN Thuoc t ON dt.MaThuoc = t.MaThuoc WHERE dt.MaDonThuoc = @MaDonThuoc";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@MaDonThuoc", madonthuoc);
            adapter.Fill(ds.Tables[1]);
            return ds;
        }
        [WebMethod]
        public DataSet GetBenhNhanCDHA()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dalBenhnhan.GetBenhNhanCDHA());
            return ds;
        }
        [WebMethod]
        public DataSet GetBenhNhanCoKQ()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dalBenhnhan.GetBenhNhanCoKQ());
            return ds;
        }
        [WebMethod]
        public DataSet GetAllBenhNhan()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dalBenhnhan.GetBenhNhan());
            return ds;
        }
        [WebMethod]
        public DataSet GetBenhNhanCho()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dalBenhnhan.GetBenhNhanCho());
            return ds;
        }
        [WebMethod]
        public int InsertBenhNhan(DTOBenhNhan benhnhan)
        {
            int IDExist = dalBenhnhan.CreateBenhNhan(benhnhan);
            return IDExist;
        }
        [WebMethod]
        public int UpdateStatus(int mabn)
        {
            return dalBenhnhan.UpdateStatus(mabn);
        }
        [WebMethod]
        public int UpdateStatus1(int mabn)
        {
            return dalBenhnhan.UpdateStatus1(mabn);
        }
        [WebMethod]
        public int UpdateStatus2(int mabn)
        {
            return dalBenhnhan.UpdateStatus2(mabn);
        }
        [WebMethod]
        public int selectMaBN(string tenbn)
        {
            return dalBenhnhan.selectMaBN(tenbn);
        }
        /**********************************************************************************/
        DALCT_DonThuoc dal_ctdonthuoc = new DALCT_DonThuoc();
        [WebMethod]
        public DataSet GetThuoc(int madonthuoc)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dal_ctdonthuoc.GetThuoc(madonthuoc));
            return ds;
        }
        [WebMethod]
        public int CreateCTDonThuoc(DTOCT_DonThuoc ct_donthuoc)
        {
            return dal_ctdonthuoc.CreateCTDonThuoc(ct_donthuoc);
        }
        
        /****************************************************************************************************/
        DALDichVuCha dal_dichvucha = new DALDichVuCha();
        [WebMethod]
        public DataSet getDVCha()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dal_dichvucha.GetDVCha());
            return ds;
        }
        /*******************************************************************************************************/
        DALDonThuoc dal_donthuoc = new DALDonThuoc();

        [WebMethod]
        public int InsertMaphieu(int maphieu)
        {
            return dal_donthuoc.InsertMaPhieu(maphieu);
        }
        [WebMethod]
        public int countMaDonThuoc(int madt)
        {
            return dal_donthuoc.countMaDonThuoc(madt);
        }
        [WebMethod]
        public int selectMaDonThuoc(int maphieu)
        {
            return dal_donthuoc.selectMaDonThuoc(maphieu);
        }
        /******************************************************************************************************/
        DALKhamBenh dal_khambenh = new DataAccessLayer.DALKhamBenh();
        [WebMethod]
        public int InsertCTKhambenh(DTOKhamBenh khambenh)
        {
            return dal_khambenh.Insert(khambenh);
        }
        [WebMethod]
        public int InsertKetLuan(int maphieu, string ketluan)
        {
            return dal_khambenh.InsertKetLuan(maphieu, ketluan);
        }
        //số lượng bn
        [WebMethod]
        public int selectBN(int mabn)
        {
            return dal_khambenh.selectBN(mabn);
        }
        // lấy mã phiếu của bệnh nhân
        [WebMethod]
        public int selectID(int mabn)
        {
            return dal_khambenh.selectID(mabn);
        }
        /******************************************************************************************************/
        DALKhoa dal_Khoa = new DALKhoa();
        [WebMethod]
        public DataSet GetAllKhoa()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dal_Khoa.GetKhoa());
            return ds;
        }
        [WebMethod]
        public DataSet GetAllBenhNhanByKhoa(string MaKhoa)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dal_Khoa.GetBenhNhanByMaKhoa(MaKhoa));
            return ds;
        }
        [WebMethod]
        public string GetName(string makhoa)
        {
            return dal_Khoa.GetName(makhoa);
        }
        /***********************************************************************************************/
        DALThuoc dal_thuoc = new DALThuoc();
        [WebMethod]
        public int InsertThuoc(DTOThuoc thuoc)
        {
            int IDExist = dal_thuoc.CreateThuoc(thuoc);
            return IDExist;
        }
        [WebMethod]
        public int DeleteThuoc(string mathuoc)
        {
            return dal_thuoc.DeleteThuoc(mathuoc);
        }
        [WebMethod]
        public DataSet getThuoc()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dal_thuoc.GetThuoc());
            return ds;
        }
        [WebMethod]
        public int UpdateThuoc(DTOThuoc thuoc)
        {
            return dal_thuoc.UpdateThuoc(thuoc);
        }
        [WebMethod]
        public DataSet GetMaThuoc()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(dal_thuoc.GetMaThuoc());
            return ds;
        }
        [WebMethod]
        public int GetGiabyMaThuoc(string mathuoc)
        {
            return dal_thuoc.GetGiabyMaThuoc(mathuoc);
        }
        /***************************************************************************************************/
        DALUser daluser = new DALUser();
        [WebMethod]
        public DataSet getuser()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(daluser.GetUser());
            return ds;
        }
        [WebMethod]
        public int UpdateUser(DTOTaiKhoan taikhoan)
        {
            return daluser.UpdateUser(taikhoan);
        }
        [WebMethod]
        public int UpdateUserIsDocTor(DTOTaiKhoan taikhoan)
        {
            return daluser.UpdateUserIsDoctor(taikhoan);
        }
        [WebMethod]
        public DataSet getalluser()
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(daluser.getalluser());
            return ds;
        }
        [WebMethod]
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
        
        [WebMethod]
        public int selectMaTK(string username)
        {
            return daluser.selectMaTK(username);
        }
        [WebMethod]
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
        [WebMethod]
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