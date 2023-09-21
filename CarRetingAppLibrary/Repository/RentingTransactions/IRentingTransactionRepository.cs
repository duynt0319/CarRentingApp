using CarRetingSystemLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingSystemLibrary.Repository.RentingTransactions
{
    public interface IRentingTransactionRepository
    {
        void DeleteRentingTransaction(CarRetingSystemLibrary.DataAccess.RentingTransaction c);
        void UpdateRentingTransaction(CarRetingSystemLibrary.DataAccess.RentingTransaction c);
        void SaveRentingTransaction(CarRetingSystemLibrary.DataAccess.RentingTransaction c);
        List<CarRetingSystemLibrary.DataAccess.RentingTransaction> GetRentingTransaction();
    }
}
