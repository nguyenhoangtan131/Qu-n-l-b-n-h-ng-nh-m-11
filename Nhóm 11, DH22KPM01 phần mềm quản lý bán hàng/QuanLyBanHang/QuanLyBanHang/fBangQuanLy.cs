using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Forms;
using GSF;
using QuanLyBanHang.DAO;
using QuanLyBanHang.DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace QuanLyBanHang
{
    public partial class fBangQuanLy : Form
    {
    //    BindingSource productList = new BindingSource();
        public fBangQuanLy()
        {
            InitializeComponent();
            LoadLoad();
            AddColumsForLisView();
        }

        public static int ProductWidth = 130;
        public static int ProductHeight = 130;

        private void fBangQuanLy_Load(object sender, EventArgs e)
        {

        }

        void LoadLoad()
        {
            DoiMauChu();
            LoadProduct();
        }


        #region Method

        private void DoiMauChu()
        {
            foreach (ToolStripMenuItem item in menuQL.Items) //// Đăng ký sự kiện MouseEnter và MouseLeave cho từng item trong MenuStrip
            {
                item.MouseEnter += (s, ev) =>                 // Khi di chuột qua một item
                {
                    item.ForeColor = Color.Black;     // Thay đổi màu chữ khi hover
                };
                item.MouseLeave += (s, ev) =>                // Khi chuột rời khỏi một item
                {
                    item.ForeColor = Color.White;        // Trả về màu chữ ban đầu
                };
            }
        }
        void LoadProductByeName(string name)
        {
            List<Product> productList = ProductDao.Instance.SearchByName(name);

            foreach (Product item in productList)
            {
                Button btn = new Button() { Width = ProductWidth, Height = ProductHeight };
                btn.Text = item.TenHang + Environment.NewLine + item.DonGia;
                btn.Click += btn_Click;
                btn.Tag = item; //tag là object lưu bill vào tag 

                //đặt lại thuộc tính của button
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.Font = new Font("Arial", 12, FontStyle.Bold);
                //thêm button vào flow
                flpProduct.Controls.Add(btn);
            }
        }
        void LoadProduct()
        {
            List<Product> productList = ProductDao.Instance.LoadProductList();
            
            foreach (Product item in productList)
            {
                Button btn = new Button() {Width = ProductWidth, Height = ProductHeight};                         
                btn.Text = item.TenHang + Environment.NewLine + item.DonGia;
                btn.Click += btn_Click;
                btn.Tag = item; //tag là object lưu bill vào tag 

                //đặt lại thuộc tính của button
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.Font = new Font("Arial", 12, FontStyle.Bold);
                //thêm button vào flow
                flpProduct.Controls.Add(btn);
            }
        }
        void AddColumsForLisView()
        {
            lsvHoaDon.View = View.Details; // Chế độ hiển thị chi tiết
            lsvHoaDon.Columns.Add("Mã hàng", 100);    // Cột Mã hàng
            lsvHoaDon.Columns.Add("Tên hàng", 200);   // Cột Tên hàng
            lsvHoaDon.Columns.Add("Đơn giá", 100);
            lsvHoaDon.Columns.Add("Số Lượng", 100); // Cột Đơn giá

        }
        
        void ShowProduct(int mahang)
        {
            // Lấy thông tin sản phẩm từ cơ sở dữ liệu
            List<Product> listProductInfo = ProductDao.Instance.GetProductInfo(mahang.ToString());

            Decimal TongTien = decimal.Parse(txtTongTien.Text);
            
            // Thêm dữ liệu vào ListView
            foreach (Product product in listProductInfo)
            {
                
                // Tạo một dòng dữ liệu mới cho ListView
               

                    ListViewItem item = new ListViewItem(product.MaHang.ToString());  // Giá trị cột đầu tiên (Mã hàng)
                item.SubItems.Add(product.TenHang);                   // Giá trị cột thứ hai (Tên hàng)
                item.SubItems.Add(product.DonGia.ToString());
                item.SubItems.Add("1");
                // Giá trị cột thứ ba (Đơn giá)
                Decimal Giaban = Convert.ToDecimal(product.DonGia);
                TongTien += Giaban;
                
                // Thêm dòng dữ liệu vào ListView
                lsvHoaDon.Items.Add(item);
            }
            


            // Hiển thị tổng tiền trong TextBox với định dạng Việt Nam
            txtTongTien.Text = TongTien.ToString();
           
            if (lsvHoaDon.Items.Count > 1)
            {
                for (int i = 0; i < lsvHoaDon.Items.Count-1; i++)
                {
                    
                    for (int j = i + 1; j < lsvHoaDon.Items.Count; j++)
                    {

                        if (lsvHoaDon.Items[i].SubItems[0].Text == lsvHoaDon.Items[j].SubItems[0].Text)
                        {
                            int sl;
                            sl = int.Parse(lsvHoaDon.Items[i].SubItems[3].Text);
                            sl += 1;
                            lsvHoaDon.Items[i].SubItems[3].Text=sl.ToString();
;                           lsvHoaDon.Items[j].Remove();


                        }

                    }


                }
            }


        }


        #endregion

        #region Events
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            
            if (BillDao.Instance.InsertBill())
            {
                    foreach (ListViewItem item in lsvHoaDon.Items) //duyệt qua từng dòng listview xong lưu vào chitiethoadon
                    {
                       
                        BillInFor.Instance.InsertBillInFor(int.Parse(item.SubItems[0].Text), int.Parse(BillDao.Instance.GetBillIDMax()), int.Parse(item.SubItems[3].Text)); //lưu vào chi tiết hóa đơn insertbillinfor()
                      
                    }
                DialogResult result = MessageBox.Show("Thanh Toán Thành Công ,Bạn Có Muốn Xuất Hóa Đơn Không", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(result == DialogResult.Yes)
                {
                     SaveFileDialog saveFileDialog = new SaveFileDialog
                          {
                             Title = "Chọn nơi lưu file",
                             Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*", // Bộ lọc định dạng file
                             DefaultExt = "txt", // Mặc định là file .txt
                              FileName = "listview_data.txt" // Tên file mặc định
                          };

    // Hiển thị hộp thoại chọn đường dẫn
    if (saveFileDialog.ShowDialog() == DialogResult.OK)
    {
        string filePath = saveFileDialog.FileName;

        try
        {
            // Mở file để ghi
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                // Ghi tiêu đề cột (header)
                foreach (ColumnHeader column in lsvHoaDon.Columns)
                {
                    writer.Write(column.Text + "\t"); // Ngăn cách các cột bằng tab
                }
                writer.WriteLine(); // Xuống dòng sau khi ghi xong tiêu đề

                // Ghi từng dòng (item)
                foreach (ListViewItem item in lsvHoaDon.Items)
                {
                    foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
                    {
                        writer.Write(subItem.Text + "\t"); // Ghi giá trị từng cột
                    }
                                    writer.WriteLine(); // Xuống dòng sau mỗi hàng
                }
                                writer.Write("-Thông Tin Khách Hàng:"); // Ghi giá trị từng cột
                                writer.WriteLine();
                                writer.Write(txtTenKH.Text+"-"+txtSDT.Text+"-"+txtDiaChi.Text);

                            }

            // Hiển thị thông báo thành công
            MessageBox.Show("Xuất dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            // Hiển thị lỗi nếu có
            MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
                }
                //BillInFor.Instance.InsertBillInFor(item.Text)
            }
            else
            {
                MessageBox.Show("Có Lỗi");
            }
           

        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in lsvHoaDon.Items)
            {
                item.Remove();
                txtTongTien.Text = "0";
            }
        }

        private void btn_Click(object sender, EventArgs e)
        {
            int maHang = ((sender as Button).Tag as Product).MaHang;
            ShowProduct(maHang);
        } 


        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void fBangQuanLy_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn đăng xuất?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
        private void adminToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.ShowDialog();
        }

        #endregion

        private void btnTim_Click(object sender, EventArgs e)
        {
            flpProduct.Controls.Clear();
            LoadProductByeName(txtTenHang.Text);     
        }
    }
}
