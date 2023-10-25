using CarRetingAppLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.BussinessObject
{
    public class RentingDetailDAO
    {
        private FUCarRentingManagementContext context;

         public RentingDetailDAO()
        {
            context = new FUCarRentingManagementContext(); 
        }

        public List<RentingDetail> GetRentingDetails()
        {
            return context.RentingDetails
                .Include(r => r.Car)
                .Include(r => r.RentingTransaction).ToList(); ;
        }

        public void CreateRentingDetail(RentingDetail rentingDetail)
        {
            context.RentingDetails.Add(rentingDetail);
            context.SaveChanges();
        }

        public void UpdateRentingDetail(RentingDetail rentingDetail)
        {
            context.Entry(rentingDetail).State = EntityState.Modified;
            context.SaveChanges();
        }

    }
}
