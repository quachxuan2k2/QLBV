using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

using DataTransferObject;

namespace DataAccessLayer
{
    public class DALUser
    {
        DB db = new DB();
        SqlDataAdapter usAdapter = new SqlDataAdapter();
        public DataTable GetUser()
        {
            usAdapter = new SqlDataAdapter();
            usAdapter.SelectCommand = new SqlCommand("SELECT TaiKhoan.TenDangNhap,BacSi.HoTen,TaiKhoan.MaBS FROM TaiKhoan INNER JOIN BacSi ON TaiKhoan.MaBS = BacSi.MaBS", db.conn);
            DataTable tbl_user = new DataTable();
            usAdapter.Fill(tbl_user);
            return tbl_user;  
        }
        public bool CheckUserExits(string username)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select MaTK from TaiKhoan where TenDangNhap = @TenDangNhap";
            cmd.Parameters.AddWithValue("@TenDangNhap", username);
            cmd.Connection = db.conn;
            db.conn.Open();
            var objreturn = cmd.ExecuteScalar();
            db.conn.Close();
            if(objreturn != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public int selectMaTK(string username)
        {
            int id;
            SqlCommand sql = new SqlCommand();
            sql.Connection = db.conn;
            sql.CommandText = "select MaTK from TaiKhoan where TenDangNhap = @TenDangNhap";
            sql.Parameters.AddWithValue("@TenDangNhap", username);
            db.conn.Open();
            id = int.Parse(sql.ExecuteScalar().ToString());
            db.conn.Close();
            return id;
        }
        public bool CheckUser(string username, string pass)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select MaTK from TaiKhoan where TenDangNhap = @TenDangNhap and MatKhau = @MatKhau";
            cmd.Parameters.AddWithValue("@TenDangNhap", username);
            cmd.Parameters.AddWithValue("@MatKhau", pass);
            cmd.Connection = db.conn;
            db.conn.Open();
            var objreturn  = cmd.ExecuteScalar();
            db.conn.Close();
            if(objreturn != null)
            {
                return true ;
            }
            else
            {
                return false;
            }
        }
        public int GetMaBacsi(string username)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select MaBS from TaiKhoan where TenDangNhap = @TenDangNhap";
            cmd.Parameters.AddWithValue("@TenDangNhap", username);
            cmd.Connection = db.conn;
            db.conn.Open();
            var objreturn = cmd.ExecuteScalar();
            db.conn.Close();
            if (objreturn.ToString().Trim() != "")
                return int.Parse(objreturn.ToString());
            else
                return 0;
        }
        public int AddUser(DTOTaiKhoan taikhoan)
        {
            int result;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into TaiKhoan(TenDangNhap,MatKhau)" + 
                                "values(@TenDangNhap, @MatKhau)";
            cmd.Parameters.AddWithValue("@TenDangNhap", taikhoan.tenDangNhap);
            cmd.Parameters.AddWithValue("@MatKhau", taikhoan.matKhau);
           // cmd.Parameters.AddWithValue("@MaBS", taikhoan.MaBS);   
            cmd.Connection = db.conn;
            db.conn.Open();
            result = cmd.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
        public int AddUserIsBS(DTOTaiKhoan taikhoan)
        {
            int result;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into TaiKhoan(TenDangNhap,MatKhau,MaBS)" +
                                "values(@TenDangNhap, @MatKhau, @MaBS)";
            cmd.Parameters.AddWithValue("@TenDangNhap", taikhoan.tenDangNhap);
            cmd.Parameters.AddWithValue("@MatKhau", taikhoan.matKhau);
            cmd.Parameters.AddWithValue("@MaBS", taikhoan.MaBS);   
            cmd.Connection = db.conn;
            db.conn.Open();
            result = cmd.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
        public DataTable getalluser()
        {
            usAdapter = new SqlDataAdapter();
            usAdapter.SelectCommand = new SqlCommand("select * from TaiKhoan", db.conn);
            DataTable tbl_user = new DataTable();
            usAdapter.Fill(tbl_user);
            return tbl_user;
        }
        public int UpdateUser(DTOTaiKhoan taiKhoan)
        {
            int result;
            string Command = "UPDATE TaiKhoan SET TenDangNhap=@TenDangNhap,MatKhau=@MatKhau WHERE MaTK = @MaTK";
            SqlCommand sql = new SqlCommand(Command, db.conn);
            sql.Parameters.AddWithValue("@TenDangNhap", taiKhoan.tenDangNhap);
            sql.Parameters.AddWithValue("@MatKhau", taiKhoan.matKhau);

            sql.Parameters.AddWithValue("@MaTK",taiKhoan.maTK);
            db.conn.Open();
            result = sql.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
        public int UpdateUserIsDoctor(DTOTaiKhoan taiKhoan)
        {
            int result;
            string Command = "UPDATE TaiKhoan SET TenDangNhap=@TenDangNhap,MatKhau=@MatKhau,MaBS=@MaBS WHERE MaTK = @MaTK";
            SqlCommand sql = new SqlCommand(Command, db.conn);
            sql.Parameters.AddWithValue("@TenDangNhap", taiKhoan.tenDangNhap);
            sql.Parameters.AddWithValue("@MatKhau", taiKhoan.matKhau);
            sql.Parameters.AddWithValue("@MaBS", taiKhoan.MaBS);

            sql.Parameters.AddWithValue("@MaTK", taiKhoan.maTK);
            db.conn.Open();
            result = sql.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
    }
}