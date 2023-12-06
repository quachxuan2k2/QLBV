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
    public class DALCT_DonThuoc
    {
        DB db = new DB();
        SqlDataAdapter ct_donthuoc = new SqlDataAdapter();
        public DataTable GetThuoc(int madonthuoc)
        {
            ct_donthuoc = new SqlDataAdapter();
            ct_donthuoc.SelectCommand = new SqlCommand("SELECT dt.MaDonThuoc, dt.MaThuoc, dt.SoLuong, dt.ThanhTien, dt.LieuDung, t.TenThuoc, t.GiaBan, t.HangSX FROM ct_DonThuoc dt JOIN Thuoc t ON dt.MaThuoc = t.MaThuoc WHERE dt.MaDonThuoc = @MaDonThuoc", db.conn);
            ct_donthuoc.SelectCommand.Parameters.AddWithValue("@MaDonThuoc", madonthuoc);
            DataTable tbl_benhnhan = new DataTable();
            ct_donthuoc.Fill(tbl_benhnhan);
            return tbl_benhnhan;
        }
        public void GetThuocReport(int madonthuoc, DataTable table)
        {
            ct_donthuoc = new SqlDataAdapter();
            ct_donthuoc.SelectCommand = new SqlCommand("SELECT  dt.SoLuong, dt.ThanhTien, dt.LieuDung, t.TenThuoc, t.GiaBan FROM ct_DonThuoc dt JOIN Thuoc t ON dt.MaThuoc = t.MaThuoc WHERE dt.MaDonThuoc = @MaDonThuoc", db.conn);
            ct_donthuoc.SelectCommand.Parameters.AddWithValue("@MaDonThuoc", madonthuoc);
            ct_donthuoc.Fill(table);
        }
        public int CreateCTDonThuoc(DTOCT_DonThuoc ct_donthuoc)
        {
            int result;
            string Command = "insert into ct_DonThuoc(MaDonThuoc,MaThuoc,SoLuong,ThanhTien,LieuDung)" +
                "values(@MaDonThuoc,@MaThuoc,@SoLuong,@ThanhTien,@LieuDung)";
            SqlCommand sql = new SqlCommand(Command, db.conn);
            sql.Parameters.AddWithValue("@MaDonThuoc", ct_donthuoc.maDonThuoc);
            sql.Parameters.AddWithValue("@MaThuoc", ct_donthuoc.maThuoc);
            sql.Parameters.AddWithValue("@SoLuong", ct_donthuoc.soLuong);
            sql.Parameters.AddWithValue("@ThanhTien", ct_donthuoc.thanhTien);
            sql.Parameters.AddWithValue("@LieuDung", ct_donthuoc.lieuDung);
            db.conn.Open();
            result = sql.ExecuteNonQuery();
            db.conn.Close();
            return result;
        }
    }
}