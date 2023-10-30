using CarRetingAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.Repository.Cars
{
    public interface ICarRepository
    {
        void CreateCar(CarInformation c);
        void DeleteCar(CarInformation c);
        void UpdateCar(CarInformation c);
        List<CarInformation> GetCars();
        List<Manufacturer> GetManufacturers();
        List<Supplier> GetSuppliers();
        Task<CarInformation> GetCarByID(int? id);
        IQueryable<CarInformation> GetIqCarInformations();

        IEnumerable<CarInformation>? GetCarInformationsBySearchValue(string searchValue);

    }
}
