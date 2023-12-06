using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DALKhoa
    {
        DB db = new DB();
        public DataTable GetKhoa()
        {
            SqlDataAdapter bnAdapter = new SqlDataAdapter();
            bnAdapter.SelectCommand = new SqlCommand("select * from Khoa", db.conn);
            DataTable tbl_benhnhan = new DataTable();
            bnAdapter.Fill(tbl_benhnhan);
            return tbl_benhnhan;
        }

        public DataTable GetBenhNhanByMaKhoa(string MaKhoa)
        {
            SqlDataAdapter bnAdapter = new SqlDataAdapter();
            bnAdapter.SelectCommand = new SqlCommand("select * from BenhNhan where MaKhoa = @MaKhoa", db.conn);
            bnAdapter.SelectCommand.Parameters.AddWithValue("@MaKhoa", MaKhoa);
            DataTable tbl_benhnhan = new DataTable();
            bnAdapter.Fill(tbl_benhnhan);
            return tbl_benhnhan;
        }
        public string GetName(string makhoa)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select TenKhoa from Khoa where MaKhoa = @MaKhoa";
            cmd.Parameters.AddWithValue("@MaKhoa", makhoa);
            cmd.Connection = db.conn;
            db.conn.Open();
            var objreturn = cmd.ExecuteScalar();
            db.conn.Close();
            return (string)objreturn;
        }
    }
}
