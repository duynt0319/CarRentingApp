using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.Customers;
using NguyenThanhDuyRazorPage.Pages.SessionHelpers;

namespace NguyenThanhDuyRazorPage.Pages.CustomerArea.Customers
{
    public class EditModel : PageModel
    {
        private CustomerRepository customerRepository = new CustomerRepository();

        [BindProperty]
        public Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Customer cus = SessionHelper.GetObjectFromJson<Customer>(HttpContext.Session, "user");
            id = cus.CustomerId;
            if (id == null || customerRepository.GetCustomers() == null)
            {
                return NotFound();
            }

            var customer =   customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            Customer = customer;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            customerRepository = new CustomerRepository();
            try
            {
                customerRepository.UpdateCustomer(Customer);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "user", Customer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(Customer.CustomerId))
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

        private bool CustomerExists(int id)
        {
            return (customerRepository.GetCustomerById(id) != null);
        }
    }
}
