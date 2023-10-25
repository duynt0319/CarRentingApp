using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.Customers;
using CarRetingAppLibrary.Pagging;
using System.Configuration;

namespace NguyenThanhDuyRazorPage.Pages.AdminArea.Customers
{
    public class IndexModel : PageModel
    {
        private CustomerRepository _customerRepository = new CustomerRepository();
        public PaginatedList<Customer> CustomerPage { get; set; }
        public string CurrentFilter { get; set; }
        private readonly IConfiguration Configuration;
        public IndexModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void OnGet(string currentFilter, string searchString, int? pageIndex)
        {
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = CurrentFilter;
            }

            if (currentFilter != null)
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;

            IQueryable<Customer> customerIQ = _customerRepository.GetListCustomers();


            if (!String.IsNullOrEmpty(searchString))
            {
                customerIQ = customerIQ.Where(s => s.CustomerName.Contains(searchString));
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            CustomerPage = PaginatedList<Customer>.Create(customerIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
