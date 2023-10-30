using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.Cars;
using CarRetingAppLibrary.Repository.Manufacturers;
using CarRetingAppLibrary.Repository.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
    /// Interaction logic for CarUpdateWindow.xaml
    /// </summary>
    public partial class CarUpdateWindow : Window
    {
        private CarInformation selectedCar;
        private CarRepository carRepository = new CarRepository();
        private ManufacturersRepository respManu = new ManufacturersRepository();
        private SuppliersRepository respSuppli = new SuppliersRepository();
        public CarUpdateWindow(CarInformation carInformation)
        {
            InitializeComponent();
            selectedCar = carInformation;

            txtCarName.Text = carInformation.CarName;
            txtCarDescription.Text = carInformation.CarDescription;
            txtCarNumberOfDoors.Text = carInformation.NumberOfDoors.ToString();
            txtCarSeat.Text = carInformation.SeatingCapacity.ToString();
            txtCarFuelType.Text = carInformation.FuelType;
            txtCarYear.Text = carInformation.Year.ToString();
            txtCarStatus.Text = carInformation.CarStatus.ToString();
            txtCarRentingPricePerDay.Text = carInformation.CarRentingPricePerDay.ToString();

            GetValueManufacturerCombobox(selectedCar.ManufacturerId.ToString());
            GetValueSupplierCombobox(selectedCar.SupplierId.ToString());


        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                selectedCar.CarName = txtCarName.Text;
                selectedCar.CarDescription = txtCarDescription.Text;
                selectedCar.NumberOfDoors = int.Parse(txtCarNumberOfDoors.Text);
                selectedCar.SeatingCapacity = int.Parse(txtCarSeat.Text);
                selectedCar.FuelType = txtCarFuelType.Text;
                selectedCar.Year = int.Parse(txtCarYear.Text);
                selectedCar.ManufacturerId = (int)cbManufacture.SelectedValue;
                selectedCar.SupplierId = (int)cbSupplier.SelectedValue;
                selectedCar.CarStatus = byte.Parse(txtCarStatus.Text);
                selectedCar.CarRentingPricePerDay = decimal.Parse(txtCarRentingPricePerDay.Text);

                carRepository.UpdateCar(selectedCar);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get car Information");

            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void GetValueManufacturerCombobox(string manufacturerID = null)
        {
            // Lấy danh sách tất cả các nhà sản xuất
            var allManufacturers = respManu.GetManufacturer();

            // Gán danh sách tất cả nhà sản xuất vào ComboBox
            cbManufacture.ItemsSource = allManufacturers;
            cbManufacture.DisplayMemberPath = "ManufacturerName";
            cbManufacture.SelectedValuePath = "ManufacturerId";

            // Tìm nhà sản xuất của xe đã được chọn và đặt nó làm giá trị mặc định
            if (manufacturerID != null)
            {
                var selectedManufacturer = allManufacturers.FirstOrDefault(manufacturer => manufacturer.ManufacturerId == int.Parse(manufacturerID));
                if (selectedManufacturer != null)
                {
                    cbManufacture.SelectedItem = selectedManufacturer;
                }
            }
        }

        public void GetValueSupplierCombobox(string supplierID = null)
        {
            // Lấy danh sách tất cả các nhà cung cấp
            var allSuppliers = respSuppli.GetSupplier();

            // Gán danh sách tất cả nhà cung cấp vào ComboBox
            cbSupplier.ItemsSource = allSuppliers;
            cbSupplier.DisplayMemberPath = "SupplierName";
            cbSupplier.SelectedValuePath = "SupplierId";

            // Tìm nhà cung cấp của xe đã được chọn và đặt nó làm giá trị mặc định
            if (supplierID != null)
            {
                var selectedSupplier = allSuppliers.FirstOrDefault(supplier => supplier.SupplierId == int.Parse(supplierID));
                if (selectedSupplier != null)
                {
                    cbSupplier.SelectedItem = selectedSupplier;
                }
            }
        }

    }
}
