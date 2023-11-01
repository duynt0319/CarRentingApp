
using CarRentingAppWPF.CustomerFlow;
using CarRentingAppWPF.CustomerFlow.CustomerManageProfile;
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
    public partial class Login : Window
    {
        CustomerRepository customerRepository;

        private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private Customer GetCustomerObject()
        {
            Customer customer = null;
            try
            {
                customer = new Customer
                {
                    Email = txtEmail.Text,
                    Password = txtPassword.Password
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get customer");
            }
            return customer;
        }

        private void ButtonLogin_OnClick(object sender, RoutedEventArgs e)
        {
            FUCarRentingManagementContext context = new FUCarRentingManagementContext();
            customerRepository = new CustomerRepository();
            var adminEmail = context.GetAdminEmail();
            var adminPassword = context.GetAdminPassword();
            Customer customer = GetCustomerObject();

            if (txtEmail.Text.Trim().Equals(adminEmail) && txtPassword.Password.Trim().Equals(adminPassword))
            {
                MenuManagement choose = new MenuManagement();
                choose.Show();
                this.Close();
            }
            else if (customerRepository.CheckCustomer(customer.Email, customer.Password) != null)
            {
                
                Customer loggedInCustomer = customerRepository.CheckCustomer(customer.Email, customer.Password);
                ManageProfile manageProfile = new ManageProfile(loggedInCustomer);
                manageProfile.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Username or password was wrong, please try again!");
            }
        }
    }
}
