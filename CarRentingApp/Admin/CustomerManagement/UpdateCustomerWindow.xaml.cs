using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.Customers;
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

namespace CarRentingAppWPF.Admin.CustomerManagement
{

    public partial class UpdateCustomerWindow : Window
    {
        private Customer selectedCus = new Customer();
        CustomerRepository customerRepository = new CustomerRepository();
        public UpdateCustomerWindow(Customer customer)
        {
            InitializeComponent();
            selectedCus = customer;

            txtCustomerName.Text = customer.CustomerName;
            txtCustomerEmail.Text = customer.Email;
            txtCustomerPhone.Text = customer.Telephone;
            txtCustomerStatus.Text = customer.CustomerStatus.ToString();
            txtCustomerBirthday.SelectedDate = customer.CustomerBirthday;
            txtCustomerPass.Text = customer.Password;
           
        }

        private void btn_update(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy thông tin cập nhật từ các trường nhập liệu
                string updatedCustomerName = txtCustomerName.Text;
                string updatedCustomerEmail = txtCustomerEmail.Text;
                string updatedCustomerPhone = txtCustomerPhone.Text;
                byte updatedCustomerStatus = byte.Parse(txtCustomerStatus.Text);
                string updatedCustomerPass = txtCustomerPass.Text;

                DateTime? updatedCustomerBirthday = txtCustomerBirthday.SelectedDate;

                // Cập nhật thông tin khách hàng đã chọn
                selectedCus.CustomerName = updatedCustomerName;
                selectedCus.Email = updatedCustomerEmail;
                selectedCus.Telephone = updatedCustomerPhone;
                selectedCus.CustomerStatus = updatedCustomerStatus;
                selectedCus.Password = updatedCustomerPass;

                if (updatedCustomerBirthday != null)
                {
                    selectedCus.CustomerBirthday = updatedCustomerBirthday.Value;
                }
                customerRepository.UpdateCustomer(selectedCus);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi cập nhật");
            }
        }

        private void btn_close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
