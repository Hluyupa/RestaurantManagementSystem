using System;
using System.Collections.Generic;

#nullable disable

namespace RestarauntWebApplication.Models.EFModels
{
    public partial class Waiter
    {
        public Waiter()
        {
            Orders = new HashSet<Order>();
            ServiceZones = new HashSet<ServiceZone>();
        }

        public int WaiterId { get; set; }
        public int? WorkerId { get; set; }

        public virtual Worker Worker { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<ServiceZone> ServiceZones { get; set; }
    }
}
