using CarRetingSystemLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingSystemLibrary.Repository.Suppliers
{
    public interface ISupplierRepository
    {
        List<Supplier> GetSupplier();
    }
}
