using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CarRetingAppLibrary.DataAccess
{
    public partial class RentingTransaction
    {
        public RentingTransaction()
        {
            RentingDetails = new HashSet<RentingDetail>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentingTransationId { get; set; }
        public DateTime RentingDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public byte RentingStatus { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<RentingDetail> RentingDetails { get; set; }
    }
}
