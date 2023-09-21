using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.Repository.RentingTransactions
{
    public interface IRentingTransactionsRepository
    {
        void DeleteRentingTransaction(CarRetingAppLibrary.DataAccess.RentingTransaction c);
        void UpdateRentingTransaction(CarRetingAppLibrary.DataAccess.RentingTransaction c);
        void SaveRentingTransaction(CarRetingAppLibrary.DataAccess.RentingTransaction c);
        List<CarRetingAppLibrary.DataAccess.RentingTransaction> GetRentingTransaction();
    }
}
