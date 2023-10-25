using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRetingAppLibrary.DataViewModel
{
    public class RentingTransactionViewModel
    {
        public string CustomerName { get; set; }
        public DateTime? RentingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public byte RentingStatus { get; set; }
        public string CarName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
    }
}
