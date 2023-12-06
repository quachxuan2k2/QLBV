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
            dvAdapter.SelectCommand = new SqlCommand("select * from ct_DichVu where MaPhieu = @MaPhieu", db.conn);
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
