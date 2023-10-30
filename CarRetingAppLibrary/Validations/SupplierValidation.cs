using CarRetingAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.Validations
{
    public class SupplierValidation
    {
        private Supplier _supplier;

        public SupplierValidation(Supplier sup)
        {
            _supplier = sup;
        }

        public bool IsValidId()
        {
            if (_supplier.SupplierId == null) return false;
            return true;
        }
    }
}
