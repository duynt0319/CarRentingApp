
using CarRetingAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.Validations
{
    public class CarValidation
    {
        private CarInformation _carInformation;

        public CarValidation(CarInformation car)
        {
            _carInformation = car;
        }



        public bool IsValidName()
        {
            if(_carInformation.CarName == null) return false;
            else if (_carInformation.CarName.Length < 1) return false;
            return true;
        }


        public bool IsValidDescription()
        {
            if(_carInformation.CarDescription == null) return false;
            else if (_carInformation.CarDescription.Length < 1) return false;
            return true;
        }


        public bool IsValidDoors()
        {
            if(_carInformation.NumberOfDoors == null) return false;
            else if (_carInformation.NumberOfDoors < 2)
            {
                return false;
            }
            else
            {
                int n;
                bool isNumeric = int.TryParse(_carInformation.NumberOfDoors.ToString(), out n);
                if (!isNumeric) return false;
                return true;
            }
        }


        public bool IsValidSeatingCapacity()
        {
            if (_carInformation.SeatingCapacity == null) return false;
            else if (_carInformation.SeatingCapacity < 2)
            {
                return false;
            }
            else
            {
                int n;
                bool isNumeric = int.TryParse(_carInformation.SeatingCapacity.ToString(), out n);
                if (!isNumeric) return false;
                return true;
            }
        }

        public bool IsValidFuelType()
        {
            if(_carInformation.FuelType == null) return false;
            else if (_carInformation.FuelType.Trim().Length < 1) return false;
            return true;
        }


        public bool IsValidYear()
        {
            if(_carInformation.Year == null) return false;
            else if (_carInformation.Year > DateTime.Now.Year || _carInformation.Year < 2000) return false;
            return true;
        }

        public bool IsValidStatus()
        {
            if (_carInformation.CarStatus == null) return false;
            else if (_carInformation.CarStatus == 1 || _carInformation.CarStatus == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
