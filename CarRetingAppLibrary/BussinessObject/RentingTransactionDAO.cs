using CarRetingAppLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.BussinessObject
{
    public class RentingTransactionDAO
    {
        private static FUCarRentingManagementContext context;

        static RentingTransactionDAO()
        {
            context = new FUCarRentingManagementContext();
        }

        public static List<RentingTransaction> GetRentingTransaction()
        {
            return context.RentingTransactions.ToList();
        }

        public static void CreatingRentingTransactions(RentingTransaction p)
        {
            context.RentingTransactions.Add(p);
            context.SaveChanges();
        }

        public static void UpdateRentingTransactions(RentingTransaction p)
        {
            context.Entry(p).State = EntityState.Modified;
            context.SaveChanges();
        }

        public static void DeleteRentingTransactions(RentingTransaction p)
        {
            var p1 = context.RentingTransactions.SingleOrDefault(c => c.RentingTransationId == p.RentingTransationId);
            if (p1 != null)
            {
                context.RentingTransactions.Remove(p1);
                context.SaveChanges();
            }
        }
    }
}
