using CarRetingAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.Repository.Manufacturers
{
    public interface IManufacturersRepository
    {
        List<Manufacturer> GetManufacturer();
    }
}
