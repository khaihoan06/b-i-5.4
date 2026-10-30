
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace BT4
{
    public partial class Form1 : Form
    {
        private readonly string connectionString =
            @"Server=localhost;Database=QuanLyMonAn;Integrated Security=True;TrustServerCertificate=True;";

        private readonly Dictionary<string, decimal> _prices = new();

        private Button btnSave;

        public Form1()
        {
            InitializeComponent();

            // Tạo nút lưu hóa đơn
            btnSave = new Button();
            btnSave.Text = "Lưu hóa đơn";
            btnSave.Location = new System.Drawing.Point(237, 190);
            btnSave.Size = new System.Drawing.Size(100, 35);
            btnSave.Click += btnSave_Click;

            this.Controls.Add(btnSave);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadMenu();
            UpdateTotalLabel();
        }

        // Đọc danh sách món ăn từ SQL Server
        private void LoadMenu()
        {
            try
            {
                lstMenu.Items.Clear();
                _prices.Clear();

                using (SqlConnection conn =
                    new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sql = "SELECT TenMon, Gia FROM MonAn";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string name = reader["TenMon"].ToString();
                            decimal price = Convert.ToDecimal(reader["Gia"]);

                            _prices[name] = price;
                            lstMenu.Items.Add(name);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải món ăn: " + ex.Message);
            }
        }

        // Thêm món ăn
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (lstMenu.SelectedItem == null)
                return;

            string item = lstMenu.SelectedItem.ToString();

            lstSelected.Items.Add(item);
            lstMenu.Items.Remove(item);

            UpdateTotalLabel();
        }

        // Bỏ món ăn
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lstSelected.SelectedItem == null)
                return;

            string item = lstSelected.SelectedItem.ToString();

            lstMenu.Items.Add(item);
            lstSelected.Items.Remove(item);

            UpdateTotalLabel();
        }

        // Tính tổng tiền
        private decimal GetTotal()
        {
            decimal sum = 0;

            foreach (var obj in lstSelected.Items)
            {
                string name = obj.ToString();

                if (_prices.TryGetValue(name, out decimal price))
                    sum += price;
            }

            return sum;
        }

        private void UpdateTotalLabel()
        {
            lblTotal.Text = $"Tổng tiền: {GetTotal()}k";
        }

        // Lưu hóa đơn vào SQL Server
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lstSelected.Items.Count == 0)
            {
                MessageBox.Show("Bạn chưa chọn món ăn!");
                return;
            }

            using (SqlConnection conn =
                new SqlConnection(connectionString))
            {
                conn.Open();

                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    decimal total = GetTotal();

                    string sqlHoaDon =
                        @"INSERT INTO HoaDon (TongTien)
                          VALUES (@TongTien);
                          SELECT CAST(SCOPE_IDENTITY() AS INT);";

                    int maHoaDon;

                    using (SqlCommand cmd =
                        new SqlCommand(sqlHoaDon, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@TongTien", total);

                        maHoaDon = (int)cmd.ExecuteScalar();
                    }

                    string sqlChiTiet =
                        @"INSERT INTO ChiTietHoaDon
                          (MaHoaDon, TenMon, Gia)
                          VALUES (@MaHoaDon, @TenMon, @Gia)";

                    foreach (var obj in lstSelected.Items)
                    {
                        string name = obj.ToString();
                        decimal price = _prices[name];

                        using (SqlCommand cmd =
                            new SqlCommand(sqlChiTiet, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                            cmd.Parameters.AddWithValue("@TenMon", name);
                            cmd.Parameters.AddWithValue("@Gia", price);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();

                    MessageBox.Show(
                        "Lưu hóa đơn thành công!\nMã hóa đơn: " + maHoaDon);

                    lstSelected.Items.Clear();
                    LoadMenu();
                    UpdateTotalLabel();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Lỗi lưu hóa đơn: " + ex.Message);
                }
            }
        }
    }
}
