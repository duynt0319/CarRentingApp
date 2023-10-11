using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.Customers;

namespace NguyenThanhDuyRazorPage.Pages.AdminArea.Customers
{
    public class IndexModel : PageModel
    {
        private CustomerRepository customerRepository = new CustomerRepository();

        public IList<Customer> Customer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (customerRepository.GetCustomers != null)
            {
                Customer =  customerRepository.GetCustomers();
            }
        }
    }
}
