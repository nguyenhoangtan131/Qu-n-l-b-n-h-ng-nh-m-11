using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.DTO
{
    public class Bill
    {
        public Bill(string maHoaDon, DateTime ngayLap)
        {
            this.NgayLap = ngayLap;
           
            this.MaHoaDon = maHoaDon;
        }
        public Bill(DataRow row)
        {
            this.NgayLap = (DateTime?)row["ngayLap"];
            
            this.MaHoaDon = row[maHoaDon].ToString();
        }

        private string maHoaDon;
        public string MaHoaDon 
        { 
            get { return maHoaDon; }
            set { maHoaDon = value; }
        }

        private DateTime? ngayLap;
        public DateTime? NgayLap
        {
            get { return ngayLap; }
            set { ngayLap = value; }
        }
        

    }
}
