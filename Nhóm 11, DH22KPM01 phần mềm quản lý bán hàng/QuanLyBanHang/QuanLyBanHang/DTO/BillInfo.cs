using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyBanHang.DTO
{
    internal class BillInfo
    {   
        public BillInfo(int soLuong, string maHD, string maHang, string maCTHD)
        {
            this.MaCTHD = maHD;
            this.MaHang = maHang;
            this.SoLuong = soLuong;
            this.MaCTHD = maCTHD;
        }

        private int soLuong;

        public int SoLuong
        {
            get { return soLuong; }
            set { soLuong = value; }
        }
        private string maHD;

        public string MaHD
        {
            get { return maHD; }
            set { maHD = value; }
        }
        private string maHang;

        public string MaHang
        {
            get { return maHang; }
            set { maHang = value; }
        }

        private string maCTHD;

        public string MaCTHD
        {
            get{ return maCTHD; }
            set{ maCTHD = value; }
        }
    }
}
