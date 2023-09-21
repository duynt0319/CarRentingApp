using CarRetingAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.BussinessObject
{
    internal class SupplierDAO
    {
        public static List<Supplier> GetSupplier()
        {
            var listSupplier = new List<Supplier>();
            using var context = new FUCarRentingManagementContext();
            listSupplier = context.Suppliers.ToList();
            return listSupplier;
        }
    }
}
