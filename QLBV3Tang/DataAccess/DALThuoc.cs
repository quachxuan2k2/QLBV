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
    public class DALThuoc
    {
        DB db = new DB();
        SqlDataAdapter thuocAdapter = new SqlDataAdapter();
        public DataTable GetMaThuoc()
        {
            thuocAdapter = new SqlDataAdapter();
            thuocAdapter.SelectCommand = new SqlCommand("select MaThuoc,TenThuoc from Thuoc", db.conn);
            DataTable tbl_benhnhan = new DataTable();
            thuocAdapter.Fill(tbl_benhnhan);
            return tbl_benhnhan;
        }
        public DataTable GetThuoc()
        {
            thuocAdapter = new SqlDataAdapter();
            thuocAdapter.SelectCommand = new SqlCommand("select * from Thuoc", db.conn);
            DataTable tbl_thuoc = new DataTable();
            thuocAdapter.Fill(tbl_thuoc);
            return tbl_thuoc;
        }
        public int UpdateThuoc(DTOThuoc thuoc)
        {
            int result;
            string Command = "UPDATE Thuoc SET MaThuoc=@MaThuoc,TenThuoc=@TenThuoc,GiaNhap=@GiaNhap,GiaBan=@GiaBan,HangSX=@HangSX,Nuoc=@Nuoc WHERE MaThuoc = @MaThuoc";
            SqlCommand sql = new SqlCommand(Command, db.conn);
            sql.Parameters.AddWithValue("@MaThuoc", thuoc.maThuoc);
            sql.Parameters.AddWithValue("@TenThuoc", thuoc.tenThuoc);
            sql.Parameters.AddWithValue("@GiaNhap", thuoc.giaNhap);
            sql.Parameters.AddWithValue("@HangSX", thuoc.hangSanXuat);
            sql.Parameters.AddWithValue("@Nuoc", thuoc.nuoc);
            sql.Parameters.AddWithValue("@GiaBan", thuoc.giaBan);
            db.conn.Open();
            result = sql.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
        public int GetGiabyMaThuoc(string mathuoc)
        {
            int id = 0;
            SqlCommand sql = new SqlCommand();
            sql.Connection = db.conn;
            sql.CommandText = "select GiaBan from Thuoc where MaThuoc=@MaThuoc";
            sql.Parameters.AddWithValue("@MaThuoc", mathuoc);
            db.conn.Open();
            object result = sql.ExecuteScalar();
            if (result != null && int.TryParse(result.ToString(), out int giaBan))
            {
                id = giaBan;
                
            }
            else
            {
                id = 0;
            }
            db.conn.Close();
            return id;
        }
        public int CreateThuoc(DTOThuoc thuoc)
        {
            int result;
            string Command = "insert into Thuoc(MaThuoc,TenThuoc,GiaNhap,GiaBan,HangSX,Nuoc)" +
                "values(@MaThuoc,@TenThuoc,@GiaNhap,@GiaBan,@HangSX,@Nuoc)";
            SqlCommand sql = new SqlCommand(Command, db.conn);
            sql.Parameters.AddWithValue("@MaThuoc", thuoc.maThuoc);
            sql.Parameters.AddWithValue("@TenThuoc", thuoc.tenThuoc);
            sql.Parameters.AddWithValue("@GiaNhap", thuoc.giaNhap);
            sql.Parameters.AddWithValue("@GiaBan", thuoc.giaBan);
            sql.Parameters.AddWithValue("@HangSX", thuoc.hangSanXuat);
            sql.Parameters.AddWithValue("@Nuoc", thuoc.nuoc);

            db.conn.Open();
            result = sql.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
        public int DeleteThuoc(string mathuoc)
        {
            int result;
            string Command = "delete from Thuoc where MaThuoc = @MaThuoc";
            SqlCommand sql = new SqlCommand(Command, db.conn);
            sql.Parameters.AddWithValue("@MaThuoc", mathuoc);
            db.conn.Open();
            result = sql.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
    } 
}