﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.RentingTransactions;

namespace NguyenThanhDuyRazorPage.Pages.AdminArea.Transactions
{
    public class IndexModel : PageModel
    {
        private RentingTransactionsRepository rentingTransactionsRepository = new RentingTransactionsRepository();

        public IList<RentingTransaction> RentingTransaction { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (rentingTransactionsRepository.GetRentingTransactions() != null)
            {
                RentingTransaction = rentingTransactionsRepository.GetRentingTransactions();
            }
        }
    }
}
