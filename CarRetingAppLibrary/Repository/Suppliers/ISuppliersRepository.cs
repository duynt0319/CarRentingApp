using CarRetingAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.Repository.Suppliers
{
    public interface ISuppliersRepository
    {
        List<Supplier> GetSupplier();
    }
}
