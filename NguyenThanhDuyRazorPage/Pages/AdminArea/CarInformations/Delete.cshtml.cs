using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.Cars;

namespace NguyenThanhDuyRazorPage.Pages.AdminArea.CarInformations
{
    public class DeleteModel : PageModel
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

            var carinformation = await carRepository.GetCarByID(id);

            if (carinformation == null)
            {
                return NotFound();
            }
            else
            {
                CarInformation = carinformation;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || carRepository.GetCars() == null)
            {
                return NotFound();
            }
            var carinformation = await carRepository.GetCarByID(id);

            if (carinformation != null)
            {
                CarInformation = carinformation;
                carRepository.DeleteCar(CarInformation);
            }

            return RedirectToPage("./Index");
        }
    }
}
