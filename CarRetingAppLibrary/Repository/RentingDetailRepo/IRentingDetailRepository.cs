using CarRetingAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.Repository.RentingDetailRepo
{
    public interface IRentingDetailRepository
    {
        List<RentingDetail> GetRentingDetail();

        void createRentingDetail(RentingDetail rentingDetail);
    }
}
