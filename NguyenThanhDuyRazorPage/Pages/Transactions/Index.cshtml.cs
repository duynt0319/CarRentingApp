using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRetingAppLibrary.DataAccess;

namespace NguyenThanhDuyRazorPage.Pages.Transactions
{
    public class IndexModel : PageModel
    {
        private readonly CarRetingAppLibrary.DataAccess.FUCarRentingManagementContext _context;

        public IndexModel(CarRetingAppLibrary.DataAccess.FUCarRentingManagementContext context)
        {
            _context = context;
        }

        public IList<RentingTransaction> RentingTransaction { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.RentingTransactions != null)
            {
                RentingTransaction = await _context.RentingTransactions
                .Include(r => r.Customer).ToListAsync();
            }
        }
    }
}
