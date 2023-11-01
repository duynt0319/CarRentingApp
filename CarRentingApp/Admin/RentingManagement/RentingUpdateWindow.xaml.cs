using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.RentingTransactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CarRentingAppWPF.Admin.RentingManagement
{
    /// <summary>
    /// Interaction logic for RentingUpdateWindow.xaml
    /// </summary>
    public partial class RentingUpdateWindow : Window
    {
        private RentingTransactionsRepository rentingRepository;
        private RentingTransaction selectedRentingTransaction;
        public RentingUpdateWindow(RentingTransaction rentingTransaction)
        {
            InitializeComponent();
            rentingRepository = new RentingTransactionsRepository();
            GetValueCombobox();
            selectedRentingTransaction = rentingTransaction;

            // Display the renting transaction information to be updated
            txtRentingDate.SelectedDate = selectedRentingTransaction.RentingDate;
            txtTotalPrice.Text = selectedRentingTransaction.TotalPrice.ToString();
            txtRentingStatus.Text = selectedRentingTransaction.RentingStatus.ToString();
            cbxCustomer.SelectedValue = selectedRentingTransaction.CustomerId;
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy thông tin từ các trường nhập liệu
                DateTime rentingDate = txtRentingDate.SelectedDate ?? DateTime.Now;
                decimal totalPrice = decimal.Parse(txtTotalPrice.Text);
                int customerId = (int)cbxCustomer.SelectedValue;
                string rentingStatus = txtRentingStatus.Text;

                // Cập nhật thông tin của giao dịch thuê xe
                selectedRentingTransaction.RentingDate = rentingDate;
                selectedRentingTransaction.TotalPrice = totalPrice;
                selectedRentingTransaction.CustomerId = customerId;
                selectedRentingTransaction.RentingStatus = (byte)(string.IsNullOrEmpty(rentingStatus) ? 0 : int.Parse(rentingStatus));

                // Thực hiện cập nhật giao dịch thuê xe trong cơ sở dữ liệu
                rentingRepository.UpdateRentingTransaction(selectedRentingTransaction);
                MessageBox.Show("Giao dịch thuê đã được cập nhật thành công.", "Thông báo");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi");
            }
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        public void GetValueCombobox(string customerID = null)
        {
            var listCustomer = rentingRepository.GetCustomers();
            cbxCustomer.ItemsSource = listCustomer;
            cbxCustomer.DisplayMemberPath = "CustomerName";
            cbxCustomer.SelectedValuePath = "CustomerId";

            if (customerID != null)
            {
                cbxCustomer.SelectedValue = int.Parse(customerID);
            }

        }
    }
}
