using CarRetingSystemLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingSystemLibrary.Repository.Manufacturers
{
    public interface IManufacturerRepository
    {
        List<Manufacturer> GetManufacturer();
    }
}
