using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DataTransferObject;
using System.Data;

namespace DataAccessLayer
{
    public class DALBacSi
    {
        DB db = new DB();

        public bool CheckBSExits(string username)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select MaBS from BacSi where HoTen = @HoTen";
            cmd.Parameters.AddWithValue("HoTen", username);
            cmd.Connection = db.conn;
            db.conn.Open();
            var objreturn = cmd.ExecuteScalar();
            db.conn.Close();
            if (objreturn != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int AddBS(DTOBacSi bacsi)
        {
            int result;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into BacSi(HoTen,DiaChi,NgaySinh,GioiTinh,CCHN,MaDV)" +
                                "values(@HoTen,@DiaChi,@NgaySinh,@GioiTinh,@CCHN,@MaDV)";
            cmd.Parameters.AddWithValue("@HoTen", bacsi.HoTen);
            cmd.Parameters.AddWithValue("@DiaChi", bacsi.DiaChi);
            cmd.Parameters.AddWithValue("@NgaySinh", DateTime.ParseExact(bacsi.NgaySinh, "dd/MM/yyyy", null));
            cmd.Parameters.AddWithValue("@GioiTinh", bacsi.GioiTinh);
            cmd.Parameters.AddWithValue("@CCHN", bacsi.CCHN);
            cmd.Parameters.AddWithValue("@MaDV", bacsi.MaDV);

            cmd.Connection = db.conn;
            db.conn.Open();
            result = cmd.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
        public int AddBS1(DTOBacSi bacsi)
        {
            int result;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "insert into BacSi(HoTen,DiaChi,NgaySinh,GioiTinh,CCHN)" +
                                "values(@HoTen,@DiaChi,@NgaySinh,@GioiTinh,@CCHN)";
            cmd.Parameters.AddWithValue("@HoTen", bacsi.HoTen);
            cmd.Parameters.AddWithValue("@DiaChi", bacsi.DiaChi);
            cmd.Parameters.AddWithValue("@NgaySinh", DateTime.ParseExact(bacsi.NgaySinh, "dd/MM/yyyy", null));
            cmd.Parameters.AddWithValue("@GioiTinh", bacsi.GioiTinh);
            cmd.Parameters.AddWithValue("@CCHN", bacsi.CCHN);
            cmd.Connection = db.conn;
            db.conn.Open();
            result = cmd.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
        SqlDataAdapter dvadapter = new SqlDataAdapter();
        public DataTable GetBS()
        {
            SqlCommand command = new SqlCommand("select * from BacSi", db.conn);
            dvadapter = new SqlDataAdapter(command);
            DataTable table_dv = new DataTable();
            dvadapter.Fill(table_dv);
            return table_dv;
        }
    }
}
