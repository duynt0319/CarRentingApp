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
    /// <summary>
    /// Interaction logic for CreateCustomerWindow.xaml
    /// </summary>
    public partial class CreateCustomerWindow : Window
    {
        CustomerRepository customerRepository = new CustomerRepository();
        public CreateCustomerWindow()
        {
            InitializeComponent();
        }
        private Customer GetCustomerObject()
        {
            Customer customer = null;
            try
            {
                return new Customer
                {
                    CustomerName = txtCustomerName.Text,
                    Telephone = txtCustomerPhone.Text,
                    Email = txtCustomerEmail.Text,
                    CustomerBirthday = txtCustomerBirthday.SelectedDate,
                    CustomerStatus = (byte?)(string.IsNullOrEmpty(txtCustomerStatus.Text) ? 0 : int.Parse(txtCustomerStatus.Text))
                };

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get customer");
            }
            return customer;
        }


        private void Button_Create(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customer = GetCustomerObject();

                if (string.IsNullOrEmpty(customer.CustomerName) || string.IsNullOrEmpty(customer.Telephone))
                {
                    MessageBox.Show("Please enter customer name and telephone number.", "Add Customer");
                    return;
                }

                customerRepository.CreateCustomer(customer);

                MessageBox.Show("Customer added successfully.", "Add Customer");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Customer");
            }
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            CustomerInfoManagement cus = new CustomerInfoManagement();
            cus.Show();
            this.Close();
        }
    }
}
