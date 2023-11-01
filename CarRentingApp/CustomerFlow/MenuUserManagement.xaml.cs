using CarRentingAppWPF.CustomerFlow.CustomerManageProfile;
using CarRetingAppLibrary.DataAccess;
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

namespace CarRentingAppWPF.CustomerFlow
{
    /// <summary>
    /// Interaction logic for MenuUserManagement.xaml
    /// </summary>
    public partial class MenuUserManagement : Window
    {
        public MenuUserManagement()
        {
            InitializeComponent();
        }
        public MenuUserManagement(Customer LogginCus)
        {
            InitializeComponent();
        }

        private void btnManageProfile_Click(object sender, RoutedEventArgs e)
        {
            ManageProfile manageProfile = new ManageProfile();
            manageProfile.Show();
            this.Close();
        }

        private void btnViewHistory_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
