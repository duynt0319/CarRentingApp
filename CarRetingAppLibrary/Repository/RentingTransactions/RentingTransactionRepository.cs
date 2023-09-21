using CarRetingSystemLibrary.BussinessObject;
using CarRetingSystemLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingSystemLibrary.Repository.RentingTransactions
{
    public class RentingTransactionRepository : IRentingTransactionRepository
    {
        public void DeleteRentingTransaction(RentingTransaction c) => RentingTransactionDAO.DeleteRentingTransactions(c);
        public void UpdateRentingTransaction(RentingTransaction c) => RentingTransactionDAO.UpdateRentingTransactions(c);
        public void SaveRentingTransaction(RentingTransaction c) => RentingTransactionDAO.SaveRentingTransactions(c);
        public List<RentingTransaction> GetRentingTransaction() => RentingTransactionDAO.GetRentingTransaction();
    }
}

