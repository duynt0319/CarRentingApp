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
    /// Interaction logic for RentingCreateWindow.xaml
    /// </summary>
    public partial class RentingCreateWindow : Window
    {
        private RentingTransactionsRepository rentingRepository;
        private CustomerRepository customerRepository = new CustomerRepository();

        public RentingCreateWindow()
        {
            InitializeComponent();
            GetValueCombobox();
            rentingRepository = new RentingTransactionsRepository();
           
        }

        private RentingTransaction GetRentingObject()
        {
            RentingTransaction car = null;
            try
            {
                return new RentingTransaction
                {
                    RentingDate = txtRentingDate.SelectedDate ?? DateTime.Now,
                    TotalPrice = decimal.Parse(txtTotalPrice.Text),
                    CustomerId = cbxCustomer.SelectedValue != null ? (int.TryParse(cbxCustomer.SelectedValue.ToString(), out int customerId) ? customerId : 0) : 0,
                    RentingStatus = (byte)(string.IsNullOrEmpty(txtRentingStatus.Text) ? 0 : int.Parse(txtRentingStatus.Text))
                };

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get car Information");
            }
            return car;
        }

        private void Button_Create(object sender, RoutedEventArgs e)
        {
            try
            {
                var transaction = GetRentingObject();
                if (transaction != null)
                {
                    rentingRepository.CreateRentingTransaction(transaction);
                    MessageBox.Show("Successfully", "Add Renting Transaction");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Do not insert Renting Transaction ");
            }
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void GetValueCombobox(string customerID = null)
        {
            var listCustomer = customerRepository.GetCustomers();
            cbxCustomer.ItemsSource = listCustomer;
            cbxCustomer.DisplayMemberPath = "CustomerName";
            cbxCustomer.SelectedValuePath = "CustomerId";
            if (customerID != null)
            {
                cbxCustomer.SelectedIndex = listCustomer.IndexOf(listCustomer.Where(p => p.CustomerId.Equals(int.Parse(customerID))).FirstOrDefault());
            }

        }
    }
}
