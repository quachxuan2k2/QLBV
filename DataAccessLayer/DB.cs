using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
   public class DB
    {
        string connectionString = "Data Source=ADMIN\\XUANDEBUG;" + "Initial Catalog=QLBV ;" + "Integrated Security=True";
        public SqlConnection conn;
        public DB()
        {
            conn = new SqlConnection(connectionString);
        }
    }
}
