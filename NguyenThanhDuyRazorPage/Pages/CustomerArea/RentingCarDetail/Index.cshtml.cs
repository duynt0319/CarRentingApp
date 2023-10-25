using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRetingAppLibrary.DataAccess;

namespace NguyenThanhDuyRazorPage.Pages.CustomerArea.RentingCarDetail
{
    public class IndexModel : PageModel
    {
        private readonly CarRetingAppLibrary.DataAccess.FUCarRentingManagementContext _context;

        public IndexModel(CarRetingAppLibrary.DataAccess.FUCarRentingManagementContext context)
        {
            _context = context;
        }

        public IList<RentingDetail> RentingDetail { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.RentingDetails != null)
            {
                RentingDetail = await _context.RentingDetails
                .Include(r => r.Car)
                .Include(r => r.RentingTransaction).ToListAsync();
            }
        }
    }
}
