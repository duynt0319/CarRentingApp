using CarRentingApp;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.Cars;
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
    /// Interaction logic for CarManagement.xaml
    /// </summary>
    public partial class CarManagement : Window
    {
        CarRepository carRepository;

        public CarManagement()
        {
            InitializeComponent();
        }

        public CarManagement(CarRepository repository)
        {
            InitializeComponent();
            carRepository = repository;
        }

        public void LoadCarInformationList()
        {
            carRepository = new CarRepository();
            lvCars.ItemsSource = carRepository.GetCars();
        }

        private void BtnLoad_OnClick(object sender, RoutedEventArgs e)
        {
            LoadCarInformationList();
        }

        private void BtnInsert_OnClick(object sender, RoutedEventArgs e)
        {
            carRepository = new CarRepository();
            CarInsertWindow carInsertWindow = new CarInsertWindow();
            carInsertWindow.Show();
            this.Close();
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnBack_OnClick(object sender, RoutedEventArgs e)
        {
            MenuManagement options = new MenuManagement();
            options.Show();
            this.Close();
        }

        private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
        {
            var search = txtSearchValue.Text;
            var listSearch = carRepository.GetCars().Where(p => p.CarName.Contains(search)).ToList();
            lvCars.ItemsSource = listSearch;
        }

        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            
            if(lvCars.SelectedItem != null)
            {
                CarInformation selectedCar = (CarInformation)lvCars.SelectedItem; 
                carRepository.DeleteCar(selectedCar); 
                LoadCarInformationList();
            }
        }

        private void BtnUpdate_OnClick(object sender, RoutedEventArgs e)
        {
            if (lvCars.SelectedItem != null)
            {
                CarInformation selectedCar = (CarInformation)lvCars.SelectedItem; 
                CarUpdateWindow updateCarPage = new CarUpdateWindow(selectedCar);
                updateCarPage.Show();
            }
        }
    }
}
