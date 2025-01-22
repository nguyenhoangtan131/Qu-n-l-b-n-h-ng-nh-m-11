using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSF.Data.Model;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.DAO
{
    public class BillDao
    {
        private static BillDao instance;

        public static BillDao Instance
        {
            get { if (instance == null) instance = new BillDao(); return BillDao.instance; }
            private set { BillDao.instance = value;}
        }

        private BillDao() { }

        public bool InsertBill()
        {

            string query = string.Format("INSERT INTO HoaDon(NgayLap)VALUES (GETDATE());");

            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        
        public string GetBillIDMax()
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT MAX(MaHD) As idMax From HoaDon");

            if (data.Rows.Count > 0)
            {
                DataRow row = data.Rows[0];
                return row["idMax"].ToString();
               
            }
            return "";
        }

        
        public string GetBillID(string mahoadon)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("SELECT * FROM HoaDon WHERE MaHD = " + mahoadon);

            if (data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.MaHoaDon;
            }
            return "";
        }

    }
}
