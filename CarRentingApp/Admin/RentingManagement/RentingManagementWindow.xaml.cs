using CarRentingApp;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.Customers;
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
    /// Interaction logic for RentingManagementWindow.xaml
    /// </summary>
    public partial class RentingManagementWindow : Window
    {
        RentingTransactionsRepository rentingRepository = new RentingTransactionsRepository();
        CustomerRepository customerRepository= new CustomerRepository();
        public RentingManagementWindow()
        {
            InitializeComponent();
            LoadRentingTransactionList();
        }

        private void LoadRentingTransactionList()
        {
            lvRentingTransaction.ItemsSource = rentingRepository.GetRentingTransactions();
        }

        private void BtnLoad_OnClick(object sender, RoutedEventArgs e)
        {
            LoadRentingTransactionList();
        }

        private void BtnInsert_OnClick(object sender, RoutedEventArgs e)
        {
            RentingCreateWindow rentingInsertWindow = new RentingCreateWindow();
            rentingInsertWindow.Show();
        }

        private void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            if (lvRentingTransaction.SelectedItem != null)
            {
                RentingTransaction selectedRenting = (RentingTransaction)lvRentingTransaction.SelectedItem;
                RentingUpdateWindow rentingUpdateWindow = new RentingUpdateWindow(selectedRenting);
                rentingUpdateWindow.Show();
            }

        }

        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if (lvRentingTransaction.SelectedItem != null)
            {
                RentingTransaction selectedRenting = (RentingTransaction)lvRentingTransaction.SelectedItem;
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this renting transaction?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    rentingRepository.DeleteRentingTransaction(selectedRenting);
                    LoadRentingTransactionList();
                }
            }
        }

        private void BtnBack_OnClick(object sender, RoutedEventArgs e)
        {
            MenuManagement menuManagementWindow = new MenuManagement();
            menuManagementWindow.Show();
            this.Close();
        }

        private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
        {
            var rentingTranscation = rentingRepository.GetRentingTransactions();
            var customer = customerRepository.GetCustomers();
            string search = txtSearchValue.Text.Trim().ToLower();
            if (string.IsNullOrWhiteSpace(search))
            {
                MessageBox.Show("Please enter a keyword.", "Search");
                return;
            }

            var listSearch = from RentingTransaction in rentingTranscation
                             join Customer in customer on RentingTransaction.RentingTransationId equals Customer.CustomerId
                             where Customer.CustomerName.Contains(search, StringComparison.OrdinalIgnoreCase)
                             select RentingTransaction;

            if (listSearch == null)
            {
                MessageBox.Show("No matching renting transactions found.", "Search");
            }
            else
            {
                lvRentingTransaction.ItemsSource = listSearch;
            }
        }
    }
}
