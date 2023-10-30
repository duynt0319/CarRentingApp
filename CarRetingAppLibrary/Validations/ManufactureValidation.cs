using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRetingAppLibrary.DataAccess;

namespace CarRetingAppLibrary.Validations
{
    public class ManufactureValidation
    {
        private Manufacturer _manufacture;

        public ManufactureValidation(Manufacturer manu)
        {
            _manufacture = manu;
        }

        public bool IsValidId()
        {
            if(_manufacture.ManufacturerId == null) return false;
            return true;
        }

        
    }
}
