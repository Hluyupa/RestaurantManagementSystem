using System;
using System.Collections.Generic;

#nullable disable

namespace RestarauntWebApplication.Models.EFModels
{
    public partial class VisitorsTable
    {
        public int BookingId { get; set; }
        public int? VisitorId { get; set; }
        public int? TableId { get; set; }
        public DateTime? DateBooking { get; set; }

        public virtual Table Table { get; set; }
        public virtual Visitor Visitor { get; set; }
    }
}
