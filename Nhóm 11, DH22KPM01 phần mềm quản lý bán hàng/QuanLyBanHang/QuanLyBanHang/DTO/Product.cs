using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.DTO
{
    //tên biến = đầu thường cuối hoa  VD: maHang = biến, thuộc tính = MaHang
    public class Product
    {
        private int maHang;
        private string tenHang;
        private Decimal donGia;
        public Product(int maHang, string tenHang, decimal donGia) //hàm tạo
        {
            this.MaHang = maHang; //gán giá trị từ đối số của hàm tạo cho phương thức của lớp hiện tại.
            this.TenHang = tenHang;
            this.DonGia = donGia;
        }
        public Product(DataRow row) //data row lưu gtri dưới dạng object nên phải ép kiểu
        {   
            this.MaHang = (int)row["maHang"]; //Truy xuất giá trị từ cột MaHang trong DataRow và chuyển đổi nó thành chuỗi.
            this.TenHang = row["tenHang"].ToString();
            this.DonGia = (decimal)row["donGia"];
        }
        
        public Product(DataRow row, string a)
        {
            this.MaHang = (int)row["maHang"];
            this.TenHang = row["tenHang"].ToString();
            this.DonGia = (decimal)row["donGia"];
        }
        
        public Decimal DonGia
        {
            get { return donGia; }
            set { donGia = value; }
        }
        public string TenHang
        {
            get { return tenHang; }
            set { tenHang = value; }
        }
        public int MaHang
        {
            get{ return maHang; } 
            set { maHang = value; }
        }


    }
}
