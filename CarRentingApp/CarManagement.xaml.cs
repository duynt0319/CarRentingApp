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

namespace CarRentingApp
{
    /// <summary>
    /// Interaction logic for CarManagement.xaml
    /// </summary>
    public partial class CarManagement : Window
    {

        ICarRepository carRepository = new CarRepository();
        IManufacturersRepository manufacturersRepository = new ManufacturersRepository();
        ISuppliersRepository suppliersRepository = new SuppliersRepository();

        public CarManagement()
        {
            InitializeComponent();
            GetValueManufacturerCombobox();
            GetValueSupplierCombobox();
        }

        public CarManagement(ICarRepository repository)
        {
            InitializeComponent();
            carRepository = repository;
        }

        private CarInformation GetCarObject()
        {
            CarInformation car = null;
            try
            {
                return new CarInformation
                {
                    CarId = string.IsNullOrEmpty(txtCarId.Text) ? 0 : int.Parse(txtCarId.Text),
                    CarName = txtCarName.Text,
                    CarDescription = txtCarDescription.Text,
                    NumberOfDoors = int.Parse(txtCarNumberOfDoors.Text),
                    SeatingCapacity = int.Parse(txtCarSeat.Text),
                    FuelType = txtCarFuelType.Text,
                    Year = int.Parse(txtCarYear.Text),

                    ManufacturerId = txtCarManufacture.SelectedValue != null ? (int.TryParse(txtCarManufacture.SelectedValue.ToString(), out int manufactureId) ? manufactureId : 0) : 0,
                    SupplierId = txtCarSupplier.SelectedValue != null ? (int.TryParse(txtCarSupplier.SelectedValue.ToString(), out int supplierId) ? supplierId : 0) : 0,

                    CarStatus = (byte?)(string.IsNullOrEmpty(txtCarStatus.Text) ? 0 : int.Parse(txtCarStatus.Text)),
                    CarRentingPricePerDay = decimal.Parse(txtCarRentingPricePerDay.Text)
                };

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get car Information");
            }
            return car;
        }

        public void GetValueManufacturerCombobox(string manufactuereID = null)
        {
            var listManufacturer = manufacturersRepository.GetManufacturer();
            txtCarManufacture.ItemsSource = listManufacturer;
            txtCarManufacture.DisplayMemberPath = "ManufacturerName";
            txtCarManufacture.SelectedValuePath = "ManufacturerId";
            if (manufactuereID != null)
            {
                txtCarManufacture.SelectedIndex = listManufacturer.IndexOf(listManufacturer.Where(p => p.ManufacturerId.Equals(int.Parse(manufactuereID))).FirstOrDefault());
            }

        }

        public void GetValueSupplierCombobox(string supplierID = null)
        {
            var listSupplier = suppliersRepository.GetSupplier();
            txtCarSupplier.ItemsSource = listSupplier;
            txtCarSupplier.DisplayMemberPath = "SupplierName";
            txtCarSupplier.SelectedValuePath = "SupplierId";
            if (supplierID != null)
            {
                txtCarSupplier.SelectedIndex = listSupplier.IndexOf(listSupplier.Where(p => p.SupplierId.Equals(int.Parse(supplierID))).FirstOrDefault());
            }
        }

        public void LoadCarList()
        {
            try
            {
                var carList = carRepository.GetCars();
                lvCars.ItemsSource = carList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of Cars");
            }
        }

        private void BtnLoad_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadCarList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load Car information list");
            }
        }

        private void BtnInsert_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var car = GetCarObject();
                if (car != null)
                {
                    car.CarId = 0;
                    carRepository.CreateCar(car);
                    LoadCarList();
                    MessageBox.Show("Successfully", "Add Car information");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Do not insert Car information");
            }
        }

        private void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                CarInformation car = GetCarObject();
                var oldObj = carRepository.GetCars().Where(p => p.CarId.Equals(int.Parse(txtCarId.Text))).FirstOrDefault();
                if (oldObj != null)
                {
                    oldObj.CarName = txtCarName.Text;
                    oldObj.CarDescription = txtCarDescription.Text;
                    oldObj.NumberOfDoors = int.Parse(txtCarNumberOfDoors.Text);
                    oldObj.SeatingCapacity = int.Parse(txtCarSeat.Text);
                    oldObj.FuelType = txtCarFuelType.Text;
                    oldObj.Year = int.Parse(txtCarYear.Text);


                    oldObj.ManufacturerId = txtCarManufacture.SelectedValue != null ? (int.TryParse(txtCarManufacture.SelectedValue.ToString(), out int manufactureId) ? manufactureId : 0) : 0;
                    oldObj.SupplierId = txtCarSupplier.SelectedValue != null ? (int.TryParse(txtCarSupplier.SelectedValue.ToString(), out int supplierId) ? supplierId : 0) : 0;

                    oldObj.CarStatus = (byte?)(string.IsNullOrEmpty(txtCarStatus.Text) ? 0 : int.Parse(txtCarStatus.Text));
                    oldObj.CarRentingPricePerDay = decimal.Parse(txtCarRentingPricePerDay.Text);


                    carRepository.UpdateCar(oldObj);
                    LoadCarList();
                    MessageBox.Show($"{car.CarName} insert successfully", "Edit CarInformation");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Do not insert CarInformation");
            }
        }

        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                var carID = txtCarId.Text;
                if (string.IsNullOrEmpty(carID))
                {
                    MessageBox.Show("Please choose a car to delete");
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this car?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        CarInformation car = GetCarObject();
                        var oldObj = carRepository.GetCars().Where(p => p.CarId.Equals(int.Parse(txtCarId.Text))).FirstOrDefault();
                        if (oldObj != null)
                        {
                            carRepository.DeleteCar(car);
                            LoadCarList();
                            MessageBox.Show($"{car.CarName} deleted successfully", "Delete CarInformation");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete CarInformation");
            }
        }

        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var chooseMenu = new MenuManagement();
            chooseMenu.ShowDialog();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            var search = txtSearch.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(search))
            {
                MessageBox.Show("Please enter a keyword .", "Search");
                return;
            }

            var listSearch = carRepository.GetCars()
                .Where(p => !string.IsNullOrEmpty(p.CarName) && p.CarName.Trim().ToLower().Contains(search))
                .ToList();

            if (listSearch.Count == 0)
            {
                MessageBox.Show("No matching cars found.", "Search");
            }
            else
            {
                lvCars.ItemsSource = listSearch;
            }
        }

        private void lvCars_SelectionChanged(object sender, RoutedEventArgs e)
        {
            CarInformation selectedCar = (CarInformation)lvCars.SelectedItem;

            
            txtCarId.Text = selectedCar.CarId.ToString();
            txtCarName.Text = selectedCar.CarName;
            txtCarDescription.Text = selectedCar.CarDescription;
            txtCarNumberOfDoors.Text = selectedCar.NumberOfDoors.ToString();
            txtCarSeat.Text = selectedCar.SeatingCapacity.ToString();
            txtCarFuelType.Text = selectedCar.FuelType;
            txtCarYear.Text = selectedCar.Year.ToString();

            
            txtCarManufacture.SelectedValue = selectedCar.ManufacturerId;
            txtCarSupplier.SelectedValue = selectedCar.SupplierId;

            txtCarStatus.Text = selectedCar.CarStatus.ToString();
            txtCarRentingPricePerDay.Text = selectedCar.CarRentingPricePerDay.ToString();
        }

    }
}
