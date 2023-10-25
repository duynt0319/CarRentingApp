using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.RentingDetailRepo;
using CarRetingAppLibrary.Repository.Cars;
using CarRetingAppLibrary.Repository.RentingTransactions;
using Microsoft.EntityFrameworkCore;
using NguyenThanhDuyRazorPage.Pages.SessionHelpers;

namespace NguyenThanhDuyRazorPage.Pages.CustomerArea.RentingCarDetail
{
    public class CreateModel : PageModel
    {
        private readonly CarRetingAppLibrary.DataAccess.FUCarRentingManagementContext _context;
        private CarRepository carRepository = new CarRepository();

        public CreateModel(CarRetingAppLibrary.DataAccess.FUCarRentingManagementContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGet(int? id)
        {

            var carInformation = await carRepository.GetCarByID(id);
            if (id == null || carInformation == null || carRepository.GetCars() == null)
            {
                return NotFound();
            }
            var carName = carInformation.CarName;
            ViewData["CarId"] = new SelectList(carName, "CarId", "CarName");

            CarInformation = carInformation;
            var user = SessionHelper.GetObjectFromJson<Customer>(HttpContext.Session, "user");
            if (user != null)
            {
                int userId = user.CustomerId;
                var userCustomers = _context.Customers.Where(c => c.CustomerId == userId).ToList();
                ViewData["CustomerId"] = new SelectList(userCustomers, "CustomerId", "Email");
            }
            return Page();

        }

        [BindProperty]
        public RentingDetail RentingDetail { get; set; } = default!;

        [BindProperty]
        public CarInformation CarInformation { get; set; } = default!;
        [BindProperty]
        public RentingTransaction RentingTransaction { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {


                var car = await _context.CarInformations.Where(c => c.CarId.Equals(CarInformation.CarId)).SingleOrDefaultAsync();
                var totalPrice = (RentingDetail.EndDate - RentingDetail.StartDate).Days * car.CarRentingPricePerDay;
                var user = SessionHelper.GetObjectFromJson<Customer>(HttpContext.Session, "user");
                if (user == null)
                {
                    ModelState.AddModelError("", "User is null");
                    return Page();
                }
                int userId = user.CustomerId;

                var rentingTransaction = new RentingTransaction
                {
                    RentingTransationId = RentingTransaction.RentingTransationId,
                    RentingDate = RentingTransaction.RentingDate,
                    CustomerId = userId,
                    RentingStatus = 1,
                    TotalPrice = totalPrice,
                };
                _context.RentingTransactions.Add(rentingTransaction);
                var rentingDetail = new RentingDetail
                {
                    RentingTransactionId = rentingTransaction.RentingTransationId,
                    CarId = CarInformation.CarId,
                    StartDate = RentingDetail.StartDate,
                    EndDate = RentingDetail.EndDate,
                    Price = totalPrice,
                };
                _context.RentingDetails.Add(rentingDetail);

                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Page();
            }
        }
    }
}
