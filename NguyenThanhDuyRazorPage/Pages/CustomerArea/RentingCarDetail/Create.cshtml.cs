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
        private CarRepository carRepository = new CarRepository();
        private RentingTransactionsRepository rentingTransactionsRepository = new RentingTransactionsRepository();
        private RentingDetailRepository detailRepository = new RentingDetailRepository();


        public async Task<IActionResult> OnGet(int? id)
        {
            var carInformation = await carRepository.GetCarByID(id);
            if (id == null || carInformation == null || carRepository.GetCars() == null)
            {
                return NotFound();
            }
            CarInformation = carInformation;
            return Page();
        }


        [BindProperty]
        public RentingDetail RentingDetail { get; set; } = default!;

        [BindProperty]
        public CarInformation CarInformation { get; set; } = default!;

        [BindProperty]
        public RentingTransaction RentingTransaction { get; set; } = default!;


        public async Task<IActionResult> OnPostAsync(int? id)
        {
            try
            {
                var user = SessionHelper.GetObjectFromJson<Customer>(HttpContext.Session, "user");
                if (user == null)
                {
                    ModelState.AddModelError("", "User is null");
                    return Page();
                }

                var carInformation = await carRepository.GetCarByID(id);
                var totalPrice = (RentingDetail.EndDate - RentingDetail.StartDate).Days * carInformation.CarRentingPricePerDay;

                var rentingTransaction = new RentingTransaction
                {
                    RentingTransationId = RentingTransaction.RentingTransationId,
                    RentingDate = DateTime.Now,
                    CustomerId = user.CustomerId,
                    RentingStatus = 1,
                    TotalPrice = totalPrice,
                };
                rentingTransactionsRepository.CreateRentingTransaction(rentingTransaction);

                var rentingDetail = new RentingDetail
                {
                    RentingTransactionId = rentingTransaction.RentingTransationId,
                    CarId = CarInformation.CarId,
                    StartDate = RentingDetail.StartDate,
                    EndDate = RentingDetail.EndDate,
                    Price = totalPrice,
                };
                detailRepository.createRentingDetail(rentingDetail);

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
