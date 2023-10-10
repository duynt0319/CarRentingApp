using CarRetingAppLibrary.DataAccess;
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
    }
}
