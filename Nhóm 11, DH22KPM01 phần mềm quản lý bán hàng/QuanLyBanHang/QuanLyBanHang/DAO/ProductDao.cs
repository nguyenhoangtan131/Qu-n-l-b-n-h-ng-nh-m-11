using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang.DAO
{
    public class ProductDao
    {
        private static ProductDao instance;
        public static ProductDao Instance
        {
            get { if (instance == null) instance = new ProductDao(); return ProductDao.instance; }
            private set { ProductDao.instance = value; }
        }
        private ProductDao() { }
        public List<Product> SearchProductByName(string tenHang)
        {
            List<Product> list = new List<Product>();

            string query = string.Format("select * from HangHoa where TenHang like N'%{0}%'", tenHang);

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Product product = new Product(item);
                list.Add(product);
            }
            return list;
        }
        public List<Product> SearchByName(string TenHang)
        {
            List<Product> list = new List<Product>();
            string query = string.Format("SELECT * FROM HangHoa WHERE dbo.fuConvertToUnsign1(TenHang) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", TenHang);
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Product product = new Product(item," ");
                list.Add(product);
            }
            return list;

        }


        public List<Product> LoadProductList() 
        {
            List<Product> productList = new List<Product>();

            DataTable data = DataProvider.Instance.ExecuteQuery("USP_GetProductList");

            foreach (DataRow item in data.Rows) //lặp qua từng dòng datarow trong datatable 
            {
                Product product = new Product(item);
                productList.Add(product);
            }

            return productList;
        }     
        public List<Product> GetProductInfo(string maHang) 
        {
            List<Product> listProductInfo = new List<Product>();

            // Truy vấn dữ liệu với tham số SQL
            DataTable data = DataProvider.Instance.ExecuteQuery(
                "SELECT MaHang, TenHang, DonGia FROM HangHoa WHERE MaHang = @maHang",
                new object[] { maHang }
            );

            // Xử lý kết quả truy vấn
            foreach (DataRow item in data.Rows)
            {
                // Sử dụng constructor có 2 tham số
                Product product = new Product(item, maHang);
                listProductInfo.Add(product);
            }

            return listProductInfo;
        }

        public DataTable GetListProduct()
        {
            return DataProvider.Instance.ExecuteQuery("select * from HangHoa");
        }
        public bool InsertProduct( string tenHang, decimal donGia)
        {
            string query = string.Format("INSERT INTO HangHoa ( TenHang, DonGia )VALUES (N'{0}', '{1}')", tenHang, donGia);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateProduct( string tenHang, decimal donGia, int maHang)
        {
            string query = string.Format(
            "UPDATE HangHoa SET TenHang = N'{0}', DonGia = {1} WHERE MaHang = '{2}'",
            tenHang, donGia, maHang);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteProduct(int maHang)
        {
            string query = string.Format("Delete HangHoa where MaHang = '{0}'", maHang);
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            return result > 0;
        }
    }
}
