using CarRetingSystemLibrary.BussinessObject;
using CarRetingSystemLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingSystemLibrary.Repository.Cars
{
    public class CarRepository : ICarRepository
    {
        public void DeleteCar(CarInformation p) => CarDAO.DeleteCar(p);
        public void SaveCar(CarInformation p) => CarDAO.SaveCar(p);
        public void UpdateCar(CarInformation p) => CarDAO.UpdateCar(p);
        public List<CarInformation> GetCars() => CarDAO.GetCars();
    }
}
