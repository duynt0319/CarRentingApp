using CarRetingAppLibrary.BussinessObject;
using CarRetingAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.Repository.RentingTransactions
{
    public class RentingTransactionsRepository : IRentingTransactionsRepository
    {
        public void CreateRentingTransaction(RentingTransaction transaction)
        {
           RentingTransactionDAO.CreatingRentingTransactions(transaction);
        }

        public void DeleteRentingTransaction(RentingTransaction transaction)
        {
            RentingTransactionDAO.DeleteRentingTransactions(transaction);
        }

        public List<RentingTransaction> GetRentingTransactions()
        {
            return RentingTransactionDAO.GetRentingTransaction();
        }

        public void UpdateRentingTransaction(RentingTransaction transaction)
        {
            RentingTransactionDAO.UpdateRentingTransactions(transaction);
        }
    }
}
