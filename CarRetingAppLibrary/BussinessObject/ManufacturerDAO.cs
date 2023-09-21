using CarRetingAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.BussinessObject
{
    public class ManufacturerDAO
    {
        public static List<Manufacturer> GetManufacturers()
        {
            using var context = new FUCarRentingManagementContext();
            return context.Manufacturers.ToList();
        }

    }
}
