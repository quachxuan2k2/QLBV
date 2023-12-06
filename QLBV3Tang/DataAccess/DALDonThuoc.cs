using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class DALDonThuoc
    {
        DB db = new DB();
        public int InsertMaPhieu(int maphieu)
        {
            string Command = "insert into DonThuoc(MaPhieu) values(@MaPhieu);SELECT SCOPE_IDENTITY()";
            SqlCommand sql = new SqlCommand(Command, db.conn);
            sql.Parameters.AddWithValue("@MaPhieu",maphieu);
            db.conn.Open();
            var result = sql.ExecuteScalar();
            db.conn.Close();
            return int.Parse(result.ToString());
        }
        public int countMaDonThuoc(int maphieu)
        {
            int id;
            SqlCommand sql = new SqlCommand();
            sql.Connection = db.conn;
            sql.CommandText = "SELECT COUNT(*) AS madonthuoc FROM DonThuoc where MaPhieu = @MaPhieu";
            sql.Parameters.AddWithValue("@MaPhieu", maphieu);
            db.conn.Open();
            id = int.Parse(sql.ExecuteScalar().ToString());
            db.conn.Close();
            return id;
        }
        public int selectMaDonThuoc(int maphieu)
        {
            int id;
            SqlCommand sql = new SqlCommand();
            sql.Connection = db.conn;
            sql.CommandText = "SELECT MaDonThuoc FROM DonThuoc where MaPhieu = @MaPhieu";
            sql.Parameters.AddWithValue("@MaPhieu", maphieu);
            db.conn.Open();
            id = int.Parse(sql.ExecuteScalar().ToString());
            db.conn.Close();
            return id;
        }
    }
}