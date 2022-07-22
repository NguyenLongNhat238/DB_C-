using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace NguyenLongNhat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conn;
        private void ConnDatabase()
        {
            string connStr = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            conn = new SqlConnection(connStr);
        }


        private DataTable ListProducts()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da;
            string query = "Select * from Products";
            da = new SqlDataAdapter(query, conn);
            da.Fill(dt);
            return dt;
        }

        private DataTable ListCategories()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da;
            string query = "Select CategoryID, CategoryName from Categories";
            da = new SqlDataAdapter(query, conn);
            da.Fill(dt);
            return dt;
        }

        private DataTable ListSuppliers()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da;
            string query = "Select SupplierID, CompanyName from Suppliers";
            da = new SqlDataAdapter(query, conn);
            da.Fill(dt);
            return dt;
        }


        private bool AddProduct(Products p)
        {
            bool r = false;
            SqlCommand sqlCom;
            string query = String.Format("insert into Products(ProductName, CategoryID, SupplierID, UnitPrice, QuantityPerUnit) values (N'{0}',{1},{2},{3},{4})",
                                        p.ProductName, p.CategoryID, p.SupplierID, p.UnitPrice, p.Quantity);
            sqlCom = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                sqlCom.ExecuteNonQuery();
                r=true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
                r = false;
            }
            finally
            {
                conn.Close();
            }
            return r;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
            ConnDatabase();
            gvSanPham.DataSource = ListProducts();
            cbLoaiSP.DataSource = ListCategories();
            cbLoaiSP.DisplayMember = "CategoryName";
            cbLoaiSP.ValueMember = "CategoryID";
            cbNCC.DataSource = ListSuppliers();
            cbNCC.DisplayMember = "CompanyName";
            cbNCC.ValueMember = "SupplierID";
            
        }

        private void cbLoaiSP_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btThem_Click(object sender, EventArgs e)
        {
            Products p = new Products();
            p.ProductName = txtTenSP.Text;
            p.CategoryID = int.Parse(cbLoaiSP.SelectedValue.ToString());
            p.SupplierID = int.Parse(cbNCC.SelectedValue.ToString());
            p.UnitPrice = double.Parse(txtDonGia.Text);
            p.Quantity = int.Parse(txtSoLuong.Text);
            if (AddProduct(p))
            {
                MessageBox.Show(@"Thêm Thành Công !!!");
                gvSanPham.DataSource = ListProducts();
            }
            else
            {
                MessageBox.Show(@"Thêm Thất Bại !!!");
            }
        }
    }
}
