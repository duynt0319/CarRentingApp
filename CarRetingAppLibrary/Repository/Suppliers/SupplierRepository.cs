using CarRetingSystemLibrary.BussinessObject;
using CarRetingSystemLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingSystemLibrary.Repository.Suppliers
{
    public class SupplierRepository : ISupplierRepository
    {
        public List<Supplier> GetSupplier() => SupplierDAO.GetSupplier();
    }
}
