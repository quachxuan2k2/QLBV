using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataTransferObject;

namespace DataAccessLayer
{
    public class DALDichVu
    {
        DB db = new DB();
        SqlDataAdapter dvadapter = new SqlDataAdapter();
        public DataTable GetDV()
        {
            SqlCommand command = new SqlCommand("select * from DichVu",db.conn);
            dvadapter = new SqlDataAdapter(command);
            DataTable table_dv = new DataTable();
            dvadapter.Fill(table_dv);
            return table_dv;
        }
        public bool CheckMaBS(int matk)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT MaBS FROM  TaiKhoan WHERE MaTK = @MaTK";
            cmd.Parameters.AddWithValue("@MaTK", matk);
            cmd.Connection = db.conn;
            db.conn.Open();
            var objreturn = cmd.ExecuteScalar();
            db.conn.Close();
            if (objreturn != null)
            {
                return true; // nếu có mã bác sĩ, trả về true
            }
            else
            {
                return false;  // không có mã bác sĩ trả về false
            }
        }
        public bool CheckDVExits(int matk)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT BacSi.MaDV FROM BacSi JOIN TaiKhoan ON BacSi.MaBS = TaiKhoan.MaBS WHERE TaiKhoan.MaTK = @MaTK";
            cmd.Parameters.AddWithValue("@MaTK", matk);
            cmd.Connection = db.conn;
            db.conn.Open();
            var objreturn = cmd.ExecuteScalar();
            db.conn.Close();
            if (objreturn != null)
            {
                return true; // nếu có mã dịch vụ, trả về true
            }
            else
            {
                return false;  // không có mã dịch vụ trả về false
            }
        }
        public int CheckDV(int matk)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT BacSi.MaDV FROM BacSi JOIN TaiKhoan ON BacSi.MaBS = TaiKhoan.MaBS WHERE TaiKhoan.MaTK = @MaTK";
            cmd.Parameters.AddWithValue("@MaTK", matk);
            cmd.Connection = db.conn;
            db.conn.Open();
            var objreturn = cmd.ExecuteScalar();
            db.conn.Close();
            if (objreturn.ToString() == "XQ" || objreturn.ToString() == "SIE" || objreturn.ToString() == "NSOI")
            {
                return 1; // nếu mã dịch vụ là XQ,SIE,NSOI => trả về 1
            }
             else if(objreturn.ToString() == "XET")
            {
                return 2;  // nếu mã dịch vụ khác trả về 2
            }
            else
            {
                return 3;
            }
            
        }
        public DataTable GetDichVu()
        {
            SqlDataAdapter dvAdapter = new SqlDataAdapter();
            dvAdapter.SelectCommand = new SqlCommand("select * from DichVu", db.conn);
            DataTable tbl_dichvu = new DataTable();
            dvAdapter.Fill(tbl_dichvu);
            return tbl_dichvu;
        }
        public int AddDichvu(DTODichVu dichvu)
        {
            int result;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into DichVu(MaDV,TenDV,Gia,MaDVCha)" +
                                "values(@MaDV, @TenDV,@Gia,@MaDVCha)";
            cmd.Parameters.AddWithValue("@MaDV",dichvu.maDV);
            cmd.Parameters.AddWithValue("@TenDV",dichvu.tenDV);
            cmd.Parameters.AddWithValue("@Gia", dichvu.gia);
            cmd.Parameters.AddWithValue("@MaDVCha", dichvu.MaDVCha);
            cmd.Connection = db.conn;
            db.conn.Open();
            result = cmd.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
        public int AddChiTiet_DV(DTOChiTietDV ct_DichVu)
        {
            int result;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into ct_DichVu(MaDV,MaPhieu)" +
                                "values(@MaDV, @MaPhieu)";
            cmd.Parameters.AddWithValue("@MaDV", ct_DichVu.MaDV);
            cmd.Parameters.AddWithValue("@MaPhieu", ct_DichVu.MaPhieu);
            cmd.Connection = db.conn;
            db.conn.Open();
            result = cmd.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
        public DataTable GetChiTiet_DV(int maphieu)
        {
            SqlDataAdapter dvAdapter = new SqlDataAdapter();
            dvAdapter.SelectCommand = new SqlCommand("SELECT dv.MaDV, dv.TenDV, dv.Gia, dv.MaDVCha FROM ct_DichVu ct JOIN DichVu dv ON ct.MaDV = dv.MaDV WHERE ct.MaPhieu = @MaPhieu", db.conn);
            dvAdapter.SelectCommand.Parameters.AddWithValue("@MaPhieu", maphieu);
            DataTable tbl_dichvu = new DataTable();
            dvAdapter.Fill(tbl_dichvu);
            return tbl_dichvu;
        }
        public int UpdateCTDV(int maphieu,DTOChiTietDV ct_dv)
        {
            int result;
            string Command = "UPDATE ct_DichVu SET KetQuaDV = @KetQuaDV, AnhChup = @AnhChup WHERE MaPhieu = @MaPhieu";
            SqlCommand sql = new SqlCommand(Command, db.conn);
            sql.Parameters.AddWithValue("@MaPhieu", maphieu);
            sql.Parameters.AddWithValue("@KetQuaDV", ct_dv.KetQuaDV);
            sql.Parameters.AddWithValue("@AnhChup", ct_dv.AnhChup);
            db.conn.Open();
            result = sql.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
        public int UpdateCTDV1(int maphieu, DTOChiTietDV ct_dv)
        {
            int result;
            string Command = "UPDATE ct_DichVu SET KetQuaDV = @KetQuaDV WHERE MaPhieu = @MaPhieu";
            SqlCommand sql = new SqlCommand(Command, db.conn);
            sql.Parameters.AddWithValue("@MaPhieu", maphieu);
            sql.Parameters.AddWithValue("@KetQuaDV", ct_dv.KetQuaDV);
            db.conn.Open();
            result = sql.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
        public int UpdateDV(string madv, DTODichVu dv)
        {
            int result;
            string Command = "UPDATE DichVu SET TenDV = @TenDV, Gia = @Gia WHERE MaDV = @MaDV";
            SqlCommand sql = new SqlCommand(Command, db.conn);
            sql.Parameters.AddWithValue("@TenDV", dv.tenDV);
            sql.Parameters.AddWithValue("@Gia", dv.gia);
            sql.Parameters.AddWithValue("@MaDV", madv);
            db.conn.Open();
            result = sql.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
        public int DeleteDV(string madv)
        {
            int result;
            string Command = "DELETE FROM DichVu WHERE MaDV = @MaDV;";
            SqlCommand sql = new SqlCommand(Command, db.conn);
            sql.Parameters.AddWithValue("@MaDV", madv);
            db.conn.Open();
            result = sql.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
        public int UpdateStatus(int mabn)
        {
            int result;
            string Command = "UPDATE BenhNhan SET TrangThai = N'Đã có kết quả' WHERE MaBN = @MaBN";
            SqlCommand sql = new SqlCommand(Command, db.conn);
            sql.Parameters.AddWithValue("@MaBN", mabn);
            db.conn.Open();
            result = sql.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
    }
}
