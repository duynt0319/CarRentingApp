using CarRetingSystemLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingSystemLibrary.Repository.Cars
{
    public interface ICarRepository
    {
        void SaveCar(CarInformation c);
        void DeleteCar(CarInformation c);
        void UpdateCar(CarInformation c);
        List<CarInformation> GetCars();
    }
}
