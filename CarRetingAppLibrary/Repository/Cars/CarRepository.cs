using CarRetingAppLibrary.BussinessObject;
using CarRetingAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.Repository.Cars
{
    public class CarRepository : ICarRepository
    {
        CarDao carDao = new CarDao();
        public void CreateCar(CarInformation c)
        {
            carDao.CreateCar(c);
        }

        public void DeleteCar(CarInformation c)
        {
            carDao.DeleteCar(c);
        }

        public async Task<CarInformation> GetCarByID(int? id)
        {
            CarInformation car = null;
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                car = carRentingManagementDB.CarInformations.FirstOrDefault(t => t.CarId == id);
                car.Manufacturer = carRentingManagementDB.Manufacturers.FirstOrDefault(m => m.ManufacturerId == car.ManufacturerId);
                car.Supplier = carRentingManagementDB.Suppliers.FirstOrDefault(m => m.SupplierId == car.SupplierId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return car;
        }

        public List<CarInformation> GetCars()
        {
            return carDao.GetCarInformations().ToList();
        }

        public List<Manufacturer> GetManufacturers()
        {
           return carDao.GetManufacturers();
        }

        public List<Supplier> GetSuppliers()
        {
            return carDao.GetSuppliers();
        }

        public void UpdateCar(CarInformation c)
        {
           carDao.UpdateCar(c);
        }

        public IQueryable<CarInformation> GetIqCarInformations()
        {
            return carDao.GetCarInformationsIq();
        }


    }
}
