using System;
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
    public class DetailsModel : PageModel
    {
        private RentingTransactionsRepository transactionsRepository = new RentingTransactionsRepository();

      public RentingTransaction RentingTransaction { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || transactionsRepository.GetRentingTransactions() == null)
            {
                return NotFound();
            }

            var rentingtransaction = transactionsRepository.GetRentingTransactionById(id);
            if (rentingtransaction == null)
            {
                return NotFound();
            }
            else 
            {
                RentingTransaction = rentingtransaction;
            }
            return Page();
        }
    }
}
