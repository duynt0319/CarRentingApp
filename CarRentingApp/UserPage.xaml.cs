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
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Window
    {
        ICustomerRepository customerRepository;
        Customer customer;
        public UserPage()
        {
            InitializeComponent();
        }

        public UserPage(Customer cus)
        {
            InitializeComponent();
            customer = cus;
        }
        public UserPage(Customer cus, ICustomerRepository repository)
        {
            InitializeComponent();
            customer = cus;
            customerRepository = repository;
        }

        private void btnManageProfile_Click(object sender, RoutedEventArgs e)
        {
            
            UserProfile userProfile = new UserProfile(customer, customerRepository);
            userProfile.Show();
            this.Close();
        }

        private void btnViewHistory_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var rentingHistory = new RentingHistory();
            rentingHistory.ShowDialog();

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
