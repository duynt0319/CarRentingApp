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
        private FUCarRentingManagementContext context;

        public CarDao()
        {
            context = new FUCarRentingManagementContext();
        }

        public List<CarInformation> GetCars()
        {
            return context.CarInformations.Where(car => car.CarStatus == 1).ToList();
        }

        public void CreateCar(CarInformation car)
        {
            context.CarInformations.Add(car);
            context.SaveChanges();
        }

        public void UpdateCar(CarInformation car)
        {
            context.CarInformations.Update(car);
            context.SaveChanges();
        }

        public void DeleteCar(CarInformation car)
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

        public List<Manufacturer> GetManufacturers()
        {
            List<Manufacturer> manufacturers = null;
            try
            {
                var manufacturerDB = new FUCarRentingManagementContext();
                manufacturers =
                    manufacturerDB.Manufacturers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return manufacturers;
        }

        public List<Supplier> GetSuppliers()
        {
            List<Supplier> suppliers = null;
            try
            {
                var supplierDB = new FUCarRentingManagementContext();
                suppliers =
                    supplierDB.Suppliers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return suppliers;
        }
    }
}
