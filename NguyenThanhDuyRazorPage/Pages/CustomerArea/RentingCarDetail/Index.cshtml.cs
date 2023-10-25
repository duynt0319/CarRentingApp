using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CarRetingAppLibrary.DataAccess;
using CarRetingAppLibrary.Repository.RentingDetailRepo;

namespace NguyenThanhDuyRazorPage.Pages.CustomerArea.RentingCarDetail
{
    public class IndexModel : PageModel
    {

        private RentingDetailRepository detailRepository = new RentingDetailRepository();

        public IList<RentingDetail> RentingDetail { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (detailRepository.GetRentingDetail() != null)
            {
                RentingDetail = detailRepository.GetRentingDetail();
            }
        }
    }
}
