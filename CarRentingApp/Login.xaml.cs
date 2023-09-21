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
            var username = txtUserName.Text;
            var password = txtPassword.Password;

            var account = customerRepository.GetCustomers().FirstOrDefault(p =>
                p.Email.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                p.Password.Equals(password) &&
                p.CustomerStatus == 1);

            if (account != null)
            {
                this.Hide();
                var chooseMenu = new MenuManagement();
                chooseMenu.ShowDialog();
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
