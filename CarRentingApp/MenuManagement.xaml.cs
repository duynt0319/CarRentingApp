using CarRentingAppWPF.Admin;
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
    /// Interaction logic for MenuManagement.xaml
    /// </summary>
    public partial class MenuManagement : Window
    {
        public MenuManagement()
        {
            InitializeComponent();
        }
  
        private void btnCustomerManagement_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var customerManagerment = new CustomerManagement();
            customerManagerment.ShowDialog();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnCarManagement_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var carManagerment = new CarManagement();
            carManagerment.ShowDialog();
        }

        private void btnRentingTransaction_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var rentingTransactionManagement = new RentingManagement();
            rentingTransactionManagement.ShowDialog();
        }
    }
}
