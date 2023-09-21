using CarRetingSystemLibrary.BussinessObject;
using CarRetingSystemLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingSystemLibrary.Repository.RentingDetail
{
    public class RentingDetailRepository : IRentingDetailRepository
    {
        public List<DataAccess.RentingDetail> GetRentingDetail() => RentingDetailDAO.GetRentingDetail();
    }
}
