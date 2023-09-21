using CarRetingAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.BussinessObject
{
    public class CarDao
    {
        private static FUCarRentingManagementContext context;

        static CarDao()
        {
            context = new FUCarRentingManagementContext();
        }

        public static List<CarInformation> GetCars()
        {
            return context.CarInformations.ToList();
        }

        public static void CreateCar(CarInformation car)
        {
            context.CarInformations.Add(car);
            context.SaveChanges();
        }

        public static void UpdateCar(CarInformation car)
        {
            context.CarInformations.Update(car);
            context.SaveChanges();
        }

        public static void DeleteCar(CarInformation car)
        {
            var carToDelete = context.CarInformations.SingleOrDefault(c => c.CarId == car.CarId);
            if (carToDelete != null)
            {
                context.CarInformations.Remove(carToDelete);
                context.SaveChanges();
            }
        }
    }
}
