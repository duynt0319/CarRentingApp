﻿using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.DataViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.Repository.RentingTransactions
{
    public interface IRentingTransactionsRepository
    {
        void CreateRentingTransaction(RentingTransaction transaction);
        void DeleteRentingTransaction(RentingTransaction transaction);
        void UpdateRentingTransaction(RentingTransaction transaction);
        List<RentingTransaction> GetRentingTransactions();
        List<Customer> GetCustomers();

        RentingTransaction GetRentingTransactionById(int? id);

        //List<RentingTransactionViewModel> GetRentingTransactionViewModels();
    }
}
