using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.Customers;

namespace NguyenThanhDuyRazorPage.Pages.AdminArea.Customers
{
    public class CreateModel : PageModel
    {

        private CustomerRepository customerRepository = new CustomerRepository();

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || customerRepository.GetCustomers() == null || Customer == null)
            {
                return Page();
            }

            customerRepository.CreateCustomer(Customer);

            return RedirectToPage("./Index");
        }
    }
}
