using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.Cars;

namespace NguyenThanhDuyRazorPage.Pages.ControllerPages
{
    public class CustomerPage : PageModel
    {
       private CarRepository CarRepository= new CarRepository();

        public IList<CarInformation> CarInformation { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (CarRepository.GetCars != null)
            {
                CarInformation = CarRepository.GetCars();
            }
        }
    }
}
