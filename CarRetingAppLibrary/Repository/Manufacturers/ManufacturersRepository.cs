using CarRetingAppLibrary.BussinessObject;
using CarRetingAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.Repository.Manufacturers
{
    public class ManufacturersRepository : IManufacturersRepository
    {
        public List<Manufacturer> GetManufacturer() => ManufacturerDAO.GetManufacturers();
    }
}
