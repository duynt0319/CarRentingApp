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

        public IEnumerable<CarInformation> GetCarInformations()
        {
            List<CarInformation> carInformations;
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                carInformations = carRentingManagementDB.CarInformations.ToList();
                foreach (var car in carInformations)
                {
                    if (GetManufacturerById(car.ManufacturerId) != null)
                    {
                        car.Manufacturer = GetManufacturerById(car.ManufacturerId);
                    }

                    if (GetSupplierById(car.SupplierId) != null)
                    {
                        car.Supplier = GetSupplierById(car.SupplierId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return carInformations;
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


        public Manufacturer GetManufacturerById(int manufactureId)
        {
            Manufacturer manufacturer = null;
            try
            {
                var manufacturerDB = new FUCarRentingManagementContext();
                manufacturer =
                    manufacturerDB.Manufacturers.SingleOrDefault(manu => manu.ManufacturerId == manufactureId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return manufacturer;
        }

        public Supplier GetSupplierById(int supplierId)
        {
            Supplier supplier = null;
            try
            {
                var supplierDB = new FUCarRentingManagementContext();
                supplier =
                    supplierDB.Suppliers.SingleOrDefault(sup => sup.SupplierId == supplierId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return supplier;
        }
    }
}
