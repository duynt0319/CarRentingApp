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

namespace CarRentingApp
{
    
    /// <summary>
    /// Interaction logic for RentingManagement.xaml
    /// </summary>
    public partial class RentingManagement : Window
    {

        IRentingTransactionsRepository respTransaction = new RentingTransactionsRepository();

        ICustomerRepository respCustomer = new CustomerRepository();

        public RentingManagement()
        {
            InitializeComponent();
            GetValueCombobox();
        }

        private RentingTransaction GetRentingObject()
        {
            try
            {
                RentingTransaction transaction = new RentingTransaction
                {
                    RentingTransationId = string.IsNullOrEmpty(txtRentingTransactionId.Text) ? 0 : int.Parse(txtRentingTransactionId.Text),
                    RentingDate = txtRentingDate.SelectedDate,
                    TotalPrice = decimal.Parse(txtTotalPrice.Text),
                    CustomerId = txtCustomerId.SelectedValue != null ? (int.TryParse(txtCustomerId.SelectedValue.ToString(), out int customerId) ? customerId : 0) : 0,
                    RentingStatus = (byte?)(string.IsNullOrEmpty(txtRentingStatus.Text) ? 0 : int.Parse(txtRentingStatus.Text))
                };

                return transaction;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Getting Transaction Information");
                return null; 
            }
        }
        public void GetValueCombobox(string customerID = null)
        {
            var listCustomer = respCustomer.GetCustomers();
            txtCustomerId.ItemsSource = listCustomer;
            txtCustomerId.DisplayMemberPath = "CustomerName";
            txtCustomerId.SelectedValuePath = "CustomerId";
            if (customerID != null)
            {
                txtCustomerId.SelectedIndex = listCustomer.IndexOf(listCustomer.Where(p => p.CustomerId.Equals(int.Parse(customerID))).FirstOrDefault());
            }

        }

        public void LoadRentingList()
        {
            try
            {
                var transactionList = respTransaction.GetRentingTransactions();
                lvRentingTransaction.ItemsSource = transactionList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of Renting Transaction");
            }
        }

        private void BtnLoad_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadRentingList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Renting Transaction list");
            }
        }

        private void BtnInsert_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var transaction = GetRentingObject();
                if (transaction != null)
                {
                    respTransaction.CreateRentingTransaction(transaction);
                    MessageBox.Show("Renting transaction added successfully.", "Add Renting Transaction");
                }
                else
                {
                    MessageBox.Show("Invalid transaction data. Please check the input.", "Add Renting Transaction");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error Adding Renting Transaction");
            }
        }

        private void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var rentingTransaction = GetRentingObject();
                if (rentingTransaction != null)
                {
                    var oldObj = respTransaction.GetRentingTransactions().FirstOrDefault(p => p.RentingTransationId == rentingTransaction.RentingTransationId);
                    if (oldObj != null)
                    {
                        oldObj.RentingDate = rentingTransaction.RentingDate;
                        oldObj.TotalPrice = rentingTransaction.TotalPrice;
                        oldObj.CustomerId = rentingTransaction.CustomerId;
                        oldObj.RentingStatus = rentingTransaction.RentingStatus;

                        respTransaction.UpdateRentingTransaction(oldObj);
                        MessageBox.Show($"Renting transaction with ID {rentingTransaction.RentingTransationId} updated successfully.", "Edit Renting Transaction");
                    }
                    else
                    {
                        MessageBox.Show($"Renting transaction with ID {rentingTransaction.RentingTransationId} not found.", "Edit Renting Transaction");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid transaction data. Please check the input.", "Edit Renting Transaction");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error Updating Renting Transaction");
            }
        }

        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var rentingTransactionID = txtRentingTransactionId.Text;
                if (string.IsNullOrEmpty(rentingTransactionID))
                {
                    MessageBox.Show("Please choose rentingTransactionID to delete.");
                }
                else
                {
                    var confirmationResult = MessageBox.Show($"Are you sure you want to delete renting transaction with ID {rentingTransactionID}?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (confirmationResult == MessageBoxResult.Yes)
                    {
                        RentingTransaction rentingTransaction = GetRentingObject();
                        var oldObj = respTransaction.GetRentingTransactions().FirstOrDefault(p => p.RentingTransationId == rentingTransaction.RentingTransationId);
                        if (oldObj != null)
                        {
                            respTransaction.DeleteRentingTransaction(rentingTransaction);
                            MessageBox.Show($"Renting transaction with ID {rentingTransaction.RentingTransationId} deleted successfully.", "Delete Renting Transaction");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Delete Renting Transaction");
            }
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var chooseMenu = new MenuManagement();
            chooseMenu.ShowDialog();

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var search = txtSearch.Text.Trim().ToLower();
            var rentingTranscation = respTransaction.GetRentingTransactions();
            var customer = respCustomer.GetCustomers();

            var listSearch = from RentingTransaction in rentingTranscation
                             join Customer in customer on RentingTransaction.RentingTransationId equals Customer.CustomerId
                             where Customer.CustomerName.Trim().ToLower().Contains(search)
                             select RentingTransaction;

            if (listSearch.Any())
            {
                lvRentingTransaction.ItemsSource = listSearch.ToList();
            }
            else
            {
                MessageBox.Show("No matching renting transactions found.", "Search");
            }
        }
    }
}
