using CarRentingManagementLibrary.DataAccess;
using CarRetingAppLibrary.Repository.Customers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NguyenThanhDuyRazorPage.Pages.SessionHelpers;

namespace NguyenThanhDuyRazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private CustomerRepository _customerRepository;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {

            _customerRepository = new CustomerRepository();

            Admin userAdmin = new Admin();
            if (Email.Trim().Equals(userAdmin.Email) && Password.Trim().Equals(userAdmin.Password))
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "admin", userAdmin);
                return RedirectToPage("/ControllerPages/AdminPage");
            }
            var customer = _customerRepository.CheckCustomer(Email, Password);
            if (customer != null)
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "user", customer);
                return RedirectToPage("/ControllerPages/CustomerPage");
            }
            else
            {
                // Invalid login, show an error or redirect back to the login page with a message
                TempData["ErrorMessage"] = "Invalid email or password.";
                return Page();
            }
        }
    }
}