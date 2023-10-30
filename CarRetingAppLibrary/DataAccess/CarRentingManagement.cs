using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;

namespace CarRetingAppLibrary.DataAccess
{
    public class CarRentingManagement
    {

        //Using Singleton Pattern
        private static CarRentingManagement instance = null;
        private static readonly object instanceLock = new object();
        private CarRentingManagement() { }

        public static CarRentingManagement Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new CarRentingManagement();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Customer> GetCustomerList()
        {
            List<Customer> customers;
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                customers = carRentingManagementDB.Customers.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customers;
        }


        public IQueryable<Customer> GetCustomerListIQ()
        {
            IQueryable<Customer> customers;
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                customers = carRentingManagementDB.Customers;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customers;
        }

        public Customer GetCustomerByID(int? customerId)
        {
            Customer customer = null;
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                customer = carRentingManagementDB.Customers.
                    SingleOrDefault(customer => customer.CustomerId == customerId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return customer;
        }


        public Customer GetCustomerByEmail(string email)
        {
            Customer customer = null;
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                customer = carRentingManagementDB.Customers.
                    SingleOrDefault(customer => customer.Email.Trim().ToUpper().Equals(email.Trim().ToUpper()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return customer;
        }

        public IEnumerable<Customer> GetCustomerBySearchValue(string value)
        {
            List<Customer> customers;
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                customers = carRentingManagementDB.Customers.Select(x => x).Where(x => x.CustomerName.Trim().ToUpper().Contains(value.Trim().ToUpper())).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customers;
        }


        public Customer CheckCustomer(string email, string password)
        {
            Customer customer = null;

            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                customer = carRentingManagementDB.Customers.
                    SingleOrDefault(customer => customer.Email.Trim().Equals(email) && customer.Password.Trim().Equals(password));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }

            return customer;
        }

        public bool AddNew(Customer customer)
        {
            bool result = false;
            try
            {
                Customer _customer = GetCustomerByID(customer.CustomerId);
                if (_customer == null)
                {
                    //customer.CustomerStatus = 0;
                    var carRentingManagementDB = new FUCarRentingManagementContext();
                    carRentingManagementDB?.Customers.Add(customer);
                    carRentingManagementDB?.SaveChanges();
                    result = true;
                }
                else
                {
                    throw new Exception("The customer is already exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public bool Update(Customer customer)
        {
            bool result = false;
            try
            {
                Customer _customer = GetCustomerByID(customer.CustomerId);
                
                if (_customer != null && !customer.Email.Equals(GetCustomerByID(customer.CustomerId).Email))
                {
                    if (!CheckCustomerEmailIfExist(customer.Email))
                    {
                        var carRentingManagementDB = new FUCarRentingManagementContext();
                        //customer.CustomerStatus = 1;
                        carRentingManagementDB.Entry(customer).State = EntityState.Modified;
                        carRentingManagementDB.SaveChangesAsync();
                        result = true;
                    }
                }
                else
                {
                    if (customer.Email.Equals(GetCustomerByID(customer.CustomerId).Email))
                    {
                        var carRentingManagementDB = new FUCarRentingManagementContext();
                        //customer.CustomerStatus = 1;
                        carRentingManagementDB.Entry(customer).State = EntityState.Modified;
                        carRentingManagementDB.SaveChangesAsync();
                        result = true;
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public bool Delete(Customer customer)
        {
            bool result = false;
            try
            {
                Customer _customer = GetCustomerByID(customer.CustomerId);
                if (_customer != null)
                {
                    if (GetListTransactionsByCustomerId(_customer.CustomerId).Count < 1)
                    {
                        var carRentingManagementDB = new FUCarRentingManagementContext();
                        carRentingManagementDB.Customers.Remove(customer);
                        carRentingManagementDB.SaveChanges();
                        result = true;
                    }
                    else
                    {
                        var carRentingManagementDB = new FUCarRentingManagementContext();
                        customer.CustomerStatus = 0;
                        carRentingManagementDB.Entry(customer).State = EntityState.Modified;
                        carRentingManagementDB.SaveChangesAsync();
                        result = true;
                    }
                }
                else
                {
                    throw new Exception("The customer does not exist.!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }




        public List<RentingTransaction> GetListTransactions()
        {
            List<RentingTransaction> transactions;
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                transactions = carRentingManagementDB.RentingTransactions.ToList();
                foreach (var tran in transactions)
                {
                    if (GetCustomerByID(tran.CustomerId) != null)
                    {
                        tran.Customer = GetCustomerByID(tran.CustomerId);
                    }
                    if (GetListDetailByTransactionId(tran.RentingTransationId) != null)
                    {
                        var list = tran.RentingDetails = GetListDetailByTransactionId(tran.RentingTransationId);
                        //foreach (var dt in list)
                        //{
                        //    if (GetDetailRentingByTransactionId(tran.RentingTransationId) != null)
                        //    {
                        //        var rtdt = GetCarInformationByID(
                        //            GetDetailRentingByTransactionId(tran.RentingTransationId).CarId);
                        //    }
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return transactions;
        }

        public List<RentingTransaction> GetListTransactionsByCustomerId(int customerId)
        {
            List<RentingTransaction> transactions = new List<RentingTransaction>();
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();

                var list = carRentingManagementDB.RentingTransactions.Where(t => t.CustomerId == customerId);
                transactions = list.ToList();
                foreach (var tran in transactions)
                {
                    tran.Customer = GetCustomerByID(tran.CustomerId);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return transactions;
        }

        public List<RentingDetail> GetListTransactionsDetailByTransactionId(int transactionId)
        {
            List<RentingDetail> details = new List<RentingDetail>();
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                var listDetails = carRentingManagementDB.RentingDetails.Where(t => t.RentingTransactionId == transactionId)
                    .Select(x => new
                    {
                        x.RentingTransactionId,
                        x.CarId,
                        x.StartDate,
                        x.EndDate,
                        x.Price,
                    }).ToList();

                foreach (var detail in listDetails)
                {
                    if (detail.RentingTransactionId == transactionId)
                    {
                        RentingDetail rentingDetail = new RentingDetail
                        {
                            RentingTransactionId = transactionId,
                            CarId = detail.CarId,
                            StartDate = detail.StartDate,
                            EndDate = detail.EndDate,
                            Price = detail.Price,
                        };
                        details.Add(rentingDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return details;
        }


        public CarInformation GetCarInformationsByCarId(int? carId)
        {
            CarInformation car = null;
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                car = carRentingManagementDB.CarInformations.FirstOrDefault(t => t.CarId == carId);
                car.Manufacturer = carRentingManagementDB.Manufacturers.FirstOrDefault(m => m.ManufacturerId == car.ManufacturerId);
                car.Supplier = carRentingManagementDB.Suppliers.FirstOrDefault(m => m.SupplierId == car.SupplierId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return car;
        }



        public List<RentingDetail> GetListTransactionsDetailByTransactionList(IEnumerable<RentingTransaction> transactions)
        {
            List<RentingDetail> details = new List<RentingDetail>();
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                foreach (var transaction in transactions)
                {
                    var listDetails = carRentingManagementDB.RentingDetails.Where(t => t.RentingTransactionId == transaction.RentingTransationId)
                        .Select(x => new
                        {
                            x.RentingTransactionId,
                            x.CarId,
                            x.StartDate,
                            x.EndDate,
                            x.Price,
                        }).ToList();
                    foreach (var detail in listDetails)
                    {
                        if (detail.RentingTransactionId == transaction.RentingTransationId)
                        {
                            RentingDetail rentingDetail = new RentingDetail
                            {
                                RentingTransactionId = transaction.RentingTransationId,
                                CarId = detail.CarId,
                                StartDate = detail.StartDate,
                                EndDate = detail.EndDate,
                                Price = detail.Price,
                            };
                            details.Add(rentingDetail);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return details;
        }

        public List<CarInformation> GetListCarsInformationByRentingDetails(IEnumerable<RentingDetail> details)
        {
            List<CarInformation> cars = new List<CarInformation>();
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                foreach (var detail in details)
                {
                    var carList = carRentingManagementDB.CarInformations.Where(t => t.CarId == detail.CarId).Select(x => new
                    {
                        CarId = x.CarId,
                        CarName = x.CarName,
                        CarDescription = x.CarDescription,
                        NumberOfDoors = x.NumberOfDoors,
                        SeatingCapacity = x.SeatingCapacity,
                        FuelType = x.FuelType,
                        Year = x.Year,
                        ManufacturerId = x.ManufacturerId,
                        SupplierId = x.SupplierId,
                        CarStatus = x.CarStatus,
                        CarRentingPricePerDay = x.CarRentingPricePerDay
                    }).ToList(); ;

                    foreach (var cardt in carList)
                    {
                        if (cardt.CarId == detail.CarId)
                        {
                            CarInformation carInformation = new CarInformation
                            {
                                CarId = cardt.CarId,
                                CarName = cardt.CarName,
                                CarDescription = cardt.CarDescription,
                                NumberOfDoors = cardt.NumberOfDoors,
                                SeatingCapacity = cardt.SeatingCapacity,
                                FuelType = cardt.FuelType,
                                Year = cardt.Year,
                                ManufacturerId = cardt.ManufacturerId,
                                SupplierId = cardt.SupplierId,
                                CarStatus = cardt.CarStatus,
                                CarRentingPricePerDay = cardt.CarRentingPricePerDay
                            };
                            cars.Add(carInformation);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cars;
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

        public CarInformation GetCarInformationByID(int carId)
        {
            CarInformation car = null;
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                car = carRentingManagementDB.CarInformations.
                    SingleOrDefault(car => car.CarId == carId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return car;
        }
        public RentingDetail GetDetailRentingByTransactionId(int transactionId)
        {
            RentingDetail detail = null;
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                detail = carRentingManagementDB.RentingDetails.
                    SingleOrDefault(r => r.RentingTransactionId == transactionId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return detail;
        }
        public bool AddNewCar(CarInformation car)
        {
            bool result = false;
            try
            {
                CarInformation _car = GetCarInformationsByCarId(car.CarId);
                if (_car == null)
                {
                    var carRentingManagementDB = new FUCarRentingManagementContext();
                    car.Manufacturer = null;
                    car.Supplier = null;
                    carRentingManagementDB?.CarInformations.Add(car);
                    carRentingManagementDB?.SaveChanges();
                    result = true;
                }
                else
                {
                    throw new Exception("The car is already exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public bool UpdateCar(CarInformation car)
        {
            bool result = false;
            try
            {
                CarInformation _car = GetCarInformationsByCarId(car.CarId);
                if (_car != null)
                {
                    var carRentingManagementDB = new FUCarRentingManagementContext();
                    carRentingManagementDB.Entry(car).State = EntityState.Modified;
                    carRentingManagementDB.SaveChanges();
                    result = true;
                }
                else
                {
                    throw new Exception("The car does not exist.!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public string DeleteCar(CarInformation car)
        {
            string result = "";
            try
            {
                CarInformation _car = GetCarInformationsByCarId(car.CarId);
                if (_car != null)
                {
                    var carRentingManagementDB = new FUCarRentingManagementContext();
                    var detail = carRentingManagementDB.RentingDetails.FirstOrDefault(x => x.CarId == car.CarId);
                    if (detail == null)
                    {
                        carRentingManagementDB.CarInformations.Remove(car);
                        carRentingManagementDB.SaveChanges();
                        result = $"{car.CarName} was deleted successfully!";
                    }
                    else
                    {
                        car.CarStatus = 0;
                        carRentingManagementDB.Entry(car).State = EntityState.Modified;
                        carRentingManagementDB.SaveChanges();
                        result = "Because of this car was rent in another transaction, so the status will change to unavailable for this car!";
                    }

                }
                else
                {
                    throw new Exception("The car does not exist.!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
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

        public IQueryable<CarInformation> GetCarInformationsIq()
        {
            IQueryable<CarInformation> cars;
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                cars = carRentingManagementDB.CarInformations;
                foreach (var car in cars)
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

                //carRentingManagementDB.CarInformations.Include(m => m.Manufacturer);
                //carRentingManagementDB.CarInformations.Include(s => s.Supplier);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cars;
        }

        public IEnumerable<CarInformation> GetCarInformationsBySearchValue(string value)
        {
            List<CarInformation> cars;
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                cars = carRentingManagementDB.CarInformations.Select(x => x).Where(x => x.CarName.ToUpper().Contains(value.Trim().ToUpper())).ToList();
                foreach (var car in cars)
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
            return cars;
        }

        //public List<Supplier> GetSuppliers()
        //{
        //    List<Supplier> suppliers = null;
        //    try
        //    {
        //        var supplierDB = new FUCarRentingManagementContext();
        //        suppliers =
        //            supplierDB.Suppliers.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //    return suppliers;
        //}

        //public List<Manufacturer> GetManufacturers()
        //{
        //    List<Manufacturer> manufacturers = null;
        //    try
        //    {
        //        var manufacturerDB = new FUCarRentingManagementContext();
        //        manufacturers =
        //            manufacturerDB.Manufacturers.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //    return manufacturers;
        //}

        public Manufacturer GetManufactureByName(string name)
        {
            Manufacturer manufacturer = null;
            try
            {
                var manufacturerDB = new FUCarRentingManagementContext();
                manufacturer =
                    manufacturerDB.Manufacturers.SingleOrDefault(manu => manu.ManufacturerName.Trim().Equals(name.Trim()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return manufacturer;
        }

        public Supplier GetSupplierByName(string name)
        {
            Supplier supplier = null;
            try
            {
                var supplierDB = new FUCarRentingManagementContext();
                supplier =
                    supplierDB.Suppliers.SingleOrDefault(sup => sup.SupplierName.Trim().Equals(name.Trim()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return supplier;
        }


        public string GetCustomerPasswordByID(int? customerId)
        {
            string password = "";
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                password = carRentingManagementDB.Customers.
                    SingleOrDefault(customer => customer.CustomerId == customerId).Password;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return password;
        }


        public List<RentingTransaction> GetTransactionsBySearchValue(string name)
        {
            List<RentingTransaction> transactions = null;
            try
            {
                var transactionDB = new FUCarRentingManagementContext();
                transactions =
                    transactionDB.RentingTransactions.Select(tran => tran).Where(tran => tran.Customer.CustomerName.Trim().ToUpper().Contains(name.Trim().ToUpper())).ToList();
                foreach (var transaction in transactions)
                {
                    if (GetCustomerByID(transaction.CustomerId) != null)
                    {
                        transaction.Customer = GetCustomerByID(transaction.CustomerId);
                    }
                    if (GetListDetailByTransactionId(transaction.RentingTransationId) != null)
                    {
                        transaction.RentingDetails = GetListDetailByTransactionId(transaction.RentingTransationId);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return transactions;
        }

        public List<RentingDetail> GetListDetailByTransactionId(int transactionId)
        {
            List<RentingDetail> details = null;
            try
            {
                var rentingDetailDB = new FUCarRentingManagementContext();
                details =
                    rentingDetailDB.RentingDetails.Select(d => d).Where(d => d.RentingTransactionId == transactionId).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return details;
        }


        public Customer GetCustomerByEmailAndPassword(string email, string password)
        {
            Customer customer = null;
            try
            {
                var carRentingManageMent = new FUCarRentingManagementContext();
                customer = carRentingManageMent.Customers.FirstOrDefault(c => c.Email.Trim().Equals(email.Trim()) && c.Password.Equals(password.Trim()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return customer;
        }


        public bool CheckCustomerIfExist(int? id)
        {
            bool result = false;
            try
            {
                var carRentingManageMent = new FUCarRentingManagementContext();
                result = (carRentingManageMent.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }


        public bool CheckCustomerEmailIfExist(string? email)
        {
            bool result = false;
            try
            {
                var carRentingManageMent = new FUCarRentingManagementContext();
                result = (carRentingManageMent.Customers?.Any(e => e.Email.Equals(email.Trim()))).GetValueOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public bool CheckCarInformationIfExist(int? id)
        {
            bool result = false;
            try
            {
                var carRentingManageMent = new FUCarRentingManagementContext();
                result = (carRentingManageMent.CarInformations?.Any(e => e.CarId == id)).GetValueOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }



        public List<RentingDetail> GetListRentingDetails()
        {
            List<RentingDetail> details = null;
            try
            {
                var rentingDetailDB = new FUCarRentingManagementContext();
                details =
                    rentingDetailDB.RentingDetails.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return details;
        }

        public RentingTransaction GetTransactionById(int? transactionId)
        {
            RentingTransaction transaction = null;
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                transaction =
                    carRentingManagementDB.RentingTransactions.FirstOrDefault(t =>
                        t.RentingTransationId == transactionId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return transaction;
        }

        public bool CheckIfTransactionExist(int? id)
        {
            bool result = false;
            try
            {
                var carRentingManageMent = new FUCarRentingManagementContext();
                result = (carRentingManageMent.RentingTransactions?.Any(e => e.RentingTransationId == id)).GetValueOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public bool AddNewTransaction(RentingTransaction transaction)
        {
            bool result = false;
            try
            {
                var _transaction = GetTransactionById(transaction.RentingTransationId);
                if (_transaction == null)
                {
                    var carRentingManagementDB = new FUCarRentingManagementContext();
                    carRentingManagementDB?.RentingTransactions.Add(transaction);
                    carRentingManagementDB?.SaveChanges();
                    result = true;
                }
                else
                {
                    throw new Exception("The transaction is already exist!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public bool UpdateTransaction(RentingTransaction transaction)
        {
            bool result = false;
            try
            {
                var _transaction = GetTransactionById(transaction.RentingTransationId);
                if (_transaction != null)
                {
                    var carRentingManagementDB = new FUCarRentingManagementContext();
                    carRentingManagementDB.Entry(transaction).State = EntityState.Modified;
                    carRentingManagementDB.SaveChanges();
                    result = true;
                }
                else
                {
                    throw new Exception("The car does not exist.!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

        public string DeleteTransaction(RentingTransaction transaction)
        {
            string result = "";
            try
            {
                var _transaction = GetTransactionById(transaction.RentingTransationId);
                if (_transaction != null)
                {
                    var carRentingManagementDB = new FUCarRentingManagementContext();
                    var transactionInformation = carRentingManagementDB.RentingTransactions.FirstOrDefault(x => x.CustomerId == transaction.Customer.CustomerId);
                    if (transactionInformation == null)
                    {
                        carRentingManagementDB.RentingTransactions.Remove(transactionInformation);
                        carRentingManagementDB.SaveChanges();
                        result = $"{transactionInformation.RentingTransationId} was deleted successfully!";
                    }
                    else
                    {
                       transactionInformation.RentingStatus = 0;
                        carRentingManagementDB.Entry(transactionInformation).State = EntityState.Modified;
                        carRentingManagementDB.SaveChanges();
                        result = "Because of this transaction was owner by a customer, so the status will change to unavailable for this car!";
                    }

                }
                else
                {
                    throw new Exception("The transaction does not exist.!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return result;
        }

    }
}
