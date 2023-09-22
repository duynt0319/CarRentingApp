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
            return context.CarInformations.Where(car => car.CarStatus == 1).ToList();
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
                var rentingTransactions = context.RentingDetails
                    .Where(rd => rd.CarId == car.CarId)
                    .Select(rd => rd.RentingTransactionId)
                    .ToList();

                if (rentingTransactions.Count == 0)
                {
                    context.CarInformations.Remove(carToDelete);
                }
                else
                {
                    carToDelete.CarStatus = 0; 
                }

                context.SaveChanges();
            }
        }
    }
}
