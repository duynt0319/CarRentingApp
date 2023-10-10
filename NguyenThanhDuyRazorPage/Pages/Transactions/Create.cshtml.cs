using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.RentingDetail;
using CarRetingAppLibrary.BussinessObject;
using CarRetingAppLibrary.Repository.RentingTransactions;

namespace NguyenThanhDuyRazorPage.Pages.Transactions
{
    public class CreateModel : PageModel
    {
        private RentingTransactionsRepository rentingTransactionsRepository = new RentingTransactionsRepository();

        public IActionResult OnGet()
        {
        ViewData["CustomerId"] = new SelectList(rentingTransactionsRepository.GetCustomers(), "CustomerId", "Email");
            return Page();
        }

        [BindProperty]
        public RentingTransaction RentingTransaction { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if ( rentingTransactionsRepository.GetRentingTransactions() == null || RentingTransaction == null)
            {
                return Page();
            }

            rentingTransactionsRepository.CreateRentingTransaction(RentingTransaction);

            return RedirectToPage("./Index");
        }
    }
}
