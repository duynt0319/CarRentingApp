using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.Cars;

namespace NguyenThanhDuyRazorPage.Pages.CarInformations
{
    public class EditModel : PageModel
    {
        private CarRepository carRepository = new CarRepository();

        [BindProperty]
        public CarInformation CarInformation { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || carRepository.GetCars() == null)
            {
                return NotFound();
            }

            var carinformation = carRepository.GetCarByID(id);
            if (carinformation == null)
            {
                return NotFound();
            }
            CarInformation = carinformation;
            ViewData["ManufacturerId"] = new SelectList(carRepository.GetManufacturers(), "ManufacturerId", "ManufacturerName");
            ViewData["SupplierId"] = new SelectList(carRepository.GetSuppliers(), "SupplierId", "SupplierName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}


            try
            {
                carRepository.UpdateCar(CarInformation);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarInformationExists(CarInformation.CarId))
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

        private bool CarInformationExists(int id)
        {
            return (carRepository.GetCarByID(id) != null);
        }
    }
}
