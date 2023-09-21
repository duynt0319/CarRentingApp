using CarRetingAppLibrary.BussinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.Repository.RentingDetail
{
    public class RentingDetailRepository : IRentingDetailRepository
    {
        public List<DataAccess.RentingDetail> GetRentingDetail()
        {
            return RentingDetailDAO.GetRentingDetails();
        }
    }
}
