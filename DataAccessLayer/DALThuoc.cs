using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

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
    } 
}