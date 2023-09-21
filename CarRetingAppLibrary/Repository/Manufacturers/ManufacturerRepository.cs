using CarRetingSystemLibrary.BussinessObject;
using CarRetingSystemLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingSystemLibrary.Repository.Manufacturers
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        public List<Manufacturer> GetManufacturer() => ManufacturerDAO.GetManufacturer();
    }
}
