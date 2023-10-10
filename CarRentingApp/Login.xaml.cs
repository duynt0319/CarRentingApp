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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        ICustomerRepository customerRepository = new CustomerRepository();
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            FUCarRentingManagementContext context = new FUCarRentingManagementContext();
            var username = txtUserName.Text;
            var password = txtPassword.Password;
            var adminEmail = context.GetAdminEmail();
            var adminPassword = context.GetAdminPassword();

            var account = customerRepository.GetCustomers().FirstOrDefault(p =>
                p.Email.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                p.Password.Equals(password) &&
                p.CustomerStatus == 1);

            if (username.Equals(adminEmail) && password.Equals(adminPassword))
            {
                var chooseMenu = new MenuManagement();
                chooseMenu.Show();
                this.Close();
            }else if (!username.Equals(adminEmail) && !password.Equals(adminPassword))
            {
                this.Hide();
                var userPage = new UserPage(customerRepository.CheckCustomer(username, password));
                userPage.ShowDialog();
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Warning", MessageBoxButton.OK);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
