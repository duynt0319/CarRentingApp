using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.Cars;
using CarRetingAppLibrary.Pagging;

namespace NguyenThanhDuyRazorPage.Pages.AdminArea.CarInformations
{
    public class IndexModel : PageModel
    {
        private CarRepository _carRepository = new CarRepository();
        private readonly IConfiguration Configuration;

        public IndexModel(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public string CurrentFilter { get; set; }


        public PaginatedList<CarInformation> CarInformations { get; set; }

        public void OnGet(string sortOrder, string currentFilter, string searchString, int? pageIndex)
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

            IQueryable<CarInformation> carIQ = _carRepository.GetIqCarInformations();


            if (!String.IsNullOrEmpty(searchString))
            {
                carIQ = carIQ.Where(s => s.CarName.Contains(searchString));
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            CarInformations = PaginatedList<CarInformation>.Create(carIQ, pageIndex ?? 1, pageSize);
        }

    }
}
