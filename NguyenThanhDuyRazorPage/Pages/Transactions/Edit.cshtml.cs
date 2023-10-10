using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.RentingTransactions;

namespace NguyenThanhDuyRazorPage.Pages.Transactions
{
    public class EditModel : PageModel
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

            var rentingtransaction =  rentingTransactionsRepository.GetRentingTransactionById(id);
            if (rentingtransaction == null)
            {
                return NotFound();
            }
            RentingTransaction = rentingtransaction;
           ViewData["CustomerId"] = new SelectList(rentingTransactionsRepository.GetCustomers(), "CustomerId", "Email");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {


            try
            {
                rentingTransactionsRepository.UpdateRentingTransaction(RentingTransaction);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentingTransactionExists(RentingTransaction.RentingTransationId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool RentingTransactionExists(int id)
        {
          return (rentingTransactionsRepository.GetRentingTransactionById(id) != null);
        }
    }
}
