using DataTransferObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class DALKhamBenh
    {
        DB db = new DB();
        public int Insert(DTOKhamBenh khambenh)
        {
            SqlCommand sql = new SqlCommand();
            sql.Connection = db.conn;
            sql.CommandText = "insert into ct_KhamBenh(MaBS, MaBN, ThoiGian, Ketluan) " +
                "VALUES(@bs, @bn, @tg, @kl); SELECT SCOPE_IDENTITY()";
            sql.Parameters.AddWithValue("@bs", khambenh.MaBS);
            sql.Parameters.AddWithValue("@bn", khambenh.MaBN);
            sql.Parameters.AddWithValue("@tg", DateTime.Now);
            sql.Parameters.AddWithValue("@kl", khambenh.KetLuan);
            db.conn.Open();
            var id = sql.ExecuteScalar();
            db.conn.Close();
            return int.Parse(id.ToString());
        }
        public int InsertKetLuan(int maphieu,string ketluan)
        {
            int result;
            SqlCommand sql = new SqlCommand();
            sql.Connection = db.conn;
            sql.CommandText = "UPDATE ct_KhamBenh SET KetLuan = @KetLuan WHERE MaPhieu = @MaPhieu;";
            sql.Parameters.AddWithValue("@MaPhieu",maphieu);
            sql.Parameters.AddWithValue("@KetLuan", ketluan);
            db.conn.Open();
            result = sql.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
        // kiểm tra xem bệnh nhân có trong bảng ct_KhamBenh hay chưa
        public int selectBN(int mabn)
        {
            int id;
            SqlCommand sql = new SqlCommand();
            sql.Connection = db.conn;
            sql.CommandText = "SELECT COUNT(*) AS SLBenhNhan FROM ct_KhamBenh where MaBN = @MaBN";
            sql.Parameters.AddWithValue("@MaBN", mabn);
            db.conn.Open();
            id = int.Parse(sql.ExecuteScalar().ToString());
            db.conn.Close();
            return id;
        }
        // lấy mã phiếu của bệnh nhân đã có
        public int selectID(int mabn)
        {
            int id;
            SqlCommand sql = new SqlCommand();
            sql.Connection = db.conn;
            sql.CommandText = "SELECT MaPhieu FROM ct_KhamBenh where MaBN = @MaBN";
            sql.Parameters.AddWithValue("@MaBN", mabn);
            db.conn.Open();
            id = int.Parse(sql.ExecuteScalar().ToString());
            db.conn.Close();
            return id;
        }
    }
}