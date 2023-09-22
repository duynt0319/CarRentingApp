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
    /// Interaction logic for CustomerManagement.xaml
    /// </summary>
    public partial class CustomerManagement : Window
    {
        ICustomerRepository repoCus = new CustomerRepository();
        public CustomerManagement()
        {
            InitializeComponent();
        }

        public CustomerManagement(ICustomerRepository repository)
        {
            InitializeComponent();
            repoCus = repository;
        }

        private Customer GetCustomerObject()
        {
            Customer customer = null;
            try
            {
                return new Customer
                {
                    CustomerId = string.IsNullOrEmpty(txtCustomerId.Text) ? 0 : int.Parse(txtCustomerId.Text),
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

        private void BtnReturn_OnClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var chooseMenu = new MenuManagement();
            chooseMenu.ShowDialog();
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
            try
            {
                Customer customer = GetCustomerObject();

                if (string.IsNullOrEmpty(customer.CustomerName) || string.IsNullOrEmpty(customer.Telephone))
                {
                    MessageBox.Show("Please enter customer name and telephone number.", "Add Customer");
                    return;
                }

                repoCus.CreateCustomer(customer);

                LoadCustomersList();
                MessageBox.Show("Customer added successfully.", "Add Customer");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Customer");
            }
        }

        private void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customer = GetCustomerObject();
                var oldObj = repoCus.GetCustomers().Where(p => p.CustomerId.Equals(int.Parse(txtCustomerId.Text))).FirstOrDefault();
                if (oldObj != null)
                {
                    oldObj.CustomerName = txtCustomerName.Text;
                    oldObj.Telephone = txtCustomerPhone.Text;
                    oldObj.Email = txtCustomerEmail.Text;
                    oldObj.CustomerBirthday = txtCustomerBirthday.SelectedDate;
                    oldObj.CustomerStatus = byte.Parse(txtCustomerStatus.Text);
                    repoCus.UpdateCustomer(oldObj);
                    LoadCustomersList();
                    MessageBox.Show($"{customer.CustomerName} insert successfully", "Edit Customer");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Can not Update Customer");
            }
        }

        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var customerID = txtCustomerId.Text;
                if (string.IsNullOrEmpty(customerID))
                {
                    MessageBox.Show("Please choose a customer to delete", "Delete Customer");
                }
                else
                {
                    Customer customer = GetCustomerObject();
                    var oldObj = repoCus.GetCustomers().Where(p => p.CustomerId.Equals(int.Parse(txtCustomerId.Text))).FirstOrDefault();
                    if (oldObj != null)
                    {
                        MessageBoxResult result = MessageBox.Show($"Are you sure you want to delete {customer.CustomerName}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (result == MessageBoxResult.Yes)
                        {
                            repoCus.DeleteCustomer(customer);
                            LoadCustomersList();
                            MessageBox.Show($"{customer.CustomerName} deleted successfully", "Delete Customer");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Customer");
            }
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
