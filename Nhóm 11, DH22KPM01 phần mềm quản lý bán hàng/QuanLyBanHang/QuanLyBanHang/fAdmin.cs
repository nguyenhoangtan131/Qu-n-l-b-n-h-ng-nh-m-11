using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyBanHang.DAO;
using QuanLyBanHang.DTO;

namespace QuanLyBanHang
{
    public partial class fAdmin : Form
    {
        BindingSource employeeList = new BindingSource();
        BindingSource accountList = new BindingSource();
        BindingSource productList = new BindingSource();
        public fAdmin()
        {
            InitializeComponent();
            
            LoadLoad();
        }
        void LoadLoad()
        {
            dtgvNhanVien.DataSource = employeeList;
            dtgvTaiKhoan.DataSource = accountList;
            dtgvKhoHang.DataSource = productList;
            LoadEmployeeList();
            LoadProduct();
            AddProductBinding();
            AddEmployeeBinding();
            AddAccountBinding();
            LoadAccount();
        }
        void AddProductBinding()
        {
            txtMaHH.DataBindings.Add(new Binding("Text", dtgvKhoHang.DataSource, "MaHang"));
            txtTenHH.DataBindings.Add(new Binding("Text", dtgvKhoHang.DataSource, "TenHang"));
            txtDonGia.DataBindings.Add(new Binding("Text", dtgvKhoHang.DataSource, "DonGia"));
        }

        void LoadProduct()
        {
            productList.DataSource = ProductDao.Instance.GetListProduct();
        }

        private void btnXemHH_Click(object sender, EventArgs e)
        {
            LoadProduct();
        }

        List<Employee> SearchEmployeeByName(string tenNV)
        {
            List<Employee> listEmployee = EmployeeDao.Instance.SearchEmployeeByName(tenNV);
            return listEmployee;
        }

        void AddAccountBinding()
        {
            txtTenTaiKhoan.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "TenDangNhap"));
            txtTenHienThi.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "TenHienThi"));
            txtMatKhau.DataBindings.Add(new Binding("Text", dtgvTaiKhoan.DataSource, "MatKhau"));
            numQuyenHan.DataBindings.Add(new Binding("Value", dtgvTaiKhoan.DataSource, "QuyenHan"));
        }

        void AddAccount(string tenDangNhap, string tenHienThi, string matKhau, int quyenHan)
        {
            if (AccountDao.Instance.InsertAccount(tenDangNhap, tenHienThi, matKhau, quyenHan))
            {
                MessageBox.Show("Thêm tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
            LoadAccount();
        }
        void EditAccount(string tenDangNhap, string tenHienThi, string matKhau, int quyenHan)
        {
            if (AccountDao.Instance.UpdateAccount(tenDangNhap, tenHienThi, matKhau, quyenHan))
            {
                MessageBox.Show("Sửa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Sửa thất bại");
            }
            LoadAccount();
        }
        void DeleteAccount(string tenDangNhap)
        {
            if (AccountDao.Instance.DeleteAccount(tenDangNhap))
            {
                MessageBox.Show("Xóa tài khoản thành công");
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
            LoadAccount();
        }

        private void btnXemTK_Click(object sender, EventArgs e)
        {
            
        }


        private void btnThemTK_Click(object sender, EventArgs e)
        {

            string tenDangNhap = txtTenTaiKhoan.Text;
            string tenHienThi = txtTenHienThi.Text;
            string matKhau = txtMatKhau.Text;
            int quyenHan = (int)numQuyenHan.Value;

            AddAccount(tenDangNhap, tenHienThi, matKhau, quyenHan);
        }


        private void btnXoaTK_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenTaiKhoan.Text;
            DeleteAccount(tenDangNhap);
        }

        private void btnSuaTK_Click(object sender, EventArgs e)
        {
            string tenDangNhap = txtTenTaiKhoan.Text;
            string tenHienThi = txtTenHienThi.Text;
            string matKhau = txtMatKhau.Text;
            int quyenHan = (int)numQuyenHan.Value;

            EditAccount(tenDangNhap, tenHienThi, matKhau, quyenHan);
        }


        private void btnTimNV_Click(object sender, EventArgs e)
        {
            employeeList.DataSource = SearchEmployeeByName(txtTimNV.Text);
        }

        void LoadAccount()
        {
            accountList.DataSource = AccountDao.Instance.GetListAccount();
        }

        void AddEmployeeBinding()
        {
            txtMaNV.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "MaNV"));
            txtTenNV.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "TenNV"));
            txtLuong.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "LuongCoBan"));
            txtSDTNV.DataBindings.Add(new Binding("Text", dtgvNhanVien.DataSource, "SoDienThoai"));
        }
        private void LoadEmployeeList()
        {
            employeeList.DataSource = EmployeeDao.Instance.LoadEmployeeList();
        }


        private void btnXemNV_Click(object sender, EventArgs e)
        {
            LoadEmployeeList();
        }
        private void btnThemNV_Click(object sender, EventArgs e)
        {
            
            string tenNhanVien = txtTenNV.Text;
            string sDTNV = txtSDTNV.Text;
            decimal luongCoBan;
            if (!decimal.TryParse(txtLuong.Text, out luongCoBan))
            {
                MessageBox.Show("Lương cơ bản phải là một số hợp lệ.");
                return;
            }
            if (EmployeeDao.Instance.InsertEmployee( luongCoBan, tenNhanVien, sDTNV))
            {
                MessageBox.Show("Thêm thành công!");
                LoadEmployeeList();
            }
            else 
            {
                MessageBox.Show("Lỗi thêm");
            }

        }
        private void btnSuaNV_Click(object sender, EventArgs e)
        {
            int maNhanVien = int.Parse(txtMaNV.Text);
            string tenNhanVien = txtTenNV.Text;
            string sDTNV = txtSDTNV.Text;
            
            decimal luongCoBan;
            if (!decimal.TryParse(txtLuong.Text, out luongCoBan))
            {
                MessageBox.Show("Lương cơ bản phải là một số hợp lệ.");
                return;
            }
            if (EmployeeDao.Instance.UpdateEmployee(maNhanVien, luongCoBan, tenNhanVien, sDTNV))
            {
                MessageBox.Show("Sửa thành công!");
                LoadEmployeeList();
            }
            else
            {
                MessageBox.Show("Lỗi sửa");
                
            }
             
        }

        private void btnXoaNV_Click(object sender, EventArgs e)
        {
            int maNhanVien = int.Parse(txtMaNV.Text);
            if (EmployeeDao.Instance.DeleteEmployee(maNhanVien))
            {
                MessageBox.Show("Xóa thành công!");
                LoadEmployeeList();
            }
            else
            {
                MessageBox.Show("Lỗi xóa");
                
            }
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
           LoadDoanhThuByDate(dtpkTuNgay.Value,dtpkToiNgay.Value);
        }
        void LoadDoanhThuByDate(DateTime statr,DateTime end)
        {
            string query=string.Format("SELECT b.MaHD As MaHD, b.NgayLap AS NgayLap, hh.TenHang AS TenHangHoa,bi.SoLuong AS SoLuong, (bi.SoLuong * hh.DonGia) AS TongTien FROM HoaDon b JOIN ChiTietHoaDon bi ON b.MaHD = bi.idBill JOIN HangHoa hh ON bi.idProduct = hh.MaHang WHERE b.NgayLap BETWEEN '{0}' AND '{1}'",statr,end);
            DataTable dt = DataProvider.Instance.ExecuteQuery(query);
            dtgvHoaDon.DataSource = dt;
        }

        private void btnTimTK_Click(object sender, EventArgs e)
        {
            accountList.DataSource = AccountDao.Instance.SearchAccountByName(txtTimTK.Text);
        }

        void AddProduct(string tenHang, decimal donGia)
        {
            if (ProductDao.Instance.InsertProduct(tenHang, donGia))
            {
                MessageBox.Show("Thêm  thành công");
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
            LoadProduct();
        }

        private void btnThemHH_Click(object sender, EventArgs e)
        {
            
            string tenHang = txtTenHH.Text;
            decimal donGia;

            if (!decimal.TryParse(txtDonGia.Text, out donGia))
            {
                MessageBox.Show("Lương cơ bản phải là một số hợp lệ.");
                return;
            }   
            AddProduct(tenHang, donGia);
        }

        void UpdateProduct( string tenHang, decimal donGia, int maHang)
        {
            if (ProductDao.Instance.UpdateProduct(tenHang, donGia, maHang))
            {
                MessageBox.Show("Sửa thành công");
            }
            else
            {
                MessageBox.Show("Sửa thất bại");
            }
            LoadProduct();
        }
        void DeleteProduct(int maHang)
        {
            if (ProductDao.Instance.DeleteProduct(maHang))
            {
                MessageBox.Show("Xóa thành công");
            }
            else
            {
                MessageBox.Show("Xóa thất bại");
            }
            LoadProduct();
        }

        private void btnXoaHH_Click(object sender, EventArgs e)
        {
            int maHang = int.Parse(txtMaHH.Text);
            DeleteProduct(maHang);
        }

        private void btnSuaHH_Click(object sender, EventArgs e)
        {
            string tenHang = txtTenHH.Text;
            decimal donGia;
            int maHang = int.Parse(txtMaHH.Text);

            if (!decimal.TryParse(txtDonGia.Text, out donGia))
            {
                MessageBox.Show("Lương cơ bản phải là một số hợp lệ.");
                return;
            }
            UpdateProduct(tenHang, donGia, maHang);
        }
        private void btnTimHH_Click(object sender, EventArgs e)
        {
            productList.DataSource = ProductDao.Instance.SearchProductByName(txtTimHH.Text);
        }
    }
}
