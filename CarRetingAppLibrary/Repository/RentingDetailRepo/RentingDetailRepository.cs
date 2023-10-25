using CarRetingAppLibrary.BussinessObject;
using CarRetingAppLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.Repository.RentingDetailRepo
{
    public class RentingDetailRepository : IRentingDetailRepository
    {
        RentingDetailDAO detailDAO = new RentingDetailDAO();    
        public void createRentingDetail(RentingDetail rentingDetail)
        {
            detailDAO.CreateRentingDetail(rentingDetail);
        }

        public List<RentingDetail> GetRentingDetail()
        {
            return detailDAO.GetRentingDetails();
        }
    }
}
