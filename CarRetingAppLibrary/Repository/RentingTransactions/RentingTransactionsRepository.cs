using CarRetingAppLibrary.BussinessObject;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.DataViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.Repository.RentingTransactions
{
    public class RentingTransactionsRepository : IRentingTransactionsRepository
    {
        RentingTransactionDAO rentingTransactionDAO = new RentingTransactionDAO();
        public void CreateRentingTransaction(RentingTransaction transaction)
        {
            rentingTransactionDAO.CreatingRentingTransactions(transaction);
        }

        public void DeleteRentingTransaction(RentingTransaction transaction)
        {
            rentingTransactionDAO.DeleteRentingTransactions(transaction);
        }

        public List<Customer> GetCustomers()
        {
            return rentingTransactionDAO.GetCustomers();
        }

        public RentingTransaction GetRentingTransactionById(int? id)
        {
            return rentingTransactionDAO.GetTransactionById(id);
        }

        public List<RentingTransaction> GetRentingTransactions()
        {
            return rentingTransactionDAO.GetRentingTransaction();
        }

        //public List<RentingTransactionViewModel> GetRentingTransactionViewModels()
        //{
        //   return rentingTransactionDAO.GetRentingTransactionData();
        //}

        public void UpdateRentingTransaction(RentingTransaction transaction)
        {
            rentingTransactionDAO.UpdateRentingTransactions(transaction);
        }
    }
}
