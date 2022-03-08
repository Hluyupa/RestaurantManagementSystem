using System;
using System.Collections.Generic;

#nullable disable

namespace RestarauntWebApplication.Models.EFModels
{
    public partial class Table
    {
        public Table()
        {
            ServiceZones = new HashSet<ServiceZone>();
            VisitorsTables = new HashSet<VisitorsTable>();
        }

        public int TableId { get; set; }
        public string TableDescription { get; set; }
        public int? TableCountSeat { get; set; }
        public string TableMapPosition { get; set; }

        public virtual ICollection<ServiceZone> ServiceZones { get; set; }
        public virtual ICollection<VisitorsTable> VisitorsTables { get; set; }
    }
}
