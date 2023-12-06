using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    public class DALDichVuCha
    {
        DB db = new DB();
        public DataTable GetDVCha()
        {
            SqlDataAdapter DVAdapter = new SqlDataAdapter();
            DVAdapter.SelectCommand = new SqlCommand("select * from DichVuCha", db.conn);
            DataTable tbl_dichvu = new DataTable();
            DVAdapter.Fill(tbl_dichvu);
            return tbl_dichvu;
        }
    }
}
