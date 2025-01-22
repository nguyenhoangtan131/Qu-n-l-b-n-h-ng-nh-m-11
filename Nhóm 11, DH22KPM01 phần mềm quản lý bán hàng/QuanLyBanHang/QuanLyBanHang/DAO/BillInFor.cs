using QuanLyBanHang.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.DAO
{
    public class BillInFor
    {
        private static BillInFor instance;

        public static BillInFor Instance
        {
            get { if (instance == null) instance = new BillInFor(); return BillInFor.instance; }
            private set { BillInFor.instance = value; }
        }

        private BillInFor() { }
       
        public bool InsertBillInFor( int idHangHoa, int idBill, int SoLuong)
        {

            string query = string.Format("INSERT INTO ChiTietHoaDon ( idProduct, idBill,SoLuong )VALUES ({0}, {1},{2})", idHangHoa, idBill, SoLuong);
            
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
       


    }
}
