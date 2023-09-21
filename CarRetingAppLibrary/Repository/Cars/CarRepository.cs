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
        public void CreateCar(CarInformation c)
        {
           CarDao.CreateCar(c);
        }

        public void DeleteCar(CarInformation c)
        {
           CarDao.DeleteCar(c);
        }

        public List<CarInformation> GetCars()
        {
            return CarDao.GetCars();
        }

        public void UpdateCar(CarInformation c)
        {
           CarDao.UpdateCar(c);
        }
    }
}
