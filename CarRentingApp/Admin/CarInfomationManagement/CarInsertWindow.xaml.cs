using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.Cars;
using CarRetingAppLibrary.Repository.Manufacturers;
using CarRetingAppLibrary.Repository.Suppliers;
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

namespace CarRentingAppWPF.Admin
{
    /// <summary>
    /// Interaction logic for CarInsertWindow.xaml
    /// </summary>
    public partial class CarInsertWindow : Window
    {
        CarRepository carRepository = new CarRepository();
        ManufacturersRepository respManu = new ManufacturersRepository();
        SuppliersRepository respSuppli = new SuppliersRepository();
        public CarInsertWindow()
        {
            InitializeComponent();
            GetValueManufacturerCombobox();
            GetValueSupplierCombobox();
        }



        private CarInformation GetCar()
        {
            CarInformation car = new CarInformation();
            try
            {
                return new CarInformation
                {
                    CarName = txtCarName.Text,
                    CarDescription = txtCarDescription.Text,
                    NumberOfDoors = int.Parse(txtCarNumberOfDoors.Text),
                    SeatingCapacity = int.Parse(txtCarSeat.Text),
                    FuelType = txtCarFuelType.Text,
                    Year = int.Parse(txtCarYear.Text),

                    ManufacturerId = cbManufacture.SelectedValue != null ? (int.TryParse(cbManufacture.SelectedValue.ToString(), out int manufactureId) ? manufactureId : 0) : 0,
                    SupplierId = cbSupplier.Text != null ? (int.TryParse(cbSupplier.SelectedValue.ToString(), out int supplierId) ? supplierId : 0) : 0,

                    CarStatus = byte.Parse(txtCarStatus.Text),
                    CarRentingPricePerDay = decimal.Parse(txtCarRentingPricePerDay.Text)
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get car Information");
            }
            return car;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var car = GetCar();
                if (car != null)
                {
                    car.CarId = 0;
                    carRepository.CreateCar(car);
                    MessageBox.Show("Successfully", "Add Car information");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Do not insert Car information");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CarManagement carManagement = new CarManagement();
            carManagement.Show();
            this.Close();
        }

        public void GetValueManufacturerCombobox(string manufactuereID = null)
        {
            var listManufacturer = respManu.GetManufacturer();
            cbManufacture.ItemsSource = listManufacturer;
            cbManufacture.DisplayMemberPath = "ManufacturerName";
            cbManufacture.SelectedValuePath = "ManufacturerId";
            if (manufactuereID != null)
            {
                cbManufacture.SelectedIndex = listManufacturer.IndexOf(listManufacturer.Where(p => p.ManufacturerId.Equals(int.Parse(manufactuereID))).FirstOrDefault());
            }

        }

        public void GetValueSupplierCombobox(string supplierID = null)
        {
            var listSupplier = respSuppli.GetSupplier();
            cbSupplier.ItemsSource = listSupplier;
            cbSupplier.DisplayMemberPath = "SupplierName";
            cbSupplier.SelectedValuePath = "SupplierId";
            if (supplierID != null)
            {
                cbSupplier.SelectedIndex = listSupplier.IndexOf(listSupplier.Where(p => p.SupplierId.Equals(int.Parse(supplierID))).FirstOrDefault());
            }

        }
    }
}
