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

namespace CarRentingAppWPF.CustomerFlow.CustomerManageProfile
{
    /// <summary>
    /// Interaction logic for ManageProfile.xaml
    /// </summary>
    public partial class ManageProfile : Window
    {
        private CustomerRepository customerRepository;
        Customer updateCus;
        public ManageProfile()
        {
            InitializeComponent();
        }

        public ManageProfile(Customer loggedInCustomer)
        {
            InitializeComponent();
            updateCus =loggedInCustomer;

            txtCustomerName.Text = loggedInCustomer.CustomerName;
            txtCustomerPhone.Text = loggedInCustomer.Telephone;
            txtCustomerEmail.Text = loggedInCustomer.Email;
            txtCustomerPassword.Text = loggedInCustomer.Password;
            txtpklCustomerBirthday.SelectedDate = loggedInCustomer.CustomerBirthday;
        }

        private void ButtonUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                customerRepository = new CustomerRepository();
                    
                updateCus.CustomerName = txtCustomerName.Text;
                updateCus.Email = txtCustomerEmail.Text;
                updateCus.Telephone = txtCustomerPhone.Text;
                updateCus.Password = txtCustomerPassword.Text;
                updateCus.CustomerBirthday = txtpklCustomerBirthday.SelectedDate;

                customerRepository.UpdateCustomer(updateCus);
                MessageBox.Show("Cập nhật thông tin thành công.", "Thành công");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi cập nhật");
            }
        }

        private void ButtonRen_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonClose_OnClick(object sender, RoutedEventArgs e)
        {
            
            this.Close();
        }
    }
}
