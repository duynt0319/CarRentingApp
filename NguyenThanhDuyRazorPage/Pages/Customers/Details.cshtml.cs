using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.Customers;

namespace NguyenThanhDuyRazorPage.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private CustomerRepository customerRepository = new CustomerRepository();

        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || customerRepository.GetCustomers() == null)
            {
                return NotFound();
            }

            var customer = customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            else
            {
                Customer = customer;
            }
            return Page();
        }
    }
}
