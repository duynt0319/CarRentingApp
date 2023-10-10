using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.Cars;

namespace NguyenThanhDuyRazorPage.Pages.CarInformations
{
    public class CreateModel : PageModel
    {
        private CarRepository carRepository = new CarRepository();
        public IActionResult OnGet()
        {
        ViewData["ManufacturerId"] = new SelectList(carRepository.GetManufacturers(), "ManufacturerId", "ManufacturerName");
        ViewData["SupplierId"] = new SelectList(carRepository.GetSuppliers(), "SupplierId", "SupplierName");
            return Page();
        }

        [BindProperty]
        public CarInformation CarInformation { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if ( carRepository.GetCars == null || CarInformation == null)
            {
                return Page();
            }

            carRepository.CreateCar(CarInformation);

            return RedirectToPage("./Index");
        }
    }
}
