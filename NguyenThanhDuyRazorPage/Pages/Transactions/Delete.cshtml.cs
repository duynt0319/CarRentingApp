using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.RentingTransactions;

namespace NguyenThanhDuyRazorPage.Pages.Transactions
{
    public class DeleteModel : PageModel
    {
        private RentingTransactionsRepository rentingTransactionsRepository = new RentingTransactionsRepository();

        [BindProperty]
        public RentingTransaction RentingTransaction { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || rentingTransactionsRepository.GetRentingTransactions() == null)
            {
                return NotFound();
            }

            var rentingtransaction = rentingTransactionsRepository.GetRentingTransactionById(id);

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || rentingTransactionsRepository.GetRentingTransactions() == null)
            {
                return NotFound();
            }
            var rentingtransaction =rentingTransactionsRepository.GetRentingTransactionById(id);

            if (rentingtransaction != null)
            {
                RentingTransaction = rentingtransaction;
                rentingTransactionsRepository.DeleteRentingTransaction(RentingTransaction);
            }

            return RedirectToPage("./Index");
        }
    }
}
