using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.Customers;
using NguyenThanhDuyRazorPage.Pages.SessionHelpers;

namespace NguyenThanhDuyRazorPage.Pages.CustomerArea.UserInformation
{
    public class IndexModel : PageModel
    {
        private CustomerRepository customerRepository = new CustomerRepository();

        public Customer Customer { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Customer cus = SessionHelper.GetObjectFromJson<Customer>(HttpContext.Session, "user");
            id = cus.CustomerId;
            if (id == null || customerRepository.GetCustomers() == null)
            {
                return NotFound();
            }

            var getCusByID = customerRepository.GetCustomerById(id);
            if (getCusByID == null)
            {
                return NotFound();
            }
            else
            {
                Customer = getCusByID;
            }
            return Page();
        }
    }
}
