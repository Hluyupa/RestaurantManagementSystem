using System;
using System.Collections.Generic;

#nullable disable

namespace RestarauntWebApplication.Models.EFModels
{
    public partial class Visitor
    {
        public Visitor()
        {
            Orders = new HashSet<Order>();
            VisitorsTables = new HashSet<VisitorsTable>();
        }

        public int VisitorId { get; set; }
        public string VisitorFullName { get; set; }
        public string VisitorTelephone { get; set; }
        public string VisitorEmail { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<VisitorsTable> VisitorsTables { get; set; }
    }
}
