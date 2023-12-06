using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class DTOChiTietDV
    {
        public int MaPhieu { get; set; }
        public string MaDV { get; set; }
        public string KetQuaDV { get; set; }
        public Byte[] AnhChup { get; set; }
    }
}
