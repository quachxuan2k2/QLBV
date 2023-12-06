using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALBenhNhan
    {
        DB db = new DB();
        SqlDataAdapter bnAdapter = new SqlDataAdapter();
        public DataTable GetBenhNhanXetNghiem()
        {
            bnAdapter = new SqlDataAdapter();
            bnAdapter.SelectCommand = new SqlCommand("SELECT bn.HoTen,bn.NgaySinh,bn.GioiTinh,bn.DiaChi,TrieuChung,TenDV,Gia,bs.HoTen FROM BenhNhan AS bn INNER JOIN ct_KhamBenh AS kb ON bn.MaBN = kb.MaBN INNER JOIN BacSi AS bs ON bs.MaBS = bs.MaBS INNER JOIN ct_DichVu AS dv ON kb.MaPhieu = dv.MaPhieu " +
                "INNER JOIN DichVu AS dvu ON dv.MaDV = dvu.MaDV INNER JOIN DichVuCha AS dvuc ON dvu.MaDVCha = dvuc.MaDVCha WHERE dvuc.MaDVCha ='XET' and bn.TrangThai=N'Đang khám';", db.conn);
            DataTable tbl_bn = new DataTable();
            bnAdapter.Fill(tbl_bn);
            return tbl_bn;
        }
        public DataTable GetBenhNhanCDHA()
        {
            bnAdapter = new SqlDataAdapter();
            bnAdapter.SelectCommand = new SqlCommand("SELECT DIStinct kb.MaPhieu,bn.HoTen,bn.MaBN,bn.NgaySinh,bn.GioiTinh,bn.DiaChi,TrieuChung,TenDV,Gia, bs.HoTen FROM BenhNhan AS bn INNER JOIN ct_KhamBenh AS kb ON bn.MaBN = kb.MaBN INNER JOIN BacSi AS bs ON kb.MaBS = bs.MaBS INNER JOIN ct_DichVu AS dv ON kb.MaPhieu = dv.MaPhieu INNER JOIN DichVu AS dvu ON dv.MaDV = dvu.MaDV INNER JOIN DichVuCha AS dvuc ON dvu.MaDVCha = dvuc.MaDVCha WHERE (dvuc.MaDVCha = 'NSOI'or dvuc.MaDVCha = 'SIE'or dvuc.MaDVCha = 'XQ') and dv.KetQuaDV is null; ", db.conn);
            DataTable tbl_bn = new DataTable();
            bnAdapter.Fill(tbl_bn);
            return tbl_bn;
        }
        public DataTable GetBenhNhanCoKQ()
        {
            bnAdapter = new SqlDataAdapter();
            bnAdapter.SelectCommand = new SqlCommand("SELECT DIStinct kb.MaPhieu,dv.KetQuaDV, bn.HoTen,bn.NgaySinh,bn.GioiTinh,bn.DiaChi,TrieuChung,TenDV,Gia, bs.HoTen FROM BenhNhan AS bn INNER JOIN ct_KhamBenh AS kb ON bn.MaBN = kb.MaBN INNER JOIN BacSi AS bs ON kb.MaBS = bs.MaBS INNER JOIN ct_DichVu AS dv ON kb.MaPhieu = dv.MaPhieu INNER JOIN DichVu AS dvu ON dv.MaDV = dvu.MaDV INNER JOIN DichVuCha AS dvuc ON dvu.MaDVCha = dvuc.MaDVCha WHERE(dvuc.MaDVCha = 'NSOI'or dvuc.MaDVCha = 'SIE'or dvuc.MaDVCha = 'XQ') and dv.KetQuaDV is not null and TrangThai <> N'Đã khám'; ", db.conn);
            DataTable tbl_bn = new DataTable();
            bnAdapter.Fill(tbl_bn);
            return tbl_bn;
        }
        public int selectMaBN(string tenbn)
        {
            int id;
            SqlCommand sql = new SqlCommand();
            sql.Connection = db.conn;
            sql.CommandText = "select MaBN from BenhNhan where HoTen=@HoTen";
            sql.Parameters.AddWithValue("HoTen",tenbn);
            db.conn.Open();
            id = int.Parse(sql.ExecuteScalar().ToString());
            db.conn.Close();
            return id;
        }
        public DataTable GetBenhNhan()
        {
            bnAdapter = new SqlDataAdapter();
            bnAdapter.SelectCommand = new SqlCommand("select * from BenhNhan", db.conn);
            DataTable tbl_benhnhan = new DataTable();
            bnAdapter.Fill(tbl_benhnhan);
            return tbl_benhnhan;
        }
        public void GetBenhNhanReport(int maphieu, DataTable table)
        {
            bnAdapter = new SqlDataAdapter();
            bnAdapter.SelectCommand = new SqlCommand("SELECT BN.HoTen AS HoTenBenhNhan, BN.NgaySinh, BN.DiaChi, BN.GioiTinh, DVu.TenDV, DVu.Gia, DV.AnhChup, CT.KetLuan, BS.HoTen AS HoTenBacSi FROM ct_KhamBenh CT JOIN BenhNhan BN ON CT.MaBN = BN.MaBN JOIN ct_DichVu DV ON CT.MaPhieu = DV.MaPhieu JOIN DichVu DVu ON DV.MaDV = DVu.MaDV JOIN BacSi BS ON CT.MaBS = BS.MaBS WHERE CT.MaPhieu = @MaPhieu; ", db.conn);
            bnAdapter.SelectCommand.Parameters.AddWithValue("@MaPhieu",maphieu);
            bnAdapter.Fill(table);
        }
        public DataTable GetBenhNhanCho()
        {
            bnAdapter = new SqlDataAdapter();
            bnAdapter.SelectCommand = new SqlCommand("SELECT * FROM BenhNhan WHERE TrangThai=N'Chờ' OR TrangThai='Ðang khám'", db.conn);
            DataTable tbl_benhnhan = new DataTable();
            bnAdapter.Fill(tbl_benhnhan);
            return tbl_benhnhan;
        }
        public int CreateBenhNhan(DTOBenhNhan benhnhan)
        {
            int result;
            string Command = "insert into BenhNhan(HoTen,DiaChi,Dienthoai,NgaySinh,GioiTinh,TrieuChung,MaBHYT,MaKhoa,TrangThai)" +
                "values(@HoTen,@DiaChi,@Dienthoai,@NgaySinh,@GioiTinh,@TrieuChung,@MaBHYT,@MaKhoa,N'Chờ')";
            SqlCommand sql = new SqlCommand(Command, db.conn);
            sql.Parameters.AddWithValue("@GioiTinh", benhnhan.GioiTinh);
            sql.Parameters.AddWithValue("@TrieuChung", benhnhan.TrieuChung);
            sql.Parameters.AddWithValue("@MaBHYT", benhnhan.MaBHYT);
            sql.Parameters.AddWithValue("@HoTen", benhnhan.HoTen);
            sql.Parameters.AddWithValue("@DiaChi", benhnhan.DiaChi);
            sql.Parameters.AddWithValue("@Dienthoai", benhnhan.DienThoai);
            sql.Parameters.AddWithValue("@NgaySinh", DateTime.ParseExact(benhnhan.NgaySinh, "dd/MM/yyyy", null));
            sql.Parameters.AddWithValue("@MaKhoa", benhnhan.MaKhoa);

            db.conn.Open();
            result = sql.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
        public int UpdateStatus(int mabn)
        {
            int result;
            string Command = "UPDATE BenhNhan SET TrangThai = N'Đang khám' WHERE MaBN = @MaBN";
            SqlCommand sql = new SqlCommand(Command, db.conn);
            sql.Parameters.AddWithValue("@MaBN", mabn);
            db.conn.Open();
            result = sql.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
        public int UpdateStatus1(int mabn)
        {
            int result;
            string Command = "UPDATE BenhNhan SET TrangThai = N'Đã có CLS' WHERE MaBN = @MaBN";
            SqlCommand sql = new SqlCommand(Command, db.conn);
            sql.Parameters.AddWithValue("@MaBN", mabn);
            db.conn.Open();
            result = sql.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
        public int UpdateStatus2(int mabn)
        {
            int result;
            string Command = "UPDATE BenhNhan SET TrangThai = N'Đã khám' WHERE MaBN = @MaBN";
            SqlCommand sql = new SqlCommand(Command, db.conn);
            sql.Parameters.AddWithValue("@MaBN", mabn);
            db.conn.Open();
            result = sql.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
    }
}
