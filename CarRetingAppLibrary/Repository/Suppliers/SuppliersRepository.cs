using CarRetingAppLibrary.BussinessObject;
using CarRetingAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.Repository.Suppliers
{
    public class SuppliersRepository : ISuppliersRepository
    {
        public List<Supplier> GetSupplier()
        {
            return SupplierDAO.GetSupplier();
        }
    }
}
