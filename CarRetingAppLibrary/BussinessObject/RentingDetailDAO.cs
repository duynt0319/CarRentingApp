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
        private static FUCarRentingManagementContext context;

        static RentingDetailDAO()
        {
            context = new FUCarRentingManagementContext(); 
        }

        public static List<RentingDetail> GetRentingDetails()
        {
            return context.RentingDetails.ToList();
        }

        public static void CreateRentingDetail(RentingDetail rentingDetail)
        {
            context.RentingDetails.Add(rentingDetail);
            context.SaveChanges();
        }

        public static void UpdateRentingDetail(RentingDetail rentingDetail)
        {
            context.Entry(rentingDetail).State = EntityState.Modified;
            context.SaveChanges();
        }

    }
}
