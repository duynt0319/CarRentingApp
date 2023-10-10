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

namespace CarRentingApp
{
    /// <summary>
    /// Interaction logic for UserProfile.xaml
    /// </summary>
    public partial class UserProfile : Window
    {
        ICustomerRepository customerRepository = new CustomerRepository();
        Customer customerBinding = new Customer();
        public UserProfile()
        {
            InitializeComponent();
        }

        public UserProfile(Customer cus)
        {
            InitializeComponent();
            customerBinding = cus;
        }

      
        public UserProfile(Customer cus, ICustomerRepository repository)
        {
            InitializeComponent();
            customerRepository = repository;
            customerBinding = cus;

            txtCustomerName.Text = customerBinding.CustomerName;
            txtCustomerEmail.Text = customerBinding.Email;
            txtCustomerPhone.Text = customerBinding.Telephone;
            txtpklCustomerBirthday.Text = customerBinding.CustomerBirthday.ToString();
            txtCustomerPassword.Text = customerBinding.Password;
        }



        private Customer GetCustomerObject()
        {
            Customer customer = null;
            try
            {
                customer = new Customer
                {
                    CustomerId = customerBinding.CustomerId,
                    CustomerName = txtCustomerName.Text,
                    CustomerBirthday = DateTime.Parse(txtpklCustomerBirthday.Text),
                    Email = txtCustomerEmail.Text,
                    Telephone = txtCustomerPhone.Text,
                    Password = txtCustomerPassword.Text
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update user");
            }
            return customer;
        }



        private void ButtonUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customer = GetCustomerObject();
                customerRepository.UpdateCustomer(customer);

                // Hiển thị thông báo khi cập nhật thành công
                MessageBox.Show("Update successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update user");
            }
        }

        private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var userPage = new UserPage();
            userPage.ShowDialog();
        }
    }
}
