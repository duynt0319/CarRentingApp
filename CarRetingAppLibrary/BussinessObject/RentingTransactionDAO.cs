using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.DataViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.BussinessObject
{
    public class RentingTransactionDAO
    {
        private  FUCarRentingManagementContext context;

        List<RentingDetail> transactionsDetail;
        List<CarInformation> cars;

        public RentingTransactionDAO()
        {
            context = new FUCarRentingManagementContext();
        }

        public List<RentingTransaction> GetRentingTransaction()
        {
            return context.RentingTransactions.Include(r => r.RentingDetails).Include(r => r.Customer).ToList();
        }

        public void CreatingRentingTransactions(RentingTransaction p)
        {
            context.RentingTransactions.Add(p);
            context.SaveChanges();
        }

        public void UpdateRentingTransactions(RentingTransaction p)
        {
            context.Entry(p).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void DeleteRentingTransactions(RentingTransaction p)
        {
            var p1 = context.RentingTransactions.SingleOrDefault(c => c.RentingTransationId == p.RentingTransationId);
            if (p1 != null)
            {
                context.RentingTransactions.Remove(p1);
                context.SaveChanges();
            }
        }

        public List<Customer> GetCustomers()
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

        public RentingTransaction GetTransactionById(int? transactionId)
        {
            RentingTransaction transaction = null;
            try
            {
                var carRentingManagementDB = new FUCarRentingManagementContext();
                transaction =
                    carRentingManagementDB.RentingTransactions.FirstOrDefault(t =>
                        t.RentingTransationId == transactionId);
                    transaction.Customer = carRentingManagementDB.Customers.FirstOrDefault(t =>
                        t.CustomerId == transaction.CustomerId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return transaction;
        }

        //public List<RentingTransactionViewModel> GetRentingTransactionData()
        //{
        //    // Initialize and populate your variables (transactions, transactionsDetail, and cars)

        //    List<RentingTransaction> transactions = GetRentingTransaction();
        //    var transactionDetailViewModels = transactions
        //        .SelectMany(transaction => transactionsDetail
        //            .Where(detail => detail.RentingTransactionId == transaction.RentingTransationId)
        //            .Select(detail => new
        //            {
        //                Transaction = transaction,
        //                Detail = detail
        //            }))
        //        .Join(cars, pair => pair.Detail.CarId, car => car.CarId, (pair, car) => new RentingTransactionViewModel
        //        {
        //            CustomerName = pair.Transaction.Customer.CustomerName,
        //            RentingDate = pair.Transaction.RentingDate,
        //            TotalPrice = pair.Transaction.TotalPrice,
        //            RentingStatus = pair.Transaction.RentingStatus,
        //            CarName = car.CarName,
        //            StartDate = pair.Detail.StartDate,
        //            EndDate = pair.Detail.EndDate,
        //            Price = pair.Detail.Price ?? 0
        //        }).ToList();

        //    return transactionDetailViewModels;
        //}
    }
}
