using CarRentingApp;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.Cars;
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
    /// Interaction logic for CustomerInfoManagement.xaml
    /// </summary>
    public partial class CustomerInfoManagement : Window
    {
        CustomerRepository repoCus = new CustomerRepository();
        public CustomerInfoManagement()
        {
            InitializeComponent();
            LoadCustomersList();
        }

        public void LoadCustomersList()
        {
            try
            {
                var customerList = repoCus.GetCustomers();
                lvCustomers.ItemsSource = customerList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of customers");
            }
        }

        private void BtnLoad_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadCustomersList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load customer list");
            }
        }

        private void BtnInsert_OnClick(object sender, RoutedEventArgs e)
        {
            CreateCustomerWindow options = new CreateCustomerWindow();
            options.Show();
            this.Close();
        }

        private void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            if (lvCustomers.SelectedItem != null)
            {
                Customer selectedCus = (Customer)lvCustomers.SelectedItem;
                UpdateCustomerWindow options = new UpdateCustomerWindow(selectedCus);
                options.Show();
            }
        }

        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (lvCustomers.SelectedItem != null)
            {
                Customer selectedCus = (Customer)lvCustomers.SelectedItem;
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    repoCus.DeleteCustomer(selectedCus);
                    LoadCustomersList();
                }
            }
        }

        private void BtnReturn_OnClick(object sender, RoutedEventArgs e)
        {
            MenuManagement options = new MenuManagement();
            options.Show();
            this.Close();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var search = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(search))
            {
                MessageBox.Show("Please enter a keyword.", "Search");
                return;
            }

            var listSearch = repoCus.GetCustomers()
                .Where(p => p.CustomerName != null && p.CustomerName.Trim().ToLower().Contains(search))
                .ToList();

            if (listSearch.Count == 0)
            {
                MessageBox.Show("No matching customers found.", "Search");
            }
            else
            {
                lvCustomers.ItemsSource = listSearch;
            }
        }
    }
}
