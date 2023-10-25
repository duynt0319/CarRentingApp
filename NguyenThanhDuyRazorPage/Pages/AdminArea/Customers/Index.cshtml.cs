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
        private readonly IConfiguration Configuration;

        public IndexModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

       

        public void OnGet(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
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

            switch (sortOrder)
            {
                case "name_desc":
                    customerIQ = customerIQ.OrderByDescending(s => s.CustomerName);
                    break;
                case "Date":
                    customerIQ = customerIQ.OrderBy(s => s.CustomerBirthday);
                    break;
                case "date_desc":
                    customerIQ = customerIQ.OrderByDescending(s => s.CustomerBirthday);
                    break;
                default:
                    customerIQ = customerIQ.OrderBy(s => s.CustomerName);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            CustomerPage = PaginatedList<Customer>.Create(customerIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
